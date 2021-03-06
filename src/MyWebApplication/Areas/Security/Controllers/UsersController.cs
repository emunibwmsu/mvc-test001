﻿using MyWebApplication.Areas.Security.Models;
using MyWebApplication.dal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApplication.Areas.Security.Controllers
{
    public class UsersController : Controller
    {

       public UsersController ()
       {
          var genders = new List<SelectListItem>{
                new SelectListItem
                {
                  Value= "Male",
                  Text ="Male"
                },
                   new SelectListItem
                {
                  Value= "Female",
                  Text ="Female"
                }
            };
            ViewBag.Gender = genders;
    }
        
        public ActionResult Index()
        {
            using(var db = new DatabaseContext())
            {
                var users = (from user in db.Users
                             select new UserModelView
                {
                    Id = user.Id,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Gender = user.Gender,
                    Age = user.Age,
                    EmploymentDate = user.EmploymentDate,
                    Education = user.Educations.Select(s =>
                          new EducationModelView
                          {
                              School = s.School,
                              YearAttended = s.YearAttended
                          }).ToList()

                }).ToList();

                return View(users);
            }
            
        }


        
        public ActionResult Details(int id)
        {
           
            return View(GetUser(id));
        }

        
        public ActionResult Create()
        {
            ViewBag.Gender = new List<SelectListItem>{
                new SelectListItem
                {
                  Value= "Male",
                  Text ="Male"
                },
                   new SelectListItem
                {
                  Value= "Female",
                  Text ="Female"
                }
            };
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModelView viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View();

                using (var db = new DatabaseContext())
                {
                    var newUser = new User
                    {
                        Guid = Guid.NewGuid(),
                        Firstname = viewModel.Firstname,
                        Lastname = viewModel.Lastname,
                        Age = viewModel.Age,
                        Gender = viewModel.Gender,
                        EmploymentDate = viewModel.EmploymentDate
                    };

                    if (viewModel.Education != null)
                    {
                        foreach (var edu in viewModel.Education)
                        {
                            newUser.Educations.Add(new Education
                            {
                                School = edu.School,
                                YearAttended = edu.YearAttended
                            });
                        }
                    }

                    db.Users.Add(newUser);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }


        // GET: Security/Users/Edit/5
        public ActionResult Edit(int Id)
        {
            using (var db = new DatabaseContext())
            {
                var users = (from user in db.Users
                             select new UserModelView
                             {
                                 Id = user.Id,
                                 Firstname = user.Firstname,
                                 Lastname = user.Lastname,
                                 Age = user.Age,
                                 Gender = user.Gender,
                                 EmploymentDate = user.EmploymentDate
                               
                             }).ToList();

                var u = users.FirstOrDefault(user => user.Id == Id);

                return View(u);
            }
        }

        // POST: Security/Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int Id, UserModelView viewModel)
        {
            try
            {
                using (var db = new DatabaseContext())
                {


                    var u = db.Users.FirstOrDefault(us => us.Id == Id);
                    if (u != null)
                    {
                        u.Firstname = viewModel.Firstname;
                        u.Lastname = viewModel.Lastname;
                        u.Age = viewModel.Age;
                        u.Gender = viewModel.Gender;
                        u.EmploymentDate = viewModel.EmploymentDate;
                        
                        db.SaveChanges();

                    }
                    return RedirectToAction("Index");
                }
            }

            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            return View(GetUser(id));
        }

        
        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var delete =db.Users.FirstOrDefault(user => user.Id == id);
                    db.Users.Remove(delete);

                    db.SaveChanges();
               
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        private UserModelView GetUser(int id)
        {
            using (var db = new DatabaseContext())
            {
                return (from user in db.Users
                        where user.Id == id
                        select new UserModelView
                        {
                            Id = user.Id,
                            Firstname = user.Firstname,
                            Lastname = user.Lastname,
                            Age = user.Age,
                            Gender = user.Gender,
                            EmploymentDate = user.EmploymentDate
                           
                        }).FirstOrDefault();

            }

        }
    }
}
