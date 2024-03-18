using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using AIA.Models;


namespace AIA.Controllers
{
    public class DisplaySupportController : Controller
    {

        private DtClsDB_AIADataContext db;
        private string controller_name = "DisplaySupport";
        private string title_name = "DisplaySupport";


        // GET: DisplaySPR1
        public DisplaySupportController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        public ActionResult Index()
        {
            var resultSmartDLastUpdate = db.tbl_t_setting_fleets.OrderByDescending(x => x.updated_at).FirstOrDefault().updated_at;
            ViewBag.smartd_last_update = resultSmartDLastUpdate;
            return View();
        }

        public ActionResult GetDozer()
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => x.LOADER.Contains("DOZER") && x.cn_unit != null)
                    .OrderBy(x => x.cn_unit)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 25px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].RemOpaCNunit + "</div>";
                    lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultLoader[i].colornewdisplay + ";'>" + resultLoader[i].NAMA + "</div>";
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

        public ActionResult GetDriling()
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => x.LOADER.Contains("DRILING") && x.cn_unit != null)
                    .OrderBy(x => x.cn_unit)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 25px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].RemOpaCNunit + "</div>";
                    lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultLoader[i].colornewdisplay + ";'>" + resultLoader[i].NAMA + "</div>";
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


        public ActionResult GetGrader()
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => x.LOADER.Contains("GRADER") && x.cn_unit != null)
                    .OrderBy(x => x.cn_unit)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 25px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].RemOpaCNunit + "</div>";
                    lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultLoader[i].colornewdisplay + ";'>" + resultLoader[i].NAMA + "</div>";
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

        public ActionResult GetLD()
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => x.LOADER.Contains("LD") && x.cn_unit != null)
                    .OrderBy(x => x.cn_unit)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 25px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].RemOpaCNunit + "</div>";
                    lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultLoader[i].colornewdisplay + ";'>" + resultLoader[i].NAMA + "</div>";
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

        public ActionResult GetSTB()
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => x.LOADER.Contains("STB") && x.cn_unit != null)
                    .OrderBy(x => x.cn_unit)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 25px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].RemOpaCNunit + "</div>";
                    lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultLoader[i].colornewdisplay + ";'>" + resultLoader[i].NAMA + "</div>";
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

        public ActionResult GetWorkshop()
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => x.LOADER.Contains("WORKSHOP") && x.cn_unit != null)
                    .OrderBy(x => x.cn_unit)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 30px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].RemOpaCNunit + "</div>";
                    lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultLoader[i].colornewdisplay + ";'>" + resultLoader[i].NAMA + "</div>";
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


        public ActionResult GetUnitnoset()
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => x.LOADER.Contains("unit_no_setting") && x.cn_unit != null)
                    .OrderBy(x => x.cn_unit)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 30px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].RemOpaCNunit + "</div>";
                    lineLoader += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultLoader[i].colornewdisplay + ";'>" + resultLoader[i].NAMA + "</div>";
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
    }
}