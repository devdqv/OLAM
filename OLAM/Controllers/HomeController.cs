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


namespace OLAM.Controllers
{
    public class HomeController : BaseController
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



            return View();
        }

        [HttpPost]
        public ActionResult ExportToExcel()
        {
            OLAMEntities db = new OLAMEntities();
            var gv = new GridView();
            gv.DataSource = db.PEELINGs.OrderByDescending(x => x.time_update).ToList().Select(x=> new{ x.ss_pressure, x.value_pressure, x.ss_speeddrum, x.Value_speeddrum, x.time_update});
            gv.DataBind();
            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition",
            // "attachment;filename=GridViewExport.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport_Ngay_"+DateTime.Now.ToString("dd-MM")+".xls");
            //Mã hóa chữa sang UTF8
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //Apply text style to each Row
                gv.Rows[i].Attributes.Add("class", "textmode");
            }
            //Add màu nền cho header của file excel
            gv.HeaderRow.BackColor = System.Drawing.Color.DarkOrange;
            //Màu chữ cho header của file excel
            gv.HeaderStyle.ForeColor = System.Drawing.Color.White;

            gv.HeaderRow.Cells[0].Text = "Tên cảm biến";
            gv.HeaderRow.Cells[1].Text = "Giá trị";
            gv.HeaderRow.Cells[2].Text = "Tên cảm biến";
            gv.HeaderRow.Cells[3].Text = "Giá trị";
            gv.HeaderRow.Cells[4].Text = "Ngày cập nhật";
            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("XuongTachVo");
        }


    }
}