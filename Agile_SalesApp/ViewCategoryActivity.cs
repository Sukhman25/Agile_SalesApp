using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agile_SalesApp.Adapters;
using Agile_SalesApp.DataLayer;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Agile_SalesApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class ViewCategoryActivity : AppCompatActivity
    {
        DataOperation operation;
        Button btn1;
        ListView listCategory;
        CategoryListAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.view_category_main);
            operation = new DataOperation();

            listCategory = FindViewById<ListView>(Resource.Id.listCategory);
            btn1 = FindViewById<Button>(Resource.Id.btn1);

            btn1.Click += Btn1_Click;

            adapter = new CategoryListAdapter(this, operation.GetAllCategory());
            listCategory.Adapter = adapter;
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CategoryMainActivity));
            Finish();
        }
    }
}