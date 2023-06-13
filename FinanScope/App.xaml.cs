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


        public App()
        {
            InitializeComponent();

            
            MainPage = new TabbedPage1();
        }

    }
}
