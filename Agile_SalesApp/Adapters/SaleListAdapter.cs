using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agile_SalesApp.DataLayer;
using Agile_SalesApp.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Agile_SalesApp.Adapters
{
    public class SaleListAdapter : BaseAdapter<Sale>
    {
        private readonly Activity context;
        private readonly List<Sale> datas;
        private DataOperation operation;
        public SaleListAdapter(Activity context, List<Sale> datas)
        {
            this.datas = datas;
            this.context = context;
            operation = new DataOperation();
        }

        public override int Count
        {
            get { return datas.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Sale this[int position]
        {
            get { return datas[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.sale_row, null, false);
            }

            TextView text1 = row.FindViewById<TextView>(Resource.Id.text1);
            TextView text2 = row.FindViewById<TextView>(Resource.Id.text2);
            TextView text3 = row.FindViewById<TextView>(Resource.Id.text3);
            TextView text4 = row.FindViewById<TextView>(Resource.Id.text4);
            TextView text5 = row.FindViewById<TextView>(Resource.Id.text5);

            Sale sale = datas[position];
            Product product = operation.GetProduct(sale.ProductID);
            text1.Text = "Invoice ID: " + sale.SaleID;
            text2.Text = "Invoice Date: " + sale.SaleDate;
            text3.Text = product.ProductName;
            text4.Text = "Buyer: " + sale.Name;
            text5.Text = "Total: $ " + (product.Price * sale.Quantity);
            return row;
        }
    }
}