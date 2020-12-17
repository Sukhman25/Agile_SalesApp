using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Agile_SalesApp.Adapters;
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
    public class AddSaleActivity : AppCompatActivity
    {
        Button btn1, btn2;
        EditText etName, etQuantity;
        DataOperation operation;
        Spinner spinnerProduct;
        ProductSpinnerAdapter adapter;
        List<Product> products;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.add_sale_main);

            operation = new DataOperation();
            products = operation.GetAllProduct();

            etName = FindViewById<EditText>(Resource.Id.etName);
            etQuantity = FindViewById<EditText>(Resource.Id.etQuantity);

            spinnerProduct = FindViewById<Spinner>(Resource.Id.spinnerProduct);

            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);

            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;

            adapter = new ProductSpinnerAdapter(this, products);
            spinnerProduct.Adapter = adapter;
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SaleMainActivity));
            Finish();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            string name = etName.Text.Trim();
            string qty = etQuantity.Text.Trim();
            Product product = products[spinnerProduct.SelectedItemPosition];
            string message = "";
            if (name.Length == 0 || qty.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else 
            {
                try
                {
                    int quantity = int.Parse(qty);
                    Sale sale = new Sale();
                    sale.ProductID = product.ProductID;
                    sale.Quantity = quantity;
                    sale.Name = name;
                    sale.SaleDate = DateTime.Now;
                    if (operation.AddSaleEntry(sale))
                    {
                        message = "New Sale Entry is Saved in System";
                    }
                    else
                    {
                        message = operation.Message;
                    }
                }
                catch(Exception ex)
                {
                    message = "Invalid Quantity Given";
                }
                
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

    }
}