using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AIA.Models;
using System.DirectoryServices;

namespace AIA.Controllers
{
    public class LoginController : Controller
    {
        private DtClsDB_AIADataContext db;
        public LoginController()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }


        // GET: Login
        public ActionResult Index()
        {
            if (Session["is_login"] != null)
            {
                var kategori_user_id = Session["kategori_user_id"];
                var cek_kategori_user_id = db.tbl_r_kategori_users.Where(x => x.kategori == kategori_user_id).FirstOrDefault();

                return RedirectToAction(cek_kategori_user_id.login_function, cek_kategori_user_id.login_controller);
            }
            else
            {
                ViewBag.Setting = db.tbl_m_setting_aplikasis.FirstOrDefault();
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult ProsesLogin(String pnrp, String password, String ip)
        {

            //Cek apakah nrp tsb ada di db pamapersada.net
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://pamapersada.net", pnrp, password, AuthenticationTypes.Secure);
                DirectorySearcher searcher = new DirectorySearcher(entry);
                searcher.SearchScope = SearchScope.OneLevel;

                SearchResult Results = searcher.FindOne();

                //Jika tidak ada nrp tsb di db pamapersada.net
                if (Results == null)
                {
                    return Json(new { status = false, remarks = "Login gagal. Username/password salah" }, JsonRequestBehavior.AllowGet);
                }

                //Jika ada nrp tsb di db pamapersada.net
                else
                {
                    Session["pnrp"] = pnrp;
                    var nrp = pnrp.Substring(1);

                    //Cek ada kah nrp tsb di tbl user login
                    try
                    {
                        var a = db.tbl_m_user_logins.Where(x => x.nrp == nrp).FirstOrDefault<tbl_m_user_login>();

                        Session["is_login"] = true;
                        Session["nrp"] = a.nrp;
                        Session["nama"] = a.nama;
                        Session["dept"] = a.dept_code;
                        Session["ip"] = ip;

                        var kategori_user = db.vw_t_user_kategoris.Where(x => x.nrp == a.nrp).ToList();

                        return Json(new { status = true, remarks = "Login Sukses: " + a.kategori_user_id, data = kategori_user }, JsonRequestBehavior.AllowGet);
                    }

                    //Jika tidak ada
                    catch (Exception ex)
                    {
                        return Json(new { status = false, remarks = ex }, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            //Jika tidak ada di db pamapersada.net (sebagai user)
            catch (Exception e)
            {
                try
                {
                    var a = db.tbl_m_user_logins.Where(x => x.nrp == pnrp).Where(x => x.password == password).FirstOrDefault<tbl_m_user_login>();
                    Session["is_login"] = true;
                    Session["nrp"] = a.nrp;
                    Session["nama"] = a.nama;
                    Session["ip"] = ip;

                    var kategori_user = db.tbl_m_user_logins.Where(x => x.nrp == a.nrp).ToList();

                    return Json(new { status = true, remarks = "Login Sukses: " + a.kategori_user_id, data = kategori_user }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
                }
            }

        }

        [HttpGet]
        public ActionResult CekKategoriUser(String kategori_user_id)
        {
            try
            {
                var results = db.tbl_r_kategori_users.Where(x => x.kategori == kategori_user_id).FirstOrDefault();
                Session["kategori_user_id"] = results.kategori;
                return Json(new { status = true, remarks = "Sukses", data = results }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetSessionData()
        {
            var sessionData = new
            {
                isLogin = Session["is_login"],
                nrp = Session["nrp"],
                nama = Session["nama"],
                dept = Session["dept"],
                ip = Session["ip"],

            };

            return Json(sessionData, JsonRequestBehavior.AllowGet);
        }

    }
}