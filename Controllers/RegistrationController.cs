using Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment.Controllers
{
    public class RegistrationController : Controller
    {
        public ActionResult RegIndex()
        {
            //retrv from db
            var db = new ZeroHungerDBEntities();
            var users = db.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            //add to db
            var db = new ZeroHungerDBEntities();
            db.Users.Add(user);
            db.SaveChanges();
            TempData["msg"] = "User Added Successfully";
            return RedirectToAction("RegIndex");
        }
        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserLogin(User user)
        {
            TempData["msg"] = "Logged in successfully.";

            if (ModelState.IsValid)
            {
                using (var db = new ZeroHungerDBEntities())
                {
                    var obj = db.Users.Where(a => a.email.Equals(user.email) && a.password.Equals(user.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["Id"] = obj.id.ToString();
                        Session["Type"] = obj.type.ToString();
                        Session["Name"] = obj.name.ToString();
                        if (obj.type == "Admin")
                        {
                            return RedirectToAction("DashBoard");
                        }
                        else if (obj.type == "Restaurant")
                        {
                            return RedirectToAction("ResDashBoard");
                        }
                        else
                        {
                            return RedirectToAction("EmpDashBoard");
                        }

                    }
                }
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Dashboard()
        {
            var db = new ZeroHungerDBEntities();
            var orders = db.Orders.ToList();
            return View(orders);
        }
        [HttpGet]
        public ActionResult AdminHistory()
        {
            var db = new ZeroHungerDBEntities();
            var orders = db.Orders.ToList();
            return View(orders);
        }
        [HttpGet]
        public ActionResult AdminRes()
        {
            var db = new ZeroHungerDBEntities();
            var users = db.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult AdminEmp()
        {
            var db = new ZeroHungerDBEntities();
            var users = db.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult ResDashboard()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResDashboard(Order order)
        {
            var db = new ZeroHungerDBEntities();
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("OrderIndex");
        }
        [HttpGet]
        public ActionResult EmpDashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult OrderIndex()
        {
            TempData["msg"] = "Order Added Successfully";
            var db = new ZeroHungerDBEntities();
            var orders = db.Orders.ToList();
            return View(orders);
        }
        [HttpGet]
        public ActionResult ResHistory()
        {
            var db = new ZeroHungerDBEntities();
            var orders = db.Orders.ToList();
            return View(orders);
        }
        [HttpGet]
        public ActionResult ResDetails()
        {
            var db = new ZeroHungerDBEntities();
            var users = db.Users.ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult ResEdit()
        {
            var db = new ZeroHungerDBEntities();
            var users = db.Users.ToList();
            return View(users);

        }
        [HttpPost]
        public ActionResult ResEdit(User usr)
        {
            var db = new ZeroHungerDBEntities();
            var ext = (from st in db.Users
                       where st.id == usr.id
                       select st).FirstOrDefault();
            db.Entry(ext).CurrentValues.SetValues(usr);
            //db.SaveChanges();
            return RedirectToAction("ResDetails");

        }
        [HttpGet]
        public ActionResult AdminReqEdit()
        {
            var db = new ZeroHungerDBEntities();
            var users = db.Users.ToList();
            return View(users);

        }


    }
}