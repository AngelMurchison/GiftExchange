using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftExchange.Models
{
    public class GiftModel
    {
        public int Id { get; set; }

        public string Contents { get; set; }

        public string GiftHint { get; set; }

        public string ColorWrappingPaper { get; set; }

        public double? Height { get; set; }

        public double? Width { get; set; }

        public double? Depth { get; set; }

        public double? Weight { get; set; }

        public bool? isOpened { get; set; }

    }
}