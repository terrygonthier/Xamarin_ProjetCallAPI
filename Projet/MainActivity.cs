using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Graphics.Drawables;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FFImageLoading;
using FFImageLoading.Views;
using Realms;
using System.Linq;
using Android.Content;

namespace Projet
{
    [Activity(Label = "ProjetApiWeather", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView  lat , lon , temp, country, tempMax, tempMin, windSpeed, desc, humidite;
        private Button historique, rechercher;
        private EditText ville;
        private HttpClient client = new HttpClient();
        private double tmpmax, tmpmin, tmp;
        private ImageViewAsync weatherImg;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            
            ville = FindViewById<EditText>(Resource.Id.ville);
            lat = FindViewById<TextView>(Resource.Id.lat);
            lon = FindViewById<TextView>(Resource.Id.lon);
            temp = FindViewById<TextView>(Resource.Id.temp);
            country = FindViewById<TextView>(Resource.Id.country);
            tempMax = FindViewById<TextView>(Resource.Id.tempMax);
            tempMin = FindViewById<TextView>(Resource.Id.tempMin);
            windSpeed = FindViewById<TextView>(Resource.Id.windSpeed);
            humidite = FindViewById<TextView>(Resource.Id.humidite);
            historique = FindViewById<Button>(Resource.Id.historique);
            desc = FindViewById<TextView>(Resource.Id.desc);
            rechercher = FindViewById<Button>(Resource.Id.rechercher);
            weatherImg = FindViewById<ImageViewAsync>(Resource.Id.weatherImg);

            //au demarrage on affiche la derniere recherhe
            AfficheDerniereRecherche();
            
            rechercher.Click += delegate
             {
                 CallApi();
             };

            historique.Click += delegate
            {
                Intent myIntent = new Intent(this,typeof(HistoriqueActivity));
                StartActivity(myIntent);
            };
        }

        private async void CallApi()
        {
            
            RootObject result = await Sample();
            if (result != null)
            {
                var realm = Realm.GetInstance();
                var key = realm.All<WeatherBDD>();
                int id = key.AsRealmCollection().Count() + 1;

                //remplir l'historique
                realm.Write(() =>
                {
                    realm.Add(new WeatherBDD()
                    {
                        id = id,
                        name = result.name,
                        lat = result.coord.lat.ToString(),
                        lon = result.coord.lon.ToString(),
                        country = result.sys.country,
                        temp = result.main.temp.ToString(),
                        temp_max = result.main.temp_max.ToString(),
                        temp_min = result.main.temp_min.ToString(),
                        humidity = result.main.humidity.ToString(),
                        speed = result.wind.speed.ToString(),
                        description = result.weather[0].description,
                        icon = result.weather[0].icon,
                    });
                });

                //remplir les vues 
                country.Text = "Region : " + result.sys.country;
                lat.Text = "Latitude : " + result.coord.lat.ToString();
                lon.Text = "Longitude : " + result.coord.lon.ToString();
                tmp = result.main.temp - 273.15;
                tmpmax = result.main.temp_max - 273.15;
                tmpmin = result.main.temp_min - 273.15;
                temp.Text = "Temperature : " + String.Format("{0:F1}", tmp) + " °C";
                tempMax.Text = "Max : " + String.Format("{0:F1}", tmpmax) + " °C";
                tempMin.Text = "Min : " + String.Format("{0:F1}", tmpmin) + " °C";
                windSpeed.Text = "Vitesse du vent: " + result.wind.speed.ToString() + " km/h";
                desc.Text = result.weather[0].description;
                string URL = "http://openweathermap.org/img/w/" + result.weather[0].icon + ".png";
                ImageService.Instance.LoadUrl(URL).Into(weatherImg);
                humidite.Text = "Humidite : " + result.main.humidity.ToString() + " %";
            }

            else
            {
                country.Text = "erreur";
            }
        }
        
        private Uri URI
        {
            get { return new Uri(string.Format("http://api.openweathermap.org/data/2.5/weather?q=" + ville.Text + "&appid=8507ea676eda9b57dfd5d92da100a27b")); }
        }
        
        public async Task<RootObject> Sample()
        {
            RootObject data = null;
            HttpResponseMessage response = await client.GetAsync(URI);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                data = JsonConvert.DeserializeObject<RootObject>(content);
            }
            return data;
        }

        private void AfficheDerniereRecherche()
        {
            var realm = Realm.GetInstance();
            //on recupere la derniere entrée de la bdd
            var ville1 = realm.All<WeatherBDD>().OrderByDescending(v => v.id).First();

            //on rempli les vues
            ville.Text = ville1.name;
            country.Text = "Region : " + ville1.country;
            lat.Text = "Latitude : " + ville1.lat;
            lon.Text = "Longitude : " + ville1.lon;
            tmp = Convert.ToDouble(ville1.temp) - 273.15;
            tmpmax = Convert.ToDouble(ville1.temp_max) - 273.15;
            tmpmin = Convert.ToDouble(ville1.temp_min) - 273.15;
            temp.Text = "Temperature : " + String.Format("{0:F1}", tmp.ToString()) + " °C";
            tempMax.Text = "Max : " + String.Format("{0:F1}", tmpmax.ToString())+ " °C";
            tempMin.Text = "Min : " + String.Format("{0:F1}", tmpmin.ToString()) + " °C";
            windSpeed.Text = "Vitesse du vent: " + ville1.speed + " km/h";
            desc.Text = ville1.description;
            string URL = "http://openweathermap.org/img/w/" + ville1.icon + ".png";
            ImageService.Instance.LoadUrl(URL).Into(weatherImg);
            humidite.Text = "Humidite : " + ville1.humidity + " %";
        }
    }
}


