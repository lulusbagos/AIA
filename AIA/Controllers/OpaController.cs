using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIA.Models;

namespace AIA.Controllers
{
    public class OpaController : Controller
    {
        private DtClsDB_AIADataContext db;
        private string controller_name = "OPA";
        private string title_name = "OPA";

        public OpaController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        // GET: Opa
        public ActionResult Index()
        {
            if (Session["is_login"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var kategori_user_id = Session["kategori_user_id"];

                var cek_kategori_user_id = db.tbl_r_menus.Where(x => x.link_controller == controller_name).Where(x => x.kategori_user_id == kategori_user_id).Count();

                if (cek_kategori_user_id > 0)
                {
                    ViewBag.Title = title_name;
                    ViewBag.Controller = controller_name;
                    ViewBag.Setting = db.tbl_m_setting_aplikasis.FirstOrDefault();
                    ViewBag.Menu = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).OrderBy(x => x.title).ToList();
                    ViewBag.MenuMasterCount = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).Where(x => x.type == "Master").OrderBy(x => x.title).Count();
                    ViewBag.MenuMasterCount = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).Where(x => x.type == "OPA").OrderBy(x => x.title).Count();
                    ViewBag.insert_by = Session["nrp"];
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        public ActionResult GetAllKategori()
        {
            try
            {
                var results = db.tbl_r_alasan_fatigues.OrderBy(x => x.remarks).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllNextFollowup()
        {
            try
            {
                var results = db.tbl_r_next_followups.OrderBy(x => x.next_followup).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAllStatusCNC()
        {
            try
            {
                var results = db.tbl_r_status_sanksis.OrderBy(x => x.status_sanksi).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetAllHistory(string nrp)
        {
            try
            {
                var results = db.vw_history_followup_operator_fatigues
                               .Where(x => x.nrp == nrp)
                               .OrderBy(x => x.nrp)
                               .ToList();

                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public ActionResult Insert(tbl_t_konseling_fatigue a)
        {
            try
            {
                a.pid = a.pid ?? Guid.NewGuid().ToString();
                a.CNC_date = DateTime.Now;
                a.status = '1';
                db.tbl_t_konseling_fatigues.InsertOnSubmit(a);
                db.SubmitChanges();
                db.Dispose();
                return Json(new { status = true, remarks = "Sukses" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}