using OLAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OLAM.Controllers
{
    public class LoginController : Controller
    {

        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));
            
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        // GET: Login
        public ActionResult Index()
        {
            Session["user"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(USERLOGIN request)
        {
            OLAMEntities db = new OLAMEntities();
            var pass = MD5Hash(request.password);
            var user = db.USERLOGINs.SingleOrDefault(x => x.username == request.username && x.password == pass);
            if(user==null)
            {
                ModelState.AddModelError("", "Tên tài khoản hoặc mật khẩu không đúng");
                return View(request);
            }
            Session["user"] = user;
            return RedirectToAction("Index", "Home");
        }
    }
}