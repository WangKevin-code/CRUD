using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using test_CRUD.Models;


namespace test_CRUD.Controllers
{
    public class CRUDController : Controller
    {
        private Database1Entities db = new Database1Entities();
        // GET: CRUD
        public ActionResult Index()
        {
            List<User> users = db.User.ToList();
           
            return View(users);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection n_user)
        {
            int id = Int32.Parse(n_user["Id"]);
            string name = n_user["Name"];
            string age = n_user["age"];
            User user = new User
            {
                Id = id,
                Name = name,
                age = age
            };
            db.User.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var d_user = db.User.FirstOrDefault(a => a.Id == id);
            if (d_user == null)
            {
                return HttpNotFound();
            }
            db.User.Remove(d_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult Detail(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FormCollection c_user)
        {
            int id = Int32.Parse(c_user["Id"]);
            string name = c_user["Name"];
            string age = c_user["age"];
            var user = db.User.Find(id);
            user.Name = name;
            user.age = age;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}