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
    public class EditProductActivity : AppCompatActivity
    {
        Button btn1, btn2, btn3;
        EditText etID, etName,etPrice;
        DataOperation operation;
        Spinner spinnerCategory;
        CategoryListAdapter adapter;
        List<Category> categories;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.edit_product_main);
            operation = new DataOperation();
            categories = operation.GetAllCategory();

            etID = FindViewById<EditText>(Resource.Id.etID);
            etName = FindViewById<EditText>(Resource.Id.etName);
            etPrice = FindViewById<EditText>(Resource.Id.etPrice);

            spinnerCategory = FindViewById<Spinner>(Resource.Id.spinnerCategory);

            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);
            btn3 = FindViewById<Button>(Resource.Id.btn3);


            btn1.Click += Btn1_Click;
            btn2.Click += Btn2_Click;
            btn3.Click += Btn3_Click;

            adapter = new CategoryListAdapter(this, categories);
            spinnerCategory.Adapter = adapter;

        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(ProductMainActivity));
            Finish();
        }

        // Update Product Details
        private void Btn2_Click(object sender, System.EventArgs e)
        {
            string id = etID.Text.Trim();
            string productname = etName.Text.Trim();
            string productprice = etPrice.Text.Trim();
            Category category = categories[spinnerCategory.SelectedItemPosition];
            string message = "";
            if (productname.Length == 0 || id.Length == 0 || productprice.Length == 0)
            {
                message = "Please Fill All Boxes";
            }
            else if (Utility.IsFloat(productprice))
            {
                try
                {
                    int productid = int.Parse(id);
                    Product product = operation.GetProduct(productid);
                    if(product!=null)
                    {

                        product.Price = float.Parse(productprice);
                        product.ProductName = productname;
                        product.CategoryID = category.CategoryID;
                        if (operation.UpdateProduct(product))
                        {
                            message = "Product is Saved in System";
                        }
                        else
                        {
                            message = operation.Message;
                        }
                    }
                    else
                    {
                        message = "There is No Such Product with Give Product ID";
                    }
                }
                catch(Exception ex)
                {
                    message = "Invalid Format of Product ID";
                }
            }
            else
            {
                message = "Invalid Format for Product Price";
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        // Fetch Details of Product
        private void Btn1_Click(object sender, System.EventArgs e)
        {
            string id = etID.Text.Trim();
            string message = "";
            try
            {
                int productid = int.Parse(id);
                Product product = operation.GetProduct(productid);
                if (product != null)
                {
                    etName.Text = product.ProductName;
                    etPrice.Text = product.Price.ToString();
                    int index = 0;
                    for (int i = 0; i < categories.Count(); i++)
                    {
                        if (categories[i].CategoryID == product.CategoryID)
                        {
                            index = i;
                            break;
                        }
                    }
                    spinnerCategory.SetSelection(index);
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