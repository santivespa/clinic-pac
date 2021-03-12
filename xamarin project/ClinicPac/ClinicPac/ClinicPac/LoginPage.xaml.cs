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
    public partial class LoginPage : ContentPage
    {

        private readonly HttpClient client = new HttpClient();
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void LoginClicked(object sender, EventArgs e)
        {

            var url = App.Url + "/Users/Login?Email=" + Email.Text + "&Pass=" + Password.Text;
            loading.IsRunning = true;




            try
            {
                var Scontent = await client.PostAsync(url, null);
                loading.IsRunning = false;
                string resultContent = Scontent.Content.ReadAsStringAsync().Result;
                User user = JsonConvert.DeserializeObject<User>(resultContent);

                if (user != null)
                {
                    await App.Database.SaveUserAsync(user);

                    await Navigation.PushModalAsync(new MainPage());
                }
                else
                {
                    LoginResult.Text = "Usuario no encontrado";
                    LoginResult.TextColor = Color.Red;
                }
            }
            catch
            {
                loading.IsRunning = false;
                LoginResult.Text = "Error del servidor";
                LoginResult.TextColor = Color.Red;
            }

        }

    }
}