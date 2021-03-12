using ClinicPac.Models;
using Nancy.Json;
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
    public partial class AccountPage : ContentPage
    {
    
       
        private readonly HttpClient client = new HttpClient();
        public AccountPage()
        {
            InitializeComponent();
        }

        public async void SaveUserChangesClicked(object sender, EventArgs e)
        {
            LblResult.Text = "";
            User currentUser = await App.Database.GetUserAsync();
            var user = (User)BindingContext;

            if (currentUser.Password == PasswordConfirm.Text)
            {
                if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Alias) || String.IsNullOrEmpty(user.Name))
                {
                    LblResult.Text = "Los campos email, nombre y alias son obligatorios";
                    LblResult.TextColor = Color.Orange;
                }
                else
                {
                    user.NewPass = Pass.Text;
                    user.NewEmail = Email.Text;
                    user.Email = currentUser.Email;
                    user.Password = currentUser.Password;
                    loading.IsRunning = true;
                    currentUser = await SaveUserChanges(user);
                    loading.IsRunning = false;
                    if (currentUser != null)
                    {
                        await App.Database.OnlySaveUserAsync(currentUser);
                        LblResult.Text = "Cambios guardados!";
                        LblResult.TextColor = Color.Green;
                        (App.Master.Master as Master).SetAlias();
                    }
                }
            }
            else
            {
                LblResult.Text = "Contraseña actual incorrecta";
                LblResult.TextColor = Color.Red;
            }
        }


        public async Task<User> SaveUserChanges(User user)
        {

            var url = App.Url + "/Users/Edit";
            var data = new
            {
                ID = user.ID,
                Name = user.Name,
                Alias = user.Alias,
                Password = user.Password,
                Email = user.Email,
                NewEmail = user.NewEmail,
                Token = user.Token,
                NewPass = user.NewPass
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var Scontent = await client.PostAsync(url, stringContent);
            string resultContent = Scontent.Content.ReadAsStringAsync().Result;
            User result = JsonConvert.DeserializeObject<User>(resultContent);
            return result;
        }
     
    }
}