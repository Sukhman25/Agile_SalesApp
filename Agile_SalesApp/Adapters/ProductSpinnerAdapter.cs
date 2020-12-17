using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agile_SalesApp.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Agile_SalesApp.Adapters
{
    public class ProductSpinnerAdapter : BaseAdapter<Product>
    {
        private readonly Activity context;
        private readonly List<Product> datas;
        public ProductSpinnerAdapter(Activity context, List<Product> datas)
        {
            this.datas = datas;
            this.context = context;
            
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
                row = LayoutInflater.From(context).Inflate(Resource.Layout.category_row, null, false);
            }

            TextView text = row.FindViewById<TextView>(Resource.Id.textCategory);

            Product product = datas[position];
            text.Text = product.ProductName + " { " + product.ProductID + " }";

            return row;
        }
    }
}