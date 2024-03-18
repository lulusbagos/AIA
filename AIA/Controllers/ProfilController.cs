using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIA.Models;


namespace AIA.Controllers
{
    public class ProfilController : Controller
    {
        private DtClsDB_AIADataContext db;
        private string controller_name = "Display";
        private string title_name = "DISPLAY";


        // GET: Display
        public ProfilController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }


        // GET: Profil
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

        public ActionResult GetAll(string nrp)
        {
            try
            {
                var results = db.vw_profils.Where(x => x.nrp == nrp).OrderBy(x => x.tanggal).ToList();
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult GetAllFtw(String nrp)
        {
            try
            {
                var resultftw = db.vw_profils
                    .Where(x => x.nrp == nrp)
                    .OrderBy(x => x.tanggal)
                    .ToList();

                var baseUrl = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

                var lineftw = "";

                for (int i = 0; i < resultftw.Count(); i++)
                {
                    lineftw += "   <li class='list-group-item'>";
                    lineftw += "   <a class='user-list-item'>";
                    lineftw += "    <div class='user avatar-sm float-start me-2'>";

                    if (resultftw[i].created_by == "FTW 1PAMA")
                    {

                        lineftw += "<i class='fas fa-user d-flex align-items-center justify-content-center rounded-circle ' style='font-size: 40px;'></i>";
                    }
                    else if (resultftw[i].created_by == "FTW OPA")
                    {
                        lineftw += "<i class='fas fa-stopwatch d-flex align-items-center justify-content-center rounded-circle ' style='font-size: 40px;'></i>";
                    }
                    lineftw += "       </div>";
                    lineftw += "       <div class='user-desc'>";
                    lineftw += $"           <h5 class='name mt-0 mb-1' id='tanggal'>{resultftw[i].tanggal}</h5>";
                    lineftw += $"           <p class='desc text-muted mb-0 font-12' id='jamtidur'>{resultftw[i].jamtidur}</p>";
                    lineftw += "       </div>";
                    lineftw += "   </a>";
                    lineftw += " </li>";
                }

                return Json(new { status = true, data = lineftw }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}