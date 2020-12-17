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
    public class DeleteProductActivity : AppCompatActivity
    {
        Button btn1, btn2;
        EditText etID;
        DataOperation operation;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.delete_product_main);
            operation = new DataOperation();

            etID = FindViewById<EditText>(Resource.Id.etID);

            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);

            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ProductMainActivity));
            Finish();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            string id = etID.Text.Trim();
            string message = "";
            try
            {
                int productid = int.Parse(id);
                Product product = operation.GetProduct(productid);
                if (product != null)
                {
                    if (operation.CheckProductSale(product.ProductID))
                    {
                        Android.Support.V7.App.AlertDialog.Builder winBuild = new Android.Support.V7.App.AlertDialog.Builder(this);
                        winBuild.SetTitle("Message!!!");
                        winBuild.SetMessage("There are some Sale Entries For This Product. So This Product is not suitable for Delete");
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
                        winBuild.SetMessage("Are You Sure to Remove This Record with Product Name: " + product.ProductName);
                        winBuild.SetPositiveButton("Delete Record", (c, v) =>
                        {
                            if (operation.DeleteProduct(product))
                            {
                                message = "Product Details is Removed From Database";
                            }
                            else
                            {
                                message = "There is Problem in Delete the Product";
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
                    message = "There is no such Product Details For Given Product ID";
                }
            }
            catch (Exception ex)
            {
                message = "Invalid Form of Product ID Given";
            }
            if (message.Length != 0)
            {
                Toast.MakeText(this, message, ToastLength.Long).Show();
            }

        }
    }
}