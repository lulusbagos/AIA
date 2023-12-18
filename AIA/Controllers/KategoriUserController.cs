using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIA.Models;

namespace AIA.Controllers
{
    public class KategoriUserController : Controller
    {
        private DtClsDB_AIADataContext db;
        private string controller_name = "KategoriUser";
        private string title_name = "KATEGORI USER";
        private string kategori_user_id;
        private string dept_code;

        public KategoriUserController()
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
            kategori_user_id = Session["kategori_user_id"].ToString();
            try
            {                
                if (kategori_user_id == "ADMINISTRATOR")
                {
                    var results = db.tbl_r_kategori_users.OrderBy(x => x.kategori).ToList();
                    return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var results = db.tbl_r_kategori_users.Where(x => x.kategori != "ADMINISTRATOR").OrderBy(x => x.kategori).ToList();
                    return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Get(int id)
        {
            try
            {
                var results = db.tbl_r_kategori_users.Where(x => x.id == id).FirstOrDefault();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Insert(tbl_r_kategori_user a)
        {
            try
            {
                a.ip = System.Environment.MachineName;
                a.created_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                db.tbl_r_kategori_users.InsertOnSubmit(a);
                db.SubmitChanges();
                db.Dispose();
                return Json(new { status = true, remarks = "Sukses" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Update(tbl_r_kategori_user a)
        {
            try
            {
                var tbl_ = db.tbl_r_kategori_users.Where(f => f.id == a.id).FirstOrDefault();
                if (tbl_ != null)
                {
                    tbl_.id = a.id;
                    tbl_.kategori = a.kategori;
                    tbl_.login_controller = a.login_controller;
                    tbl_.login_function = a.login_function;
                    tbl_.insert_by = a.insert_by;
                    a.ip = System.Environment.MachineName;
                    tbl_.updated_at = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    db.SubmitChanges();
                }
                db.Dispose();
                return Json(new { status = true, remarks = "Sukses" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var tbl_ = db.tbl_r_kategori_users.Where(f => f.id == id).FirstOrDefault();
                if (tbl_ != null)
                {
                    db.tbl_r_kategori_users.DeleteOnSubmit(tbl_);
                    db.SubmitChanges();
                }
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