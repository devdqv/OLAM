using OLAM.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OLAM.Controllers
{
    public class HomeController : Controller
    {
        // public Models.Saleman mUserInforBH;
        public System.Globalization.CultureInfo objCultureInfo = new CultureInfo("vi-VN");

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult XuongTachVo(string startdate = "", string enddate = "", int page = 1, int pageSize = 6)
        {

            OLAMEntities db = new OLAMEntities();
            var pEELINGs = db.PEELINGs.OrderByDescending(x => x.time_update);
            DateTime datenow = DateTime.Now;
            DateTime startD = new DateTime();
            DateTime endD = new DateTime();
            if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
            {
                startD = Convert.ToDateTime(startdate, objCultureInfo);
                ViewBag.startD = startD;
                endD = Convert.ToDateTime(enddate, objCultureInfo);
                ViewBag.endD = endD;


            }
            else
            {
                startD = new DateTime(datenow.Year, datenow.Month, datenow.Day, 0, 0, 0);
                ViewBag.startD = startD;
                endD = new DateTime(datenow.Year, datenow.Month, datenow.Day + 3, 23, 59, 59);
                ViewBag.endD = endD;
            }
            ViewBag.PEELINGs = pEELINGs.Where(x => x.time_update <= endD && x.time_update >= startD).ToPagedList(page, pageSize);



            return View();
        }


    }
}