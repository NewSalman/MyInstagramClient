using MyInstagram2.Models;
using MyInstagram2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyInstagram2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();

            /*RefreshView refreshView = new RefreshView();
            CollectionView collectionView = new CollectionView();

            refreshView.SetBinding(RefreshView.IsRefreshingProperty, "IsRefreshing", BindingMode.OneWay);
            refreshView.SetBinding(RefreshView.CommandProperty, "RefreshCommand");

            var item = App.Posts;

            collectionView.ItemsSource = item;

            collectionView.ItemTemplate = new DataTemplate(() =>
            {
                StackLayout layout = new StackLayout()
                {
                    if(item)
                };
                return layout;
            });*/


        }
    }
}