using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftExchange.Models;
using GiftExchange.Services;
using System.IO;
using System.Data.SqlClient;

namespace GiftExchange.Controllers
{

    public class GiftController : Controller
    {

        public static List<GiftModel> IndexList = new List<GiftModel>();


        // GET
        public ActionResult Index()
        {
            List<GiftModel> IndexList = Services.Services.getAllGifts();
            return View(IndexList);
        }

        //PUT
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult giftCreation (GiftModel gifttoadd)
        {
            Services.Services.addAGift(gifttoadd);
            return RedirectToAction("Index");
        }

        // POST
        public ActionResult Edit(int id)
        {
            GiftModel gift = Services.Services.getAGift(id);
            return View(gift);
        }      
        public ActionResult giftEdit(GiftModel gifttoedit)
        {
            Services.Services.editGift(gifttoedit);
            return RedirectToAction("Index");
        }

        // DELETE
        public ActionResult Delete(int id)
        {
            GiftModel gift = Services.Services.getAGift(id);
            return View(gift);
        }
        public ActionResult giftDeletion(int id)
        {
            Services.Services.removeGift(id);
            return RedirectToAction("Index");
        }

        //POST
        public ActionResult Open(int id)
        {
            GiftModel gift = Services.Services.getAGift(id);
            return View(gift);
        }
        public ActionResult confirmToOpen(int id)
        {
            var gifttoopen = Services.Services.getAGift(id);
            if (gifttoopen.isOpened == false)
            {
                Services.Services.openGift(gifttoopen);
                return View(gifttoopen);
            }
            else
            {
                return View(gifttoopen);
            }
        }
    }
}