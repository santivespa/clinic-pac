using ClinicPac.LocalData;
using ClinicPac.Models;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClinicPac
{
    public partial class App : Application
    {
        static UserDataBase database;
        public static MasterDetailPage Master { get; set; }

        public static string Url ="https://clinicpactest.santivespa.com/api";

        public static UserDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new UserDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "User.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

        }

        protected override void OnStart()
        {
            //test
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
