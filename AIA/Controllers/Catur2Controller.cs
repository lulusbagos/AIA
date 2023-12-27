using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using AIA.Models;

namespace AIA.Controllers
{
    public class Catur2Controller : Controller
    {

        private DtClsDB_AIADataContext db;
        private string controller_name = "Catur2";
        private string title_name = "Catur2";


        public Catur2Controller()
        {
            // Mengambil koneksi dari konfigurasi web
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB_AIAConnectionString"].ConnectionString;
            db = new DtClsDB_AIADataContext(connectionString);
        }

        // GET: Catur2
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


        [HttpGet]
        public ActionResult GetLoader(String sektor)
        {
            try
            {
                var resultLoader = db.ASM_TBL_UNIT_NOOPTs.OrderBy(x => x.CN_UNIT).ToList();
                var lineLoader = "";

                for (int i = 0; i < resultLoader.Count(); i++)
                {
                    lineLoader += "<div class='loader' style = 'width: 200px; margin-left: 10px; margin-right: 10px;'>";
                    lineLoader += " <div class='card bg-light text-black shadow'>";
                    lineLoader += "    <div class='btn btn-lg btn-secondary' id='cn_unit'> ";
                    lineLoader += "        <div class='row'>";
                    lineLoader += "            <div class='col-6 d-flex align-items-center'>";
                    lineLoader += "                <b style = 'font-size: 16pt;'>" + resultLoader[i].Pengganti + "</b>";
                    lineLoader += "            </div >";
                    lineLoader += "            <div class='col-6'>";
                    lineLoader += "                <a href='#' class='btn btn-success btn-circle btn-sm'>";
                    lineLoader += "                    S";
                    lineLoader += "                </a>";
                    lineLoader += "                <a href='#' class='btn btn-success btn-circle btn-sm'>";
                    lineLoader += "                    V";
                    lineLoader += "                </a>";
                    lineLoader += "            </div>";
                    lineLoader += "        </div>";
                    lineLoader += "    </div>";
                    lineLoader += "  </div>";
                    lineLoader += "</div> ";
                }
                return Json(new { status = true, data = lineLoader }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public ActionResult GetHaulerNoSetting()
        {
            try
            {
                var resultUnit = db.ASM_TBL_UNIT_NOOPTs.OrderBy(x => x.CN_UNIT).ToList();
                var lineUnit = "";

                for (int i = 0; i < resultUnit.Count(); i++)
                {
                    lineUnit += "<div id='" + resultUnit[i].CN_UNIT + "' class='hauler' style='margin-bottom: 5px;' draggable='true' ondragstart='onDragStart(event);'>";
                    lineUnit += " <div class='card bg-light text-black shadow'>";
                    lineUnit += "    <div class='card-body'>";
                    lineUnit += "        <div class='row'>";
                    lineUnit += "            <div class='col-6 d-flex align-items-center'>";
                    lineUnit += "                <b style = 'font-size: 16pt;'>" + resultUnit[i].CN_UNIT + "</b>";
                    lineUnit += "            </div >";
                    lineUnit += "            <div class='col-6'>";
                    lineUnit += "                <a href='#' class='btn btn-success btn-circle btn-sm'>";
                    lineUnit += "                    S";
                    lineUnit += "                </a>";
                    lineUnit += "                <a href='#' class='btn btn-success btn-circle btn-sm'>";
                    lineUnit += "                    V";
                    lineUnit += "                </a>";
                    lineUnit += "            </div>";
                    lineUnit += "        </div>";
                    lineUnit += "    </div>";
                    lineUnit += "  </div>";
                    lineUnit += "</div> ";
                }
                return Json(new { status = true, data = lineUnit }, JsonRequestBehavior.AllowGet);
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
                var resultLoader = db.ASM_TBL_UNIT_NOOPTs.OrderBy(x => x.CN_UNIT).ToList();
                var lineLoader = "";
                var lineHauler = "";

                for (int a = 0; a < resultLoader.Count(); a++)
                {
                    var resultHauler = db.ASM_TBL_UNIT_NOOPTs.OrderBy(x => x.CN_UNIT).ToList();

                    lineHauler += "<div id='" + resultLoader[a].NRP + "' class='hauler' style = 'width: 200px; margin-left: 10px; margin-right: 10px;padding-bottom:100px;' ondragover='onDragOver(event);' ondrop = 'onDrop(event);' > ";

                    for (int i = 0; i < resultHauler.Count(); i++)
                    {
                        lineHauler += "<div id='" + resultHauler[i].CN_UNIT + "' class='hauler' style='margin-bottom: 5px;' draggable='true' ondragstart='onDragStart(event);' >";
                        lineHauler += " <div class='card bg-light text-black shadow'>";
                        lineHauler += "    <div class='card-body'>";
                        lineHauler += "        <div class='row'>";
                        lineHauler += "            <div class='col-6 d-flex align-items-center'>";
                        lineHauler += "                <b style = 'font-size: 16pt;'>" + resultHauler[i].CN_UNIT + "</b>";
                        lineHauler += "            </div >";
                        lineHauler += "            <div class='col-6'>";
                        lineHauler += "                <a href='#' class='btn btn-success btn-circle btn-sm'>";
                        lineHauler += "                    S";
                        lineHauler += "                </a>";
                        lineHauler += "        </div>";
                        lineHauler += "    </div>";
                        lineHauler += "  </div>";
                        lineHauler += "</div> ";
                    }
                    lineHauler += "</div >";
                }
                return Json(new { status = true, data = lineHauler }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, remarks = "Gagal", data = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public ActionResult Insert2(tbl_t_setting_fleet a)
        //{
        //    try
        //    {

        //        db.tbl_t_setting_fleets.InsertOnSubmit(a);
        //        db.SubmitChanges();
        //        db.Dispose();
        //        return Json(new { status = true, remarks = "Sukses" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception e)
        //    {
        //        return Json(new { status = false, remarks = "Gagal", data = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
        //    }
        //}

    }
}