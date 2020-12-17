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
    public class DeleteCategoryActivity : AppCompatActivity
    {
        Button btn1, btn2;
        EditText etID;
        DataOperation operation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.delete_category_main);
            operation = new DataOperation();

            etID = FindViewById<EditText>(Resource.Id.etID);

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
            string id = etID.Text.Trim();
            string message = "";
            try
            {
                int categoryid = int.Parse(id);
                Category category = operation.GetCategory(categoryid);
                if (category != null)
                {
                    if (operation.CheckCategoryProduct(category.CategoryID))
                    {
                        Android.Support.V7.App.AlertDialog.Builder winBuild = new Android.Support.V7.App.AlertDialog.Builder(this);
                        winBuild.SetTitle("Message!!!");
                        winBuild.SetMessage("There are some Product associated with Given Category ID. So This Category is not suitable for Delete");
                        winBuild.SetNegativeButton("Close", (c, v) =>
                        {
                            winBuild.Dispose();
                        });
                        winBuild.Show();
                    }
                    else
                    {
                        Android.Support.V7.App.AlertDialog.Builder winBuild = new Android.Support.V7.App.AlertDialog.Builder(this);
                        winBuild.SetTitle("Confirmation!!!");
                        winBuild.SetMessage("Are You Sure to Remove This Record with Text: " + category.CategoryName);
                        winBuild.SetPositiveButton("Delete Record", (c, v) =>
                        {
                            if (operation.DeleteCategory(category))
                            {
                                message = "Category Details is Removed From Database";
                            }
                            else
                            {
                                message = "There is Problem in Delete the Category";
                            }
                            Toast.MakeText(this, message, ToastLength.Long).Show();
                        });
                        winBuild.SetNegativeButton("Exit", (c, v) =>
                        {
                            winBuild.Dispose();
                        });
                        winBuild.Show();
                    }
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