using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WGU_Xamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Term> terms = new ObservableCollection<Term>();

        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await SetTermsList();
            TermList.ItemsSource = terms;
        }
        private async Task SetTermsList()
        {
            terms = new ObservableCollection<Term>(await DataManager.GetTerms());
            TermList.ItemsSource = terms;
        }

        private async void OnTermTapped(object sender, ItemTappedEventArgs args)
        {
            Term term = (Term)args.Item;
            await Navigation.PushModalAsync(new TermPage(term));
            await SetTermsList();
        }
        private async void OnAddTermBtnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermPage());
            await SetTermsList();
        }
    }
}