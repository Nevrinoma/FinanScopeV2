using FinanScope.Services;
using FinanScope.ViewModels;
using FinanScope.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinanScope
{
    public partial class App : Application
    {

        public const string DATABASE_NAME = "finanscope.db";
        public App()
        {
            InitializeComponent();

            var databaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
            var mainViewModel = new MainViewModel(databaseService);
            MainPage = new NavigationPage(new MainPage(mainViewModel));
        }

    }
}
