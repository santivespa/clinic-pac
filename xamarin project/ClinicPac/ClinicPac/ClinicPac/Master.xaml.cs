using ClinicPac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClinicPac
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public Master()
        {
            InitializeComponent();
           
           
        }
        public async void SetAlias()
        {
            User u = await App.Database.GetUserAsync();
            if (u != null)
            {
                UserAlias.Text = u.Alias;
                Email.Text = u.Email;
            }
        }
        protected override async void OnAppearing()
        {
          
            User u= await App.Database.GetUserAsync();
            if (u != null)
            {
                UserAlias.Text = u.Alias;
                Email.Text = u.Email;
            }
            base.OnAppearing();
        }



        private async void NewConsultationClicked(object sender,EventArgs e)
        {
            App.Master.IsPresented = false;
           await App.Master.Detail.Navigation.PushAsync(new NewConsultationPage() { BindingContext=new Consultation()});
        }




        private async void ConsultationsClicked(object sender, EventArgs e)
        {
            App.Master.IsPresented = false;

            await App.Master.Detail.Navigation.PopToRootAsync();
        }
        private async void AccountClicked(object sender, EventArgs e)
        {
            User user =await  App.Database.GetUserAsync();
            if (user == null)
            {
                Navigation.PushModalAsync(new LoginPage());
            }
            else
            {
                App.Master.IsPresented = false;

                await App.Master.Detail.Navigation.PushAsync(new AccountPage() { BindingContext = user }); 
            }
           
        }
        private async void LogOutClicked(object sender, EventArgs e)
        {
            App.Database.ClearData();
            await Navigation.PushModalAsync(new LoginPage());
        }


        
    }
}