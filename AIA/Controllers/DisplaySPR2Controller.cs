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
    public class DisplaySPR2Controller : Controller
    {

        private DtClsDB_AIADataContext db;
        private string controller_name = "DisplaySPR2";
        private string title_name = "DisplaySPR2";


        // GET: DisplaySPR1
        public DisplaySPR2Controller()
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
        
        public ActionResult GetLoader(String sektor)
        {
            try
            {

                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                .Where(x => (x.LOADER.Contains("EX") || x.LOADER.Contains("SH")) && x.sektor == sektor && x.cn_unit == null)
                .OrderBy(x => x.LOADER)
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
                    lineLoader += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 30px; font-weight: bold; color:" + resultLoader[i].prioritycolor + "; '>" + resultLoader[i].LOADER + "</div>";
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


        [HttpGet]
        public ActionResult GetHauler(String sektor)
        {
            try
            {
                var resultLoader = db.ASM_NEW_VER_DISPLAYs
                    .Where(x => (x.LOADER.Contains("EX") || x.LOADER.Contains("SH")) && x.sektor == sektor && x.cn_unit == null)
                    .OrderBy(x => x.LOADER)
                    .ToList();

                var lineHauler = "";
                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                for (int i = 0; i < resultLoader.Count(); i++)
                {
                    var resultHauler = db.ASM_NEW_VER_DISPLAYs
                        .Where(x => x.LOADER == resultLoader[i].LOADER && x.cn_unit != null && x.cn_unit.Contains("DT"))
                        .OrderBy(x => x.cn_unit)
                        .Distinct()
                        .ToList();

                    lineHauler += "<div style='width: 130px; hight: 100px; margin-left: 1px; margin-right: 2px; margin-bottom: 30px; margin-top: 10px;'>";
                    lineHauler += "<div style='margin-bottom: 20px;'></div>";
                    for (int a = 0; a < resultHauler.Count(); a++)
                    {
                        lineHauler += "<center style='margin-bottom: 15px;'>";
                        lineHauler += "<div style='margin: 0; padding: 0;'>";
                        lineHauler += "<div class='card bg-light text-black shadow' style='margin: 0; padding: 0;'>";
                        lineHauler += "<div class='px-1 py-1 btn btn-lg btn-secondary' id='loader' style='font-size: 30px; font-weight: bold; color:" + resultHauler[a].prioritycolor + "; '>" + resultHauler[a].RemOpaCNunit + "</div>";
                        lineHauler += "<div class='px-1 py-1 btn-lg' style='font-size: 9px; font-weight: bold; color:#F0FFFF;background-color:" + resultHauler[a].colornewdisplay + ";'>" + resultHauler[a].NAMA + "</div>";
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

    }
}