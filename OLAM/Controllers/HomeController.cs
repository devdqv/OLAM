using OLAM.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLAM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XuongTachVo(int page=1, int pageSize=6)
        {
            OLAMEntities db = new OLAMEntities();
            ViewBag.PEELINGs = db.PEELINGs.OrderByDescending(x=>x.time_update).ToPagedList(page, pageSize);
            return View();
        }

      
    }
}