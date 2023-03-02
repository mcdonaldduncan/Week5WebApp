using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Week5WebApp;
using Week5WebApp.Services;
using Week5WebApp.Services.ModelServices;

namespace Week5WebApp.Controllers
{
    public class UserController : Controller
    {
        private UserServices userServices = new UserServices();


        // GET: User
        public ActionResult Index()
        {
            if (Session.Count != 0 && Session["Authorized"].Equals(true))
            {
                return View(userServices.GetUsers());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: User/Signup
        public ActionResult Signup()
        {
            return View();
        }

        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "ID,Username,Password,Permissions")] User user)
        {
            if (userServices.GetUserByName(user.Username, out _))
            {
                ViewBag.ErrorMessage = "Username is already in use!";
                return View();
            }

            userServices.PrepareUserForSave(user);

            if (ModelState.IsValid)
            {
                userServices.AddUser(user);

                Session["Authorized"] = true;
                Session["permission"] = user.Permissions.ToLower();
                Session["ID"] = user.ID;

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // POST: User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "ID,Username,Password")] User user)
        {
            if (!userServices.GetUserByName(user.Username, out User tempUser) || !userServices.VerifyPassword(user, tempUser))
            {
                ViewBag.ErrorMessage = "Invalid username or password!";
                return View();
            }

            Session["Authorized"] = true;
            Session["permission"] = tempUser.Permissions.ToLower();
            Session["ID"] = tempUser.ID;

            return RedirectToAction("Details", tempUser);
        }


        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (!Session["permission"].Equals("user") || Session["ID"].Equals(id))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (userServices.GetUserByID(id, out User user))
                {
                    return View(user);
                }

                return HttpNotFound();

            }
            else
            {
                ViewBag.ErrorMessage = "Invalid permissions!";
                return RedirectToAction("Index");
            }
        }

        // GET: User/Create
        public ActionResult Create()
        {
            if (Session["permission"].Equals("user"))
            {
                ViewBag.ErrorMessage = "Invalid permissions!";
                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Username,Password,Permissions")] User user)
        {
            if (userServices.GetUserByName(user.Username, out _))
            {
                ViewBag.ErrorMessage = "Username is already in use!";
                return View();
            }

            userServices.PrepareUserForSave(user);

            if (ModelState.IsValid)
            {
                userServices.AddUser(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["permission"].Equals("user"))
            {
                ViewBag.ErrorMessage = "Invalid permissions!";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (userServices.GetUserByID(id, out User user))
            {
                return View(user);
            }

            return HttpNotFound();
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Permissions")] User user)
        {
            userServices.PrepareUserForSave(user);

            if (ModelState.IsValid)
            {
                userServices.SaveUserChanges(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!Session["permission"].Equals("super admin"))
            {
                ViewBag.ErrorMessage = "Invalid permissions!";
                return RedirectToAction("Index");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (userServices.GetUserByID(id, out User user))
            {
                return View(user);
            }

            return HttpNotFound();
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            userServices.DeleteUserByID(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            userServices.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
