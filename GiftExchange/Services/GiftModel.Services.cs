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

        public static List<GiftModel> Gifts = getAllGifts();

        public static List<GiftModel> addAGift(string Contents, string GiftHint, string ColorWrappingPaper, double? Height, double? Width, double? Depth, double? Weight, bool? isOpened = false)
        {
            GiftModel gifttoadd = new GiftModel
            {
                Contents = Contents,
                GiftHint = GiftHint,
                ColorWrappingPaper = ColorWrappingPaper,
                Height = Height,
                Width = Width,
                Depth = Depth,
                Weight = Weight,
                isOpened = isOpened
            };
            using (var connection = new SqlConnection(connectionStrings))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"INSERT INTO Gifts VALUES (@Contents, @GiftHint, @ColorWrappingPaper, @Height, @Width, @Depth,  @Weight, @isOpened)";
                    cmd.Parameters.AddWithValue("@Contents", gifttoadd.Contents);
                    cmd.Parameters.AddWithValue("@GiftHint", gifttoadd.GiftHint);
                    cmd.Parameters.AddWithValue("@ColorWrappingPaper", gifttoadd.ColorWrappingPaper);
                    cmd.Parameters.AddWithValue("@Height", gifttoadd.Height);
                    cmd.Parameters.AddWithValue("@Width", gifttoadd.Width);
                    cmd.Parameters.AddWithValue("@Depth", gifttoadd.Depth);
                    cmd.Parameters.AddWithValue("@Weight", gifttoadd.Weight);
                    cmd.Parameters.AddWithValue("@isOpened", gifttoadd.isOpened);


                    connection.Open();
                    var reader = cmd.ExecuteReader();
                    connection.Close();
                }
                List<GiftModel> gifts = getAllGifts();
                return gifts;
            }
        }

        public static List<GiftModel> removeGift(int id)
        {
            var ids = new List<int>();
            List<GiftModel> gifts = getAllGifts();
            foreach (var gift in gifts)
            {
                ids.Add(gift.Id);
            }

            if (ids.Contains(id))
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
            gifts = getAllGifts();
            return gifts;
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

                        var gift = new GiftModel
                        {
                            Id = (int)id,
                            GiftHint = GiftHint as string,
                            ColorWrappingPaper = ColorWrappingPaper as string,
                            Height = Height as double?,
                            Width = Width as double?,
                            Depth = Depth as double?,
                            Weight = Weight as double?,
                        };
                        rv.Add(gift);
                    }
                    connection.Close();
                }
                return rv;
            }
        }

        public static List<GiftModel> editGift(string Contents, string GiftHint, string ColorWrappingPaper, double? Height, double? Width, double? Depth, double? Weight, int id)
        {
            var ids = new List<int>();

            List<GiftModel> gifts = getAllGifts();
            foreach (var gift in gifts)
            {

                ids.Add(gift.Id);
            }

            if (ids.Contains(id))
            {
                using (var connection = new SqlConnection(connectionStrings))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = $@"UPDATE Gifts SET Contents='{Contents}', GiftHint='{GiftHint}', ColorWrappingPaper='{ColorWrappingPaper}', Height='{Height}', Width='{Width}', Depth='{Depth}', Weight='{Weight}' WHERE Id={id};";

                        connection.Open();
                        var reader = cmd.ExecuteReader();
                        connection.Close();
                    }
                }
            }
            return gifts;
        }

        public static List<GiftModel> openGift(bool? isOpened, int id)
        {
            var ids = new List<int>();

            List<GiftModel> gifts = getAllGifts();
            foreach (var gift in gifts)
            {

                ids.Add(gift.Id);
            }

            if (ids.Contains(id) && (isOpened == false))
            {
                using (var connection = new SqlConnection(connectionStrings))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = $@"UPDATE Gifts SET isOpened={1} WHERE Id={id};";

                        connection.Open();
                        var reader = cmd.ExecuteReader();
                        connection.Close();
                    }
                }
            }
            return gifts;
        }
    }
}