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
    public class WeatherBDD : RealmObject
    {
        [PrimaryKey]
        public int id { get; set; }

        public string name { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string country { get; set; }
        public string temp { get; set; }
        public string temp_max { get; set; }
        public string temp_min { get; set; }
        public string humidity { get; set; }
        public string speed { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}