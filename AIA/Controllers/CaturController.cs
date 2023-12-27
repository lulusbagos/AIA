using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using AIA.Models;
using static System.Net.Mime.MediaTypeNames;

namespace AIA.Controllers
{
    public class CaturController : Controller
    {
        private DtClsDB_AIADataContext db;
        private string controller_name = "Catur";
        private string title_name = "Catur";


        public CaturController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetAll()
        {
            try
            {
                var results = db.ASM_TBL_UNIT_NOOPTs.OrderBy(x => x.CN_UNIT).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllOpt()
        {
            try
            {
                var results = db.ASM_TBL_OPT_STB_NOTSETs.OrderBy(x => x.NRP).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Update(string TGL, string shift, string cn_unit, string Pengganti, string Validasi, string validate_by)

        {
            try
            {
                var results = db.sp_update_bandara_catur(TGL, shift, cn_unit, Pengganti, Validasi, validate_by);
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllSummary()
        {
            try
            {
                var results = db.ASM_summary_caturs.FirstOrDefault(); 
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}