using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIA.Models;


namespace AIA.Controllers
{
    public class Display3ShiftController : Controller
    {
        private DtClsDB_AIADataContext db;
        private string controller_name = "Display3Shift";
        private string title_name = "Display3Shift";

        public Display3ShiftController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        public ActionResult Area1()
        {
            return View();
        }


        public ActionResult GetLoader(String sektor)
        {
            try
            {

                var resultLoader = db.tbl_setting_fleet_vs_ftw_3s_ls
                .Where(x => (x.loader.Contains("EX") || x.loader.Contains("SH")) && x.sektor == sektor && x.hauler == null)
                .OrderBy(x => x.loader)
                .ToList();
                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                var lineLoader = "";

                for (int i = 0; i < resultLoader.Count(); i++)
                {
                    lineLoader += "<div style='width: 130px; height: 80px; margin-left: 1px; margin-right: 2px; background-color: #000000; margin-bottom: 10px;'>";
                    lineLoader += "<center>";
                    lineLoader += "<center><div class='px-1 py-1 font-weight-bold text-white' style='background-color:#000000; font-size: 15px; margin: 1px;'>" + resultLoader[i].areachangeshift + "</div></center>";
                    lineLoader += "<div>";
                    lineLoader += "<div class='card bg-light text-black shadow'>";
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary id='loader' style='font-size: 30px; font-weight: bold;'>" + resultLoader[i].loader + "</div>";

                    if (resultLoader[i].Nama != null)
                    {
                        lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 7px; font-weight: bold; color:#F0FFFF;";

                        if (resultLoader[i].status_fatigue == "FIT")
                        {
                            lineLoader += "background-color: #008000;'>"; // Operator FIT
                        }
                        else if (resultLoader[i].status_fatigue == null)
                        {
                            lineLoader += "background-color: #4B0082;'>";
                        }
                        else
                        {
                            lineLoader += "background-color: #B22222;'>"; // Operator tidak fit
                        }

                        lineLoader += resultLoader[i].Nama + "</div>";
                    }
                    lineLoader += "</div>";
                    lineLoader += "</div>";
                    lineLoader += "</center>";
                    lineLoader += "</div>";
                }


                return Json(new { status = true, data = lineLoader }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetHauler(String sektor)
        {
            try
            {
                var resultLoader = db.tbl_setting_fleet_vs_ftw_3s_ls
                    .Where(x => (x.loader.Contains("EX") || x.loader.Contains("SH")) && x.sektor == sektor && x.hauler == null)
                    .OrderBy(x => x.loader)
                    .ToList();

                var lineHauler = "";
                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                for (int i = 0; i < resultLoader.Count(); i++)
                {
                    var resultHauler = db.tbl_setting_fleet_vs_ftw_3s_ls
                        .Where(x => x.loader == resultLoader[i].loader && x.hauler != null && x.hauler.Contains("DT"))
                        .OrderBy(x => x.hauler)
                        .Distinct()
                        .ToList();

                    lineHauler += "<div style='width: 130px; hight: 100px; margin-left: 1px; margin-right: 2px; margin-bottom: 30px; margin-top: 10px;'>";
                    lineHauler += "<div style='margin-bottom: 20px;'></div>";
                    for (int a = 0; a < resultHauler.Count(); a++)
                    {
                        lineHauler += "<center style='margin-bottom: 15px;'>";
                        lineHauler += "<div style='margin: 0; padding: 0;'>"; 
                        lineHauler += "<div class='card bg-light text-black shadow' style='margin: 0; padding: 0;'>"; 
                        lineHauler += "<div class='px-1 py-1 btn btn-lg btn-secondary id='loader' style='font-size: 28px; font-weight: bold; margin: 0; padding: 0;'>" + resultHauler[a].hauler + "</div>";

                        if (resultHauler[a].Nama != null)
                        {
                            lineHauler += "<div class='px-1 py-1 btn-lg' style='font-size: 7px; font-weight: bold; color:#F0FFFF;";

                            if (resultHauler[a].status_fatigue == "FIT")
                            {
                                lineHauler += "background-color: #008000;'>"; // Operator FIT
                            }
                            else if (resultHauler[a].status_fatigue == null)
                            {
                                lineHauler += "background-color: #4B0082;'>";
                            }
                            else
                            {
                                lineHauler += "background-color: #B22222;'>"; // Operator tidak fit
                            }

                            lineHauler += resultHauler[a].Nama + "</div>";
                        }

                        else
                        {
                            lineHauler += "<div class='px-1 py-1 btn-lg' style='font-size: 7px; font-weight: bold; background-color: #696969; color:#F0FFFF;'>No operator</div>";
                        }
                        lineHauler += "</div>";
                        lineHauler += "</div>";
                        lineHauler += "</center>";
                    }

                    lineHauler += "</div>";
                }
                return Json(new { status = true, data = lineHauler }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetStandby()
        {
            try
            {

                var resultLoader = db.tbl_setting_fleet_vs_ftw_3s_ls
                                    .Where(x => x.loader.Contains("unit_standby"))
                                    .OrderBy(x => x.loader)
                                    .ToList();

                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                var lineLoader = "";

                for (int i = 0; i < resultLoader.Count(); i++)
                {

                    lineLoader += "<div style='width: 130px; height: 80px; margin-left: 1px; margin-top: 1px; margin-right: 2px; background-color: #000000; margin-bottom: 1px;'>";
                    lineLoader += "<center>";
                    lineLoader += "<center><div class='px-1 py-1 font-weight-bold text-white' style='background-color:#000000; font-size: 15px; margin: 1px;'>-</div></center>";
                    lineLoader += "<div>";
                    lineLoader += "<div class='card bg-light text-black shadow'>";
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary id='loader' style='font-size: 30px; font-weight: bold;'>" + resultLoader[i].hauler + "</div>";

                    if (resultLoader[i].Nama != null)
                    {
                        lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 8px; font-weight: bold; color:#F0FFFF;";

                        if (resultLoader[i].status_fatigue == "FIT")
                        {
                            lineLoader += "background-color: #008000;'>"; // Operator FIT
                        }
                        else if (resultLoader[i].status_fatigue == null)
                        {
                            lineLoader += "background-color: #4B0082;'>";
                        }
                        else
                        {
                            lineLoader += "background-color: #B22222;'>"; // Operator tidak fit
                        }

                        lineLoader += resultLoader[i].Nama + "</div>";
                    }
                    else
                    {
                        lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 7px; font-weight: bold; background-color: #696969; color:#F0FFFF;'>No operator</div>";
                    }
                    lineLoader += "</div>";
                    lineLoader += "</div>";
                    lineLoader += "</center>";
                    lineLoader += "</div>";
                }


                return Json(new { status = true, data = lineLoader }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetSupport()
        {
            try
            {

                var resultLoader = db.tbl_setting_fleet_vs_ftw_3s_ls
                                    .Where(x => x.hauler.Contains("GR") || x.hauler.Contains("DZ"))
                                    .ToList();

                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                var lineLoader = "";

                for (int i = 0; i < resultLoader.Count(); i++)
                {

                    lineLoader += "<div style='width: 130px; height: 80px; margin-left: 1px; margin-top: 1px; margin-right: 2px; background-color: #000000; margin-bottom: 1px;'>";
                    lineLoader += "<center>";
                    lineLoader += "<center><div class='px-1 py-1 font-weight-bold text-white' style='background-color:#000000; font-size: 15px; margin: 1px;'>-</div></center>";
                    lineLoader += "<div>";
                    lineLoader += "<div class='card bg-light text-black shadow'>";
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary id='loader' style='font-size: 30px; font-weight: bold;'>" + resultLoader[i].hauler + "</div>";

                    if (resultLoader[i].Nama != null)
                    {
                        lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 8px; font-weight: bold; color:#F0FFFF;";

                        if (resultLoader[i].status_fatigue == "FIT")
                        {
                            lineLoader += "background-color: #008000;'>"; // Operator FIT
                        }
                        else if (resultLoader[i].status_fatigue == null)
                        {
                            lineLoader += "background-color: #4B0082;'>";
                        }
                        else
                        {
                            lineLoader += "background-color: #B22222;'>"; // Operator tidak fit
                        }

                        lineLoader += resultLoader[i].Nama + "</div>";
                    }
                    else
                    {
                        lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 7px; font-weight: bold; background-color: #696969; color:#F0FFFF;'>No operator</div>";
                    }
                    lineLoader += "</div>";
                    lineLoader += "</div>";
                    lineLoader += "</center>";
                    lineLoader += "</div>";
                }


                return Json(new { status = true, data = lineLoader }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAll()
        {
            try
            {
                var results = db.vw_setting_fleet_vs_ftw_3s_ls
                               .OrderByDescending(x => x.loader) // Mengurutkan berdasarkan loader secara descending
                               .ToList();

                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}