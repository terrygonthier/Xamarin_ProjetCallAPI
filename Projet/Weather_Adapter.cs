using Android.Content;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using FFImageLoading.Views;
using System;
using System.Collections.Generic;

namespace Projet
{
    public class Weather_Adapter : ArrayAdapter<WeatherBDD>
    {
        private List<WeatherBDD> items;

        public Weather_Adapter(Context context, List<WeatherBDD> objects) : base(context, 0, objects)
        {
            items = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //recup de la vue
            if (convertView == null)
            {
                //generere la vue
                convertView = LayoutInflater.FromContext(Context).Inflate(Resource.Layout.item_historique, null, false);
            }

            //recup de l'instance 
            WeatherBDD currentItem = GetItem(position);

            //recup des vues
            TextView ville = convertView.FindViewById<TextView>(Resource.Id.ville);
            TextView temp = convertView.FindViewById<TextView>(Resource.Id.temp);
            ImageViewAsync icon = convertView.FindViewById<ImageViewAsync>(Resource.Id.icon);

            //remplissage des vues
            ville.Text = currentItem.name;
            var tempTmp = Convert.ToDouble(currentItem.temp.ToString())-273.15;
            temp.Text = String.Format("{0:F1}", tempTmp) + " °C";
            string URL = "http://openweathermap.org/img/w/" + currentItem.icon + ".png";
            ImageService.Instance.LoadUrl(URL).Into(icon);

            return convertView;
        }

        public new WeatherBDD GetItem(int position)
        {
            return items[position];
        }

        public override int Count => items.Count;
    }
}