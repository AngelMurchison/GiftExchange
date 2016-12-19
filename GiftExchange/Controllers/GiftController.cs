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
            //var gifttoadd = new GiftModel
            //{
            //    Id = int.Parse(collection["Id"]),
            //    Contents = (collection["contents"]),
            //    GiftHint = (collection["gifthint"]),
            //    ColorWrappingPaper = (collection["colorwrappingpaper"]),
            //    Height = double.Parse(collection["height"]),
            //    Width = double.Parse(collection["width"]),
            //    Depth = double.Parse(collection["depth"]),
            //    Weight = double.Parse(collection["weight"]),
            //    isOpened = bool.Parse(collection["isopened"])
            //};
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
            //var gifttoedit = new GiftModel
            //{
            //    Id = int.Parse(collection["Id"]),
            //    Contents = (collection["contents"]),
            //    GiftHint = (collection["gifthint"]),
            //    ColorWrappingPaper = (collection["colorwrappingpaper"]),
            //    Height = double.Parse(collection["height"]),
            //    Width = double.Parse(collection["width"]),
            //    Depth = double.Parse(collection["depth"]),
            //    Weight = double.Parse(collection["weight"]),
            //    isOpened = bool.Parse(collection["isopened"])
            //};
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
            //var gifttoremove = new GiftModel
            //{
            //    Id = int.Parse(collection["Id"]),
            //    Contents = (collection["contents"]),
            //    GiftHint = (collection["gifthint"]),
            //    ColorWrappingPaper = (collection["colorwrappingpaper"]),
            //    Height = double.Parse(collection["height"]),
            //    Width = double.Parse(collection["width"]),
            //    Depth = double.Parse(collection["depth"]),
            //    Weight = double.Parse(collection["weight"]),
            //    isOpened = bool.Parse(collection["isopened"])
            //};
            Services.Services.removeGift(id);
            return RedirectToAction("Index");
        }

        // GET
        public ActionResult Unopened()
        {
            List<GiftModel> UnopenedList = Services.Services.getUnopenedGifts();
            return View(UnopenedList);
        }

        //POST
        public ActionResult Open(int id)
        {
            GiftModel gift = Services.Services.getAGift(id);
            return View(gift);
        }
        public ActionResult confirmToOpen(int id)
        {
            //var gifttoopen = new GiftModel
            //{
            //    Id = int.Parse(collection["Id"]),
            //    Contents = (collection["contents"]),
            //    GiftHint = (collection["gifthint"]),
            //    ColorWrappingPaper = (collection["colorwrappingpaper"]),
            //    Height = double.Parse(collection["height"]),
            //    Width = double.Parse(collection["width"]),
            //    Depth = double.Parse(collection["depth"]),
            //    Weight = double.Parse(collection["weight"]),
            //    isOpened = bool.Parse(collection["isopened"])
            //};
            var gifttoopen = Services.Services.getAGift(id);
            Services.Services.openGift(gifttoopen);
            return RedirectToAction("Index");
        }

    }
}