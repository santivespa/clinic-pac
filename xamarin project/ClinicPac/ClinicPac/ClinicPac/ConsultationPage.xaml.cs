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
    public partial class ConsultationPage : ContentPage
    {
     
      

        private readonly HttpClient client = new HttpClient();
        public ConsultationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var cons = (Consultation)BindingContext;
            Title = cons.Name;
            if(cons.State != Consultation.States.Paid)
            {
                if (cons.State == Consultation.States.NoTicket)
                {

                    TicketDateDetails.IsVisible = false;
                    PaidDateDetails.IsVisible = false;
                }
                else if (cons.State == Consultation.States.Ticket)
                {
                    PaidDateDetails.IsVisible = false;
                }
            }
            else
            {
                StatePicker.IsVisible = false;
                SaveButton.IsVisible = false;
            }
            base.OnAppearing();

        }
        public async void  DeleteConsultationClicked(object sender, EventArgs e)
        {
            try
            {


                bool res = await DisplayAlert("Borrar ", "La consulta se borrará  permanentemente", "Aceptar", "Cancelar");
                if (res)
                {
                    User user = await App.Database.GetUserAsync();
                    var cons = (Consultation)BindingContext;
                    await App.Database.DeleteConsAsync(cons);
                    loading.IsRunning = true;

                    await DeleteConsultation(user, cons);
                    loading.IsRunning = false;

                    await Navigation.PopAsync();
                }
            }
            catch
            {
                loading.IsRunning = false;
                ResultText.Text = "Error del servidor";
                ResultText.TextColor = Color.Red;
            }
        }
        public async void SaveClicked(object sender, EventArgs e)
        {

            try 
            {
                var cons = (Consultation)BindingContext;
                User user = await App.Database.GetUserAsync();


                string selectedEmployee = StatePicker.Items[StatePicker.SelectedIndex];

                if (selectedEmployee == "Sin boleta" || selectedEmployee == "Pagada" || selectedEmployee == "Con boleta")
                {
                    cons.State = selectedEmployee == "Pagada" ? Consultation.States.Paid : (selectedEmployee == "Sin boleta" ? Consultation.States.NoTicket : Consultation.States.Ticket);
                    cons.SetCurrentDate();
                    App.Database.SaveConsState(cons);

                    loading.IsRunning = true;

                    if (await ChangeConsultationState(user, cons))
                    {
                        if (cons.Ticket())
                        {
                            NoTicketDateDetails.IsVisible = true;
                            TicketDateDetails.IsVisible = true;
                            PaidDateDetails.IsVisible = false;
                        }
                        else if (cons.NoTicket())
                        {
                            NoTicketDateDetails.IsVisible = true;
                            TicketDateDetails.IsVisible = false;
                            PaidDateDetails.IsVisible = false;
                        }
                        else if (cons.Paid())
                        {
                            NoTicketDateDetails.IsVisible = true;
                            TicketDateDetails.IsVisible = true;
                            PaidDateDetails.IsVisible = true;
                        }
                        ResultText.Text = "Cambios guardados";
                        ResultText.TextColor = Color.Green;

                    }
                    else
                    {
                        ResultText.Text = "Ha ocurrido un problema inténtalo nuevamente más tarde";
                        ResultText.TextColor = Color.Red;
                    }

                    loading.IsRunning = false;

                }
            }
            catch
            {
                loading.IsRunning = false;
                ResultText.Text = "Error del servidor";
                ResultText.TextColor = Color.Red;
            }
         
        }
        //helpers
      
        public async Task<bool> ChangeConsultationState(User user, Consultation cons)
        {

            var url = App.Url + "/Consultations/ChangeConsultationState";
            var data = new
            {        
                ID=cons.ID,
                User = user,
                State = cons.State
            };

   

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var Scontent = await client.PostAsync(url, stringContent);
            string resultContent = Scontent.Content.ReadAsStringAsync().Result;
            bool result = JsonConvert.DeserializeObject<bool>(resultContent);
            return result;
        }
        public async Task<bool> DeleteConsultation(User user, Consultation cons)
        {
            
            var url = App.Url + "/Consultations/DeleteConsultation";
            var data = new
            {
                User = user,
                ID=cons.ID
            };

            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            var Scontent = await client.PostAsync(url, stringContent);
            string resultContent = Scontent.Content.ReadAsStringAsync().Result;
            bool result = JsonConvert.DeserializeObject<bool>(resultContent);
            return result;
        }
       
    }
}