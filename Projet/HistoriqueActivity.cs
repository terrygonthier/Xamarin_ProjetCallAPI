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
using Realms;

namespace Projet
{
    [Activity(Label = "Historique")]
    public class HistoriqueActivity : Activity
    {
        private Weather_Adapter adapter;
        private ListView list;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.list_historique);
            list = FindViewById<ListView>(Resource.Id.listFav);

            var realm = Realm.GetInstance();
            var weather = realm.All<WeatherBDD>();
            adapter = new Weather_Adapter(this, weather.ToList());
            list.Adapter = adapter;
        }
    }
}