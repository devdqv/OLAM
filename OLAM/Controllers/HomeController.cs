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
using OfficeOpenXml;
using System.Drawing;

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
        public ActionResult ExportToExcelXuongTachVo(string startdate = "", string enddate = "")
        {
            OLAMEntities db = new OLAMEntities();

            DateTime startD = new DateTime();
            DateTime endD = new DateTime();
            if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
            {
                startD = Convert.ToDateTime(startdate, objCultureInfo);
                ViewBag.startD = startD;
                endD = Convert.ToDateTime(enddate, objCultureInfo);
                ViewBag.endD = endD;


            }

            List<PEELINGModel> peelings = db.PEELINGs.Where(x => x.time_update <= endD && x.time_update >= startD).OrderByDescending(x => x.time_update).Select(x=> 
           new PEELINGModel{ ss_pressure= x.ss_pressure, value_pressure= x.value_pressure, ss_speeddrum= x.ss_speeddrum, Value_speeddrum=x.Value_speeddrum, time_update=x.time_update}).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "LOẠI";
            ws.Cells["B1"].Value = "Xưởng tách vỏ";

            ws.Cells["A2"].Value = "Từ ngày";
            ws.Cells["B2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells["B2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
            ws.Cells["B2"].Value = startdate;
            ws.Cells["C2"].Value = "Đến ngày";
            ws.Cells["D2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells["D2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
            ws.Cells["D2"].Value = enddate;

            ws.Cells["a4"].Value = "Timer";
            ws.Cells["b4"].Value = "....";

            ws.Row(6).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Row(6).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
            ws.Cells["A6"].Value = "Áp suất(Bar)";
            ws.Cells["C6"].Value = "Tốc độ(Drum)";

            ws.Cells["A7"].Value = "Tên cảm biến";
            ws.Cells["B7"].Value = "Giá trị";
            ws.Cells["C7"].Value = "Tên cảm biến";
            ws.Cells["D7"].Value = "Giá trị";
            ws.Cells["E7"].Value = "Thời gian cập nhật";

            int rowStart = 8;
            foreach (var item in peelings)
            {
                //if (item.Experience < 5)
                //{
                //    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                //    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                //}

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ss_pressure;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.value_pressure;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.ss_speeddrum;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Value_speeddrum;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.time_update.Value.ToString("dd/MM/yyyy HH:mm");
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

            return RedirectToAction("XuongTachVo");
        }


        [HttpPost]
        public ActionResult ExportToExcelXuongSayKho(string startdate = "", string enddate = "")
        {
            OLAMEntities db = new OLAMEntities();

            DateTime startD = new DateTime();
            DateTime endD = new DateTime();
            if (!string.IsNullOrEmpty(startdate) && !string.IsNullOrEmpty(enddate))
            {
                startD = Convert.ToDateTime(startdate, objCultureInfo);
                ViewBag.startD = startD;
                endD = Convert.ToDateTime(enddate, objCultureInfo);
                ViewBag.endD = endD;


            }

            List<CUTTING_DRYING> peelings = db.CUTTING_DRYING.Where(x => x.time_update <= endD && x.time_update >= startD).OrderByDescending(x => x.time_update).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "LOẠI";
            ws.Cells["B1"].Value = "Xưởng sấy khô";

            ws.Cells["A2"].Value = "Từ ngày";
            ws.Cells["B2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells["B2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
            ws.Cells["B2"].Value = startdate;
            ws.Cells["C2"].Value = "Đến ngày";
            ws.Cells["D2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Cells["D2"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
            ws.Cells["D2"].Value = enddate;

            ws.Cells["a4"].Value = "Timer";
            ws.Cells["b4"].Value = "....";

            ws.Row(6).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            ws.Row(6).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));
            ws.Cells["A6"].Value = "Nhiệt độ (Độ C)";
            ws.Cells["C6"].Value = "ĐỘ ẩm (%)";

            ws.Cells["A7"].Value = "Tên cảm biến";
            ws.Cells["B7"].Value = "Giá trị";
            ws.Cells["C7"].Value = "Tên cảm biến";
            ws.Cells["D7"].Value = "Giá trị";
            ws.Cells["E7"].Value = "Thời gian cập nhật";

            int rowStart = 8;
            foreach (var item in peelings)
            {
                //if (item.Experience < 5)
                //{
                //    ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                //    ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("pink")));

                //}

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.ss_temper;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.value_temper;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.ss_humidity;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.value_humidity;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.time_update.Value.ToString("dd/MM/yyyy HH:mm");
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

            return RedirectToAction("XuongTachVo");
        }




    }
}