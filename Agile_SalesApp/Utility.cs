using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Agile_SalesApp
{
    public class Utility
    {
        public static bool IsFloat(string value)
        {
            try
            {
                float.Parse(value.Trim());
                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }
    }
}