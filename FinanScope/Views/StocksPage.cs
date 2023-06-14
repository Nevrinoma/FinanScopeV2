using FinanScope.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FinanScope.Views
{
    public class StocksPage : ContentPage
    {
        public StocksPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            //string symbol = stocksAPI.GetStocks("AAPL");
            Content = new StackLayout
            {
                Children = {
                  // new Label { Text = "Apple price = " + symbol }
                }
                //Console.WriteLine($"Open {GetStocks("AAPL").Close}");
        };
        }
    }
}