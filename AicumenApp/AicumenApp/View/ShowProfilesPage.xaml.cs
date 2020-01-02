using AicumenApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AicumenApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowProfilesPage : ContentPage
    {
        public ShowProfilesPage()
        {
            InitializeComponent();

            BindingContext = new ShowProfilesPageViewModel();
        }
    }
}