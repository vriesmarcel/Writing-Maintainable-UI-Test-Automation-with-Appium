using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarvedRock.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCategory : ContentPage
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private async void AddNew_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddCategory", categoryName.Text);
            await Navigation.PopModalAsync();
        }
    }
}