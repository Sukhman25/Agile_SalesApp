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
    public class DeleteSaleActivity : AppCompatActivity
    {

        Button btn1, btn2;
        EditText etID;
        DataOperation operation;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.delete_sale_main);
            operation = new DataOperation();

            etID = FindViewById<EditText>(Resource.Id.etID);

            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);

            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SaleMainActivity));
            Finish();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            string id = etID.Text.Trim();
            string message = "";
            try
            {
                int saleid = int.Parse(id);
                Sale sale = operation.GetSale(saleid);
                if (sale != null)
                {
                        Android.Support.V7.App.AlertDialog.Builder winBuild = new Android.Support.V7.App.AlertDialog.Builder(this);
                        winBuild.SetTitle("Confirmation!!!");
                        winBuild.SetMessage("Are You Sure to Remove This Sale Entry");
                        winBuild.SetPositiveButton("Delete Record", (c, v) =>
                        {
                            if (operation.DeleteSale(sale))
                            {
                                message = "Sale Entry is Removed From Database";
                            }
                            else
                            {
                                message = "There is Problem in Delete the Sale Entry";
                            }
                            Toast.MakeText(this, message, ToastLength.Long).Show();
                        });
                        winBuild.SetNegativeButton("Exit", (c, v) =>
                        {
                            winBuild.Dispose();
                        });
                        winBuild.Show();
                    
                }
                else
                {
                    message = "There is no such Sale Entry Details For Given Invoice ID";
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