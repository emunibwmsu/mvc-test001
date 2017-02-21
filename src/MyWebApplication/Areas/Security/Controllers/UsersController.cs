using MyWebApplication.Areas.Security.Models;
using MyWebApplication.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApplication.Areas.Security.Controllers
{
    public class UsersController : Controller
    {

        private IList<UserModelView> Users
        {
            get
            {
                if (Session["data"] == null)
                {
                   Session["data"] = new List<UserModelView>(){
                   new UserModelView {
                Id = Guid.NewGuid(),
                Firstname = "Esh-haar",
                Lastname ="Munib",
                Gender = "Female",
                Age = 21
                },
                 new UserModelView
                {
                Id = Guid.NewGuid(),  
                Firstname = "Sitti",
                Lastname ="Talib",
                Gender = "Female",
                Age = 21
                }
            };
 
                       
               }
                return Session["data"] as List<UserModelView>;
            }
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
                    Age = user.Age

                }).ToList();

                return View(users);
            }
            
        }

        
        public ActionResult Details(Guid id)
        {
            var ur = Users.FirstOrDefault(user => user.Id == id);
            return View(ur);
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
                    db.Users.Add(new User
                        {
                            Id = Guid.NewGuid(),
                            Firstname = viewModel.Firstname,
                            Lastname = viewModel.Lastname,
                            Gender = viewModel.Gender,
                            Age = viewModel.Age
                        });
                    db.SaveChanges();
                }
     
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(Guid id)
        {
           var e = Users.FirstOrDefault(user => user.Id == id);
            return View(e);
        }

        
        [HttpPost]
        public ActionResult Edit(Guid id,UserModelView viewModel)
        {
            try
            {   
                    var edit =db.Users.FirstOrDefault(user => user.Id == id);
                    edit.Firstname = viewModel.Firstname;
                    edit.Lastname = viewModel.Lastname;
                    edit.Age = viewModel.Age;
                    edit.Gender = viewModel.Gender;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(Guid id)
        {
            var usr = Users.FirstOrDefault(user => user.Id == id);
            return View(usr);
        }

        
        [HttpPost]
        public ActionResult Delete(Guid id,FormCollection collection)
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
    }
}
