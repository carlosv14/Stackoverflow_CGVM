using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Stackoverflow_CGVM.Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        public Register()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void prepareInfo()
        {
            AccountRegisterModel model = new AccountRegisterModel();
            model.Name = Fname.Text;
            model.LastName = Lname.Text;
            model.Passw = Password.Password;
            model.ConfirmPassword = Password.Password;
            model.Email = Email.Text;
            RestClient cliente = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:1885/")
            };
            RestRequest request = new RestRequest { Resource = "api/Register" };
            request.AddJsonBody(model);
            var result = await cliente.Execute(request);
            if (result.IsSuccess)
                Frame.Navigate(typeof (MainPage));

        }
        private void Register1_Click(object sender, RoutedEventArgs e)
        {
            prepareInfo();
        }
    }
}
