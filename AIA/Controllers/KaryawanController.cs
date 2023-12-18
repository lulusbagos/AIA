using AIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIA.Controllers
{
    public class KaryawanController : Controller
    {
        // GET: Karyawan
        private DtClsDB_AIADataContext db;
        private string controller_name = "Karyawan";
        private string title_name = "Karyawan";


        public KaryawanController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        // GET: KategoriUser
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
                    ViewBag.MenuTransaksiCount = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).Where(x => x.type == "Transaksi").OrderBy(x => x.title).Count();
                    ViewBag.MenuOperationCount = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).Where(x => x.type == "Operation").OrderBy(x => x.title).Count();
                    ViewBag.MenuHCGSCount = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).Where(x => x.type == "HCGS").OrderBy(x => x.title).Count();
                    ViewBag.MenuSSHECount = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).Where(x => x.type == "SSHE").OrderBy(x => x.title).Count();
                    ViewBag.MenuOPACount = db.tbl_r_menus.Where(x => x.kategori_user_id == Session["kategori_user_id"]).Where(x => x.type == "OPA").OrderBy(x => x.title).Count();
                    ViewBag.insert_by = Session["nrp"];
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
        }

        public ActionResult GetAll()
        {
            try
            {
                var results = db.tbl_operator_fatigue_need_followups.OrderBy(x => x.pid).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Get(string pid)
        {
            try
            {
                var results = db.tbl_operator_fatigue_need_followups
                                .Where(x => x.pid == pid)
                                .FirstOrDefault();

                if (results != null)
                {
                    return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, remarks = "Gagal", data = "Data tidak ditemukan" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}