using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClinicPac
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
      
     
        public MainPage()
        {
            InitializeComponent();
            this.Master = new Master();
            //this.Detail = new NavigationPage( new ConsultationsPage());

            this.Detail = new NavigationPage(new TabbedConsultations());
            App.Master = this;
        }

      
    }
}