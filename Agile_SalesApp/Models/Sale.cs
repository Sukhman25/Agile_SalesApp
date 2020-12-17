using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Agile_SalesApp.Models
{
    public class Sale
    {
        [PrimaryKey, AutoIncrement]
        public int SaleID { get; set; }

        public int ProductID { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public DateTime SaleDate { get; set; }
    }
}