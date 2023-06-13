using FinanScope.Services;
using FinanScope.ViewModels;
using FinanScope.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace FinanScope
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : Xamarin.Forms.TabbedPage
    {
        public const string DATABASE_NAME = "finanscope.db";
        public TabbedPage1()
        {
            
            var databaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
            var mainViewModel = new MainViewModel(databaseService);
            NavigationPage navigationPage = new NavigationPage(new MainPage(mainViewModel));
            navigationPage.IconImageSource = "budget.png";
            navigationPage.Title = "Budget";
            //NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage plansPage = new NavigationPage(new plansPage());
            plansPage.IconImageSource = "plans.png";
            plansPage.Title = "Plans";
            NavigationPage stocksPage = new NavigationPage(new StocksPage());
            stocksPage.IconImageSource = "stocks.png";
            stocksPage.Title = "Stocks";
            //On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            Children.Add(navigationPage);
            Children.Add(plansPage);
            Children.Add(stocksPage);
        }
    }
}