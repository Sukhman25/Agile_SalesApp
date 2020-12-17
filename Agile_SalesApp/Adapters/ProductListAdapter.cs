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
    public class ProductListAdapter : BaseAdapter<Product>
    {
        private readonly Activity context;
        private readonly List<Product> datas;
        private DataOperation operation;
        public ProductListAdapter(Activity context, List<Product> datas)
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

        public override Product this[int position]
        {
            get { return datas[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.product_row, null, false);
            }

            TextView text1 = row.FindViewById<TextView>(Resource.Id.text1);
            TextView text2 = row.FindViewById<TextView>(Resource.Id.text2);
            TextView text3 = row.FindViewById<TextView>(Resource.Id.text3);

            Product product = datas[position];
            text1.Text = product.ProductName + " { " + product.ProductID + " }";
            Category category = operation.GetCategory(product.CategoryID);
            text2.Text = category.CategoryName;
            text3.Text = "$ " + product.Price;
            return row;
        }
    }
}