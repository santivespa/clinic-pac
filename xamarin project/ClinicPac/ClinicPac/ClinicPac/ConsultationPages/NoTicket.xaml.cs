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

namespace ClinicPac.ConsultationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoTicket : ContentPage
    {
     
        public NoTicket()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
             
            var list = await App.Database.GetNoTicketCons();
            LoadListView(list);
            SearchConsBar.Placeholder = "(" + list.Count() + ")Buscar...";

            base.OnAppearing();
        }
        public async void NewConsultationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewConsultationPage() { BindingContext = new Consultation() });
        }

        public async void ConsultationSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ConsultationPage
                {
                    BindingContext = e.SelectedItem as Consultation
                });
            }
        }
        public async void OnTextChangedShearch(object sender, EventArgs e)
        {
            var list = await App.Database.GetTicketCons();
            SearchBar searchBar = (SearchBar)sender;
            var results = Consultation.Filter(list, searchBar.Text);
            LoadListView(results);
            
        }
        public void LoadListView(List<Consultation> items)
        {
            items.ForEach(x =>
            {
                x.StateSpanish = x.NoTicket() ? "Sin boleta" : (x.Ticket() ? "Con boleta" : "Pagada") + " ";
                x.StateColor = x.NoTicket() ? "Black" : (x.Ticket() ? "#2196f3" : "Green");
            });
            listConsultationsView.ItemsSource = items;
        }

    }
}