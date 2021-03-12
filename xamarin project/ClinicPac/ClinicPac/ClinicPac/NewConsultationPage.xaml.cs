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
    public partial class NewConsultationPage : ContentPage
    {




        private readonly HttpClient client = new HttpClient();

        public NewConsultationPage()
        {
            InitializeComponent();
        }

        public void PrevisionFilterChanged(object sender, EventArgs e)
        {
            string result = "";
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                result = (string)picker.ItemsSource[selectedIndex];
            }
        }
        public async void NewConsultationClicked(object sender, EventArgs e)
        {

            var cons = (Consultation)BindingContext;
            User user = await App.Database.GetUserAsync();
            if (user != null)
            {
                if (PrevisionPicker.SelectedIndex == -1)
                {
                    LblResult.Text = "Debe seleccionar prevision";
                    LblResult.TextColor = Color.OrangeRed;
                }
                else
                {
                    string prevision = PrevisionPicker.Items[PrevisionPicker.SelectedIndex];
                    if (string.IsNullOrEmpty(prevision))
                    {
                        LblResult.Text = "Debe seleccionar prevision";
                        LblResult.TextColor = Color.OrangeRed;
                    }
                    else
                    {
                        loading.IsRunning = true;

                        try
                        {
                            Consultation newCons = await CreateConsultation(user, cons.Name, cons.Document, prevision, cons.Surgery, DateTime.Now);
                            loading.IsRunning = false;

                            if (newCons != null)
                            {
                                App.Database.SaveConsAsync(newCons);
                                await DisplayAlert("Resultado", "Consulta registrada", "OK");
                                await App.Master.Detail.Navigation.PopToRootAsync();
                            }
                        }
                        catch { loading.IsRunning = false; }

                    }
                }

            }
            else
            {
                NavigationPage page = new NavigationPage(new LoginPage());
                await Navigation.PushModalAsync(page);
            }

        }
        public async Task<Consultation> CreateConsultation(User user, string name, string document, string prevision, string surgery, DateTime? ticketDate)
        {

            var url = App.Url + "/Consultations/PostConsultation";
            var data = new
            {
                Name = name,
                Document = document,
                Surgery = surgery,
                TicketDate = ticketDate,
                Prevision = prevision,
                User = user
            };

            // var Scontent = await client.PostAsJsonAsync(url, data);


            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var Scontent = await client.PostAsync(url, stringContent);
            string resultContent = Scontent.Content.ReadAsStringAsync().Result;
            Consultation cons = JsonConvert.DeserializeObject<Consultation>(resultContent);

            if (cons != null)
                return cons;
            return null;
        }

    }
}