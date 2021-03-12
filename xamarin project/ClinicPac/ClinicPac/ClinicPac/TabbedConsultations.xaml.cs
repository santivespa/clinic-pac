using ClinicPac.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClinicPac
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedConsultations : TabbedPage
    {
    
        private readonly HttpClient client = new HttpClient();
        public TabbedConsultations()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var connected = await App.Database.GetUserAsync();
            if (connected == null)
            {
                 Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                try
                {
                    var url = App.Url + "/Users/Login?Email=" + connected.Email + "&Pass=" + connected.Password;
                    var Scontent = await client.PostAsync(url, null);
                    string resultContent = Scontent.Content.ReadAsStringAsync().Result;
                    User user = JsonConvert.DeserializeObject<User>(resultContent);
                    if (user != null)
                    {
                        await App.Database.SaveUserAsync(user);
                    }
                }
                catch { }
              
                base.OnAppearing();
            }

        }
      
    }
}