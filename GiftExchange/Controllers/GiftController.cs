using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GiftExchange.Models;
using System.IO;
using System.Data.SqlClient;

namespace GiftExchange.Controllers
{
    
    public class GiftController : Controller
    {
        private static string connectionStrings = @"Server=DESKTOP-577TSME\SQLEXPRESS;Database=GiftExchangeDB;Trusted_Connection=True;";

        public static List<GiftModel> getAllGifts()
        {

            var rv = new List<GiftModel>();
            using (var connection = new SqlConnection(connectionStrings))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT Id, Contents, GiftHint, ColorWrappingPaper, Height, Width, Depth, Weight, isOpened FROM Gifts";

                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = reader["Id"];
                        var Contents = reader[1];
                        var GiftHint = reader[2];
                        var ColorWrappingPaper = reader[3];
                        var Height = reader[4];
                        var Width = reader[5];
                        var Depth = reader[6];
                        var Weight = reader[7];
                        var isOpened = reader[8];

                        var gift = new GiftModel
                        {
                            Id = (int)id,
                            Contents = Contents as string,
                            GiftHint = GiftHint as string,
                            ColorWrappingPaper = ColorWrappingPaper as string,
                            Height = Height as double?,
                            Width = Width as double?,
                            Depth = Depth as double?,
                            Weight = Weight as double?,
                            isOpened = isOpened as bool?
                        };
                        rv.Add(gift);
                    }
                    connection.Close();
                }
                return rv;
            }
        }

        List<GiftModel> Gifts = getAllGifts();

        // GET: Gift

        public ActionResult Index()
        {
            return View(Gifts);
        }
    }
}