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
    public class DisplayProductivityController : Controller
    {
        private DtClsDB_AIADataContext db;
        private string controller_name = "DisplayProductivity";
        private string title_name = "DisplayProductivity";

        // GET: DisplayProductivity
        public DisplayProductivityController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gettop3(String sektor)
        {
            try
            {
                var resultTop3 = db.display_productivities
                    .Where(x => x.kategori == "TOP" && x.sektor == sektor)
                    .OrderBy(x => x.RankDesc)
                    .ToList();

                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                var lineTop3 = "";

                for (int i = 0; i < resultTop3.Count(); i++)
                {
                    lineTop3 += "<div class='card mb-1 py-1 custom-border-grenn'>";
                    lineTop3 += "     <div class='card-body'>";
                    lineTop3 += "          <div class='row' > ";
                    lineTop3 += "              <div class='col-lg-4' style ='font-size: 10px; font-weight: bold;'>" + resultTop3[i].nama +  "</div>";
                    lineTop3 += "              <div class='col-lg-2' style ='font-size: 10px; font-weight: bold;'>" + resultTop3[i].unit + "</div>";
                    lineTop3 += "              <div class='col-lg-2' style ='font-size: 10px; font-weight: bold;'>" + resultTop3[i].PDTY + "</div>";
                    lineTop3 += "              <div class='col-lg-4'>";
                    lineTop3 += "                  <div class='container'>";
                    lineTop3 += "                      <div class='skills html' style ='font-size: 30px; width:" + resultTop3[i].presentase + "; max-width: 100%; background-color: #04AA6D; text-align: center;' >" + resultTop3[i].presentase + "</div>";
                    lineTop3 += "                  </div>";
                    lineTop3 += "              </div>";
                    lineTop3 += "          </div>";
                    lineTop3 += "     </div>";
                    lineTop3 += "</div>";
                }

                return Json(new { status = true, data = lineTop3 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetBottom3(String sektor)
        {
            try
            {

                var resultBotom3 = db.display_productivities
                    .Where(x => x.kategori == "BOTTOM" && x.sektor == sektor)
                    .OrderBy(x => x.RankDesc)
                    .ToList();
                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                var linebottom3 = "";

                for (int i = 0; i < resultBotom3.Count(); i++)
                {
                    linebottom3 += "<div class='card mb-1 py-1 custom-border-red'>";
                    linebottom3 += "     <div class='card-body'>";
                    linebottom3 += "          <div class='row' > ";
                    linebottom3 += "              <div class='col-lg-4' style ='font-size: 10px; font-weight: bold;'>" + resultBotom3[i].nama + "</div>";
                    linebottom3 += "              <div class='col-lg-2' style ='font-size: 10px; font-weight: bold;'>" + resultBotom3[i].unit + "</div>";
                    linebottom3 += "              <div class='col-lg-2' style ='font-size: 10px; font-weight: bold;'>" + resultBotom3[i].PDTY + "</div>";
                    linebottom3 += "              <div class='col-lg-4'>";
                    linebottom3 += "                  <div class='container'>";
                    linebottom3 += "                      <div class='skills html' style ='font-size: 30px; width:" + resultBotom3[i].presentase + "; max-width: 100%; background-color: #ff0000; text-align: center;' >" + resultBotom3[i].presentase + "</div>";
                    linebottom3 += "                  </div>";
                    linebottom3 += "              </div>";
                    linebottom3 += "          </div>";
                    linebottom3 += "     </div>";
                    linebottom3 += "</div>";
                }


                return Json(new { status = true, data = linebottom3 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}