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
    public class AddProductActivity : AppCompatActivity
    {
        Button btn1, btn2;
        EditText etName,etPrice;
        DataOperation operation;
        Spinner spinnerCategory;
        CategoryListAdapter adapter;
        List<Category> categories;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.add_product_main);

            operation = new DataOperation();
            categories = operation.GetAllCategory();

            etName = FindViewById<EditText>(Resource.Id.etName);
            etPrice = FindViewById<EditText>(Resource.Id.etPrice);

            spinnerCategory = FindViewById<Spinner>(Resource.Id.spinnerCategory);

            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);

            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;

            adapter = new CategoryListAdapter(this, categories);
            spinnerCategory.Adapter = adapter;
        }

        private void Btn2_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ProductMainActivity));
            Finish();
        }

        private void Btn1_Click(object sender, EventArgs e)
        {
            string productname = etName.Text.Trim();
            string productprice = etPrice.Text.Trim();
            Category category = categories[spinnerCategory.SelectedItemPosition];
            string message = "";
            if (productname.Length == 0 || productprice.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else if(Utility.IsFloat(productprice))
            {
                Product product = new Product();
                product.Price = float.Parse(productprice);
                product.ProductName = productname;
                product.CategoryID = category.CategoryID;
                if (operation.AddNewProduct(product))
                {
                    message = "New Product is Saved in System";
                }
                else
                {
                    message = operation.Message;
                }
            }
            else
            {
                message = "Invalid Format for Product Price";
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }


    }
}