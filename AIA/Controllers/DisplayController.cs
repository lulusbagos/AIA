using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AIA.Models;

namespace AIA.Controllers
{
    public class DisplayController : Controller
    {

        private DtClsDB_AIADataContext db;
        private string controller_name = "Display";
        private string title_name = "DISPLAY";


        // GET: Display
        public DisplayController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        public ActionResult GetRunning()
        {
            try
            {
                var results = db.tbl_m_running_texts.OrderBy(x => x.pid).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Index()
        {
            var resultSmartDLastUpdate = db.tbl_t_setting_fleets.OrderByDescending(x => x.updated_at).FirstOrDefault().updated_at;
            ViewBag.smartd_last_update = resultSmartDLastUpdate;
            return View();
        }

        public ActionResult Bus()
        {
            var resultSmartDLastUpdate = db.tbl_t_setting_fleets.OrderByDescending(x => x.updated_at).FirstOrDefault().updated_at;
            ViewBag.smartd_last_update = resultSmartDLastUpdate;
            return View();
        }

        public ActionResult Pengganti()
        {
            var resultSmartDLastUpdate = db.tbl_t_setting_fleets.OrderByDescending(x => x.updated_at).FirstOrDefault().updated_at;
            ViewBag.smartd_last_update = resultSmartDLastUpdate;
            return View();
        }

        public ActionResult Productivity()
        {
            var resultSmartDLastUpdate = db.tbl_t_setting_fleets.OrderByDescending(x => x.updated_at).FirstOrDefault().updated_at;
            ViewBag.smartd_last_update = resultSmartDLastUpdate;
            return View();
        }

        public ActionResult GetAllService()
        {
            try
            {
                var results = db.plan_services.OrderBy(x => x.start_time).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllServiceBesuk()
        {
            try
            {
                var results = db.plan_service_besuks.OrderBy(x => x.start_time).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetAllBus()
        {
            try
            {
                var results = db.ASM_VW_BUS.OrderBy(x => x.cluster).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GetAllPengganti()
        {
            try
            {
                var results = db.ASM_Data_Operator_Penggantis.OrderBy(x => x.cluster).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetBottom(string pit)
        {
            try
            {
                var resultBottom = db.asm_display_bottom3s
                                    .Where(x => x.pit == pit)
                                    .OrderBy(x => x.Ranks)
                                    .ToList();

                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                var lineBottom = "";

                foreach (var item in resultBottom)
                {
                    lineBottom += "<div style='margin-top: 30px;'></div>";
                    lineBottom += "<div class='row'>";
                    lineBottom += "<div class='col-lg-12 center card-nama'>";
                    lineBottom += "<div class='row'>";
                    lineBottom += "<div class='col-lg-4 center'>";
                    lineBottom += item.nama;
                    lineBottom += "</div>";
                    lineBottom += "<div class='col-lg-3 center'>";
                    lineBottom += item.unit;
                    lineBottom += "</div>";
                    lineBottom += "<div class='col-lg-2 center' id='hm'>";
                    lineBottom += item.proty_hm;
                    lineBottom += "</div>";
                    lineBottom += "<div class='col-lg-3'>";
                    lineBottom += "<div id='" + item.presentase + "' class='progress-bar'>";
                    lineBottom += item.presentase;
                    lineBottom += " %</div>";
                    lineBottom += "</div>";
                    lineBottom += "</div>";
                    lineBottom += "</div>";
                    lineBottom += "</div>";
                }

                return Json(new { status = true, data = lineBottom }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetTop(string sektor)
        {
            try
            {
                var resultTop = db.asm_display_top3s
                                    .Where(x => x.pit == sektor)
                                    .OrderBy(x => x.Ranks)
                                    .ToList();

                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
                var lineTop = "";

                foreach (var item in resultTop)
                {
                    lineTop += "<div style='margin-top: 30px;'></div>";
                    lineTop += "<div class='row'>";
                    lineTop += "<div class='col-lg-12 center card-top'>";
                    lineTop += "<div class='row'>";
                    lineTop += "<div class='col-lg-4 center'>";
                    lineTop += item.nama;
                    lineTop += "</div>";
                    lineTop += "<div class='col-lg-3 center'>";
                    lineTop += item.unit;
                    lineTop += "</div>";
                    lineTop += "<div class='col-lg-2 center' id='hm'>";
                    lineTop += item.proty_hm;
                    lineTop += "</div>";
                    lineTop += "<div class='col-lg-3'>";
                    lineTop += "<div id='" + item.presentase + "' class='progress-bar1'>";
                    lineTop += item.presentase;
                    lineTop += " %</div>";
                    lineTop += "</div>";
                    lineTop += "</div>";
                    lineTop += "</div>";
                    lineTop += "</div>";
                }

                return Json(new { status = true, data = lineTop }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }



    }
}