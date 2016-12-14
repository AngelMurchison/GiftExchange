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

        public static List<GiftModel> Gifts = Services.Services.getAllGifts();
        public static List<GiftModel> IndexList = Gifts;

        // GET: Gift

        public ActionResult Index()
        {
            return View(IndexList);
        }
        public ActionResult Create(FormCollection collection)
        {
            string contents = (collection["contents"]);
            string gifthint = (collection["gifthint"]);
            string colorwrappingpaper = (collection["colorwrappingpaper"]);
            string height = (collection["height"]);
            string width = (collection["width"]);
            string depth = (collection["depth"]);
            string weight = (collection["weight"]);
            string isopened = (collection["isopened"]);
            if (weight != null)
            {
                double? dblheight = double.Parse(height);
                double? dblwidth = double.Parse(width);
                double? dbldepth = double.Parse(depth);
                double? dblweight = double.Parse(weight);
                bool? boolopened = bool.Parse(isopened);
               IndexList = Services.Services.addAGift(contents, gifthint, colorwrappingpaper, dblheight, dblwidth, dbldepth, dblweight, boolopened);
            }
            return View();
        }
        public ActionResult Edit(FormCollection collection)
        {
            return View();
        }
        public ActionResult Delete(FormCollection collection)
        {

            
            string contents = (collection["submit"]);
            if (contents != null)
            {
               IndexList = Services.Services.removeGift(contents);
            }
            return View();
        }
    }
}