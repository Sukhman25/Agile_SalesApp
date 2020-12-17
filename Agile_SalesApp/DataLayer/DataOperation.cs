using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agile_SalesApp.Models;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Agile_SalesApp.DataLayer
{
    public class DataOperation
    {
        private SQLiteConnection connection;

        public string Message { get; set; }

        public DataOperation()
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            connection = new SQLiteConnection(Path.Combine(path, "sales.db"));
            if(!IsCategoryTableExists())
            {
                connection.CreateTable<Category>();
                connection.CreateTable<Product>();
                connection.CreateTable<Sale>();
            }
        }

        public bool AddNewCategory(Category category)
        {
            try
            {
                connection.Insert(category);
                return true;
            }
            catch(Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public bool AddNewProduct(Product product)
        {
            try
            {
                connection.Insert(product);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public bool AddSaleEntry(Sale sale)
        {
            try
            {
                connection.Insert(sale);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public bool UpdateCategory(Category category)
        {
            try
            {
                connection.Update(category);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                connection.Update(product);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public bool DeleteProduct(Product product)
        {
            try
            {
                connection.Delete(product);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public bool DeleteSale(Sale sale)
        {
            try
            {
                connection.Delete(sale);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                connection.Delete(category);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return false;
            }
        }

        public List<Category> GetAllCategory()
        {
            List<Category> categories = connection.Query<Category>("Select * from Category");
            return categories;
        }

        public List<Product> GetAllProduct()
        {
            List<Product> products = connection.Query<Product>("Select * from Product");
            return products;
        }

        public List<Sale> GetAllSale()
        {
            List<Sale> sales = connection.Query<Sale>("Select * from Sale Order By SaleID Desc");
            return sales;
        }

        public Category GetCategory(int categoryid)
        {
            List<Category> categories = GetAllCategory();
            Category category = null;
            foreach (Category cat in categories)
            {
                if (cat.CategoryID == categoryid)
                {
                    category = cat;
                    break;
                }
            }
            return category;
        }

        public Product GetProduct(int productid)
        {
            List<Product> products = GetAllProduct();
            Product product = null;
            foreach (Product prd in products)
            {
                if (prd.ProductID == productid)
                {
                    product = prd;
                    break;
                }
            }
            return product;
        }

        public Sale GetSale(int saleid)
        {
            List<Sale> sales = GetAllSale();
            Sale result = null;
            foreach (Sale sale in sales)
            {
                if (sale.SaleID == saleid)
                {
                    result = sale;
                    break;
                }
            }
            return result;
        }

        public bool CheckCategoryProduct(int categoryid)
        {
            List<Product> products = GetAllProduct();
            foreach (Product product in products)
            {
                if (product.CategoryID == categoryid)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckProductSale(int productid)
        {
            List<Sale> sales = GetAllSale();
            foreach (Sale sale in sales)
            {
                if (sale.ProductID == productid)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckValidCategoryID(int categoryid)
        {
            List<Category> categories = GetAllCategory();
            foreach (Category category in categories)
            {
                if (category.CategoryID == categoryid)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCategoryTableExists()
        {
            try
            {
                connection.Get<Category>(1);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}