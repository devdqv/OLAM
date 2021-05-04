using OLAM.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Syncfusion.XlsIO;
using System.Data;
using System.ComponentModel;

namespace OLAM.Controllers
{
    public class HomeController : BaseController
    {
        // public Models.Saleman mUserInforBH;

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

            var listPeelings = pEELINGs.Where(x => x.time_update <= endD && x.time_update >= startD).ToList();
            if (listPeelings.Count == 0)
            {
                ViewBag.timer = 0;
            } else
            {
                var maxTimer = listPeelings.Max(x => x.timer_action).Value;
                ViewBag.timer = Math.Round((double)maxTimer / 1000 / 3600 * 100) / 100;
            }

            return View();
        }

        public ActionResult XuongSayKho(string startdate = "", string enddate = "", int page = 1, int pageSize = 6)
        {

            OLAMEntities db = new OLAMEntities();
            var CUTTING_DRYINGs = db.CUTTING_DRYING.OrderByDescending(x => x.time_update);
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
            ViewBag.CUTTING_DRYINGs = CUTTING_DRYINGs.Where(x => x.time_update <= endD && x.time_update >= startD).ToPagedList(page, pageSize);

            var listCutting = CUTTING_DRYINGs.Where(x => x.time_update <= endD && x.time_update >= startD).ToList();
            if (listCutting.Count == 0)
            {
                ViewBag.timer = 0;
            } else
            {
                var maxTimer = listCutting.Max(x => Math.Max(x.timer1.Value, x.timer2.Value));
                ViewBag.timer = Math.Round((double)maxTimer / 1000 / 3600 * 100) / 100;
            }

            return View();
        }

        [HttpPost]
        public ActionResult ExportToExcel()
        {
            OLAMEntities db = new OLAMEntities();
           var peelings = db.PEELINGs.OrderByDescending(x => x.time_update).Select(x=> new{ x.ss_pressure, x.value_pressure, x.ss_speeddrum, x.Value_speeddrum, x.time_update}).ToList();
            ExportToExcel excel = new ExportToExcel();
            DataTable dt = new DataTable();
            
            List<string> colNames = new List<string>();
            colNames.Add("Tên cảm biến");
            colNames.Add("Giá trị");
            colNames.Add("Tên cảm biến 2");
            colNames.Add("Giá trị 2");
            colNames.Add("Thời gian cập nhật");
            dt = ToDataTable(peelings, colNames);
            excel.Export(dt, DateTime.Now.ToString("dd-MM-yyyy"), "Điểm Danh Ngày " + DateTime.Now.ToString("dd-MM-yyyy"), colNames);
            return RedirectToAction("XuongTachVo");
        }

        public static DataTable ToDataTable<T>(IList<T> data, List<string> colNames)
        {
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < colNames.Count; i++)
            {
                table.Columns.Add(colNames[i], typeof(string));
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }


    }
}