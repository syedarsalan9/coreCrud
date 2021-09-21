using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginSignup.Models;
using System.Web.Mvc;

namespace LoginSignup.Controllers
{
    public class LoginSignupController : Controller
    {
        // GET: LoginSignup
        LoginSignupEntities db = new LoginSignupEntities();
        public ActionResult Index()
        {
            List<UserModel> item = db.UserModels.Select(x => new UserModel()
            {
                id = x.id,
                name = x.name,
                email = x.email,
            }).ToList();
            return View(item);
        }


    }
}