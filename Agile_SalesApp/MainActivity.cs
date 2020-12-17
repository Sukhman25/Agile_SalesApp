using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace Agile_SalesApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        Button btn1, btn2, btn3;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);
            btn3 = FindViewById<Button>(Resource.Id.btn3);

            btn1.Click += Btn1_Click;

            btn2.Click += Btn2_Click;
            btn3.Click += Btn3_Click;
        }

        private void Btn3_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SaleMainActivity));
            Finish();
        }

        private void Btn2_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(ProductMainActivity));
            Finish();
        }

        private void Btn1_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(CategoryMainActivity));
            Finish();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}