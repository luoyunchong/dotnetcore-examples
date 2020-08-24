using System;
using System.Collections.Generic;
using System.Text;

namespace app.Shared
{

    public class Rootobject
    {
        public Data data { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }

    public class Data
    {
        public int timeOffset { get; set; }
        public Prod[] prods { get; set; }
    }

    public class Prod
    {
        public int ProductId { get; set; }
        public string BarCode { get; set; }
        public string UserCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public int CategoryID { get; set; }
        public float QuantityPerUnit { get; set; }
        public int QuantityPerUnit2nd { get; set; }
        public decimal UnitsInStock { get; set; }
        public object DtoStartDate { get; set; }
        public object Remark { get; set; }
        public string Image { get; set; }
    }

}
