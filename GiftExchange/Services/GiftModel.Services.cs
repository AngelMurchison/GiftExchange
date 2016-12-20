using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GiftExchange.Models;
using System.Data.SqlClient;

namespace GiftExchange.Services
{
    public class Services
    {
        private static string connectionStrings = @"Server=DESKTOP-577TSME\SQLEXPRESS;Database=GiftExchangeDB;Trusted_Connection=True;";

        public static double parseSqlDouble(object measurement)
        {
            double rv = 0;
            double.TryParse(measurement.ToString(), out rv);
            return rv;

        }

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

                        var dblheight = parseSqlDouble(Height);
                        var dblweight = parseSqlDouble(Weight);
                        var dblwidth = parseSqlDouble(Width);
                        var dbldepth = parseSqlDouble(Depth);
                        var gift = new GiftModel
                        {
                            Id = (int)id,
                            Contents = Contents as string,
                            GiftHint = GiftHint as string,
                            ColorWrappingPaper = ColorWrappingPaper as string,
                            Height = dblheight,
                            Width = dblwidth,
                            Depth = dbldepth,
                            Weight = dblweight,
                            isOpened = isOpened as bool?
                        };
                        rv.Add(gift);
                    }
                    connection.Close();
                }
                return rv;
            }
        }

        public static List<GiftModel> getUnopenedGifts()
        {

            var rv = new List<GiftModel>();
            using (var connection = new SqlConnection(connectionStrings))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT Id, GiftHint, ColorWrappingPaper, Height, Width, Depth, Weight FROM Gifts WHERE isOpened = 0";

                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = reader["Id"];
                        var GiftHint = reader[1];
                        var ColorWrappingPaper = reader[2];
                        var Height = reader[3];
                        var Width = reader[4];
                        var Depth = reader[5];
                        var Weight = reader[6];

                        var dblheight = parseSqlDouble(Height);
                        var dblweight = parseSqlDouble(Weight);
                        var dblwidth= parseSqlDouble(Width);
                        var dbldepth = parseSqlDouble(Depth);

                        var gift = new GiftModel
                        {
                            Id = (int)id,
                            GiftHint = GiftHint as string,
                            ColorWrappingPaper = ColorWrappingPaper as string,
                            Height = dblheight,
                            Width = dblwidth,
                            Depth = dbldepth,
                            Weight = dblweight,
                        };
                        rv.Add(gift);
                    }
                    connection.Close();
                }
                return rv;
            }
        }

        public static void addAGift(GiftModel gift)
        {
            using (var connection = new SqlConnection(connectionStrings))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"INSERT INTO Gifts VALUES (@Contents, @GiftHint, @ColorWrappingPaper, @Height, @Width, @Depth,  @Weight, @isOpened)";
                    cmd.Parameters.AddWithValue("@Contents", gift.Contents);
                    cmd.Parameters.AddWithValue("@GiftHint", gift.GiftHint);
                    cmd.Parameters.AddWithValue("@ColorWrappingPaper", gift.ColorWrappingPaper);
                    cmd.Parameters.AddWithValue("@Height", gift.Height);
                    cmd.Parameters.AddWithValue("@Width", gift.Width);
                    cmd.Parameters.AddWithValue("@Depth", gift.Depth);
                    cmd.Parameters.AddWithValue("@Weight", gift.Weight);
                    cmd.Parameters.AddWithValue("@isOpened", gift.isOpened);


                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    connection.Close();
                }
            }
        }

        public static void removeGift(int id)
        {
            {
                using (var connection = new SqlConnection(connectionStrings))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = $@"DELETE FROM Gifts WHERE id={id}";


                        connection.Open();
                        var reader = cmd.ExecuteReader();
                        connection.Close();
                    }
                }
            }
        }

        public static void editGift(GiftModel gift)
        {
            using (var connection = new SqlConnection(connectionStrings))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $@"UPDATE Gifts SET Contents='{gift.Contents}', GiftHint='{gift.GiftHint}', ColorWrappingPaper='{gift.ColorWrappingPaper}', Height='{gift.Height}', Width='{gift.Width}', Depth='{gift.Depth}', Weight='{gift.Weight}' WHERE Id={gift.Id};";

                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    connection.Close();
                }
            }
        }

        public static void openGift(GiftModel gift)
        {
            using (var connection = new SqlConnection(connectionStrings))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $@"UPDATE Gifts SET isOpened=1 WHERE Id={gift.Id};";

                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    connection.Close();
                }
            }
        }

        public static GiftModel getAGift(int id)
        {

            var rv = new GiftModel();
            using (var connection = new SqlConnection(connectionStrings))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = $@"SELECT Contents, GiftHint, ColorWrappingPaper, Height, Width, Depth, Weight, isOpened FROM Gifts Where Id={id}";

                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var Contents = reader[0];
                        var GiftHint = reader[1];
                        var ColorWrappingPaper = reader[2];
                        var Height = reader[3];
                        var Width = reader[4];
                        var Depth = reader[5];
                        var Weight = reader[6];
                        var isOpened = reader[7];

                        var dblheight = parseSqlDouble(Height);
                        var dblweight = parseSqlDouble(Weight);
                        var dblwidth = parseSqlDouble(Width);
                        var dbldepth = parseSqlDouble(Depth);

                        var gift = new GiftModel
                        {
                            Id = (int)id,
                            Contents = Contents as string,
                            GiftHint = GiftHint as string,
                            ColorWrappingPaper = ColorWrappingPaper as string,
                            Height = dblheight,
                            Width = dblwidth,
                            Depth = dbldepth,
                            Weight = dblweight,
                            isOpened = isOpened as bool?
                        };
                        rv = gift;
                    }
                    connection.Close();
                }
                return rv;
            }
        }
    }
}


        