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
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Agile_SalesApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class AddCategoryActivity : AppCompatActivity
    {
        Button btn1, btn2;
        EditText etName;
        DataOperation operation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.add_category_main);
            operation = new DataOperation();

            etName = FindViewById<EditText>(Resource.Id.etName);

            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);

            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CategoryMainActivity));
            Finish();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            string categoryname = etName.Text.Trim();
            string message = "";
            if (categoryname.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                Category category = new Category();
                category.CategoryName = categoryname;
                if (operation.AddNewCategory(category))
                {
                    message = "New Category is Saved in System";
                }
                else
                {
                    message = operation.Message;
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }
    }
}