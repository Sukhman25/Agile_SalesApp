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
    public class EditCategoryActivity : AppCompatActivity
    {

        Button btn1, btn2, btn3;
        EditText etID, etName;
        DataOperation operation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.edit_category_main);
            operation = new DataOperation();

            etID = FindViewById<EditText>(Resource.Id.etID);
            etName = FindViewById<EditText>(Resource.Id.etName);

            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);
            btn3 = FindViewById<Button>(Resource.Id.btn3);


            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;
            btn3.Click += Btn3_Click;
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(CategoryMainActivity));
            Finish();
        }

        // Update Category Details
        private void Btn2_Click(object sender, System.EventArgs e)
        {
            string id = etID.Text.Trim();
            string name = etName.Text.Trim();
            string message = "";
            try
            {
                if (id.Length == 0 || name.Length == 0)
                {
                    message = "Please Fill All Boxes";
                }
                else
                {
                    int categoryid = int.Parse(id);
                    Category category = operation.GetCategory(categoryid);
                    if (category != null)
                    {
                        category.CategoryName = name;
                        if(operation.UpdateCategory(category))
                        {
                            message = "Category Details is Updated";
                        }
                        else
                        {
                            message = "Updation Failure: " + operation.Message;
                        }
                    }
                    else
                    {
                        message = "There is no such Category Details For Given Category ID";
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Invalid Form of Category ID Given";
            }
            if (message.Length != 0)
            {
                Toast.MakeText(this, message, ToastLength.Long).Show();
            }
        }

        // Fetch Details of Category
        private void Btn1_Click(object sender, System.EventArgs e)
        {
            string id = etID.Text.Trim();
            string message = "";
            try
            {
                int categoryid = int.Parse(id);
                Category category = operation.GetCategory(categoryid);
                if (category != null)
                {
                    etName.Text = category.CategoryName;
                }
                else
                {
                    message = "There is no such Category Details For Given Category ID";
                }
            }
            catch (Exception ex)
            {
                message = "Invalid Form of Category ID Given";
            }
            if (message.Length != 0)
            {
                Toast.MakeText(this, message, ToastLength.Long).Show();
            }
        }
    }
}