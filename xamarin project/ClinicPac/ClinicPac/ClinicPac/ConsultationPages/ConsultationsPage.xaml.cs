
using ClinicPac.Models;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClinicPac.ConsultationPages
{
    [DesignTimeVisible(false)]
    public partial class ConsultationsPage : ContentPage
    {

        public ConsultationsPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
               
            var list = await App.Database.GetConsListAsync();
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
            var list = await App.Database.GetConsListAsync();
            SearchBar searchBar = (SearchBar)sender;
            var results = Consultation.Filter(list,searchBar.Text.ToLower());
            LoadListView(results);
          
        }
        
        public void LoadListView(List<Consultation> items)
        {
            items.ForEach(x=>
            {
                x.StateSpanish = ( x.NoTicket() ? "Sin boleta" : (x.Ticket() ? "Con boleta" : "Pagada")) + " ";
                x.StateColor = x.NoTicket() ? "Black" : (x.Ticket() ? "#2196f3" : "Green");
            });
            items.OrderByDescending(x => x.Date);
            listConsultationsView.ItemsSource = items;
        }


    }
}











