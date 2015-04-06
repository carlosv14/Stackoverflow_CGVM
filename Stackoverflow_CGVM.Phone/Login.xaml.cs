using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
    public sealed partial class Login : Page
    {
        public Login()
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

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_1(object sender, RoutedEventArgs e)
        {

        }

        private async void createToken()
        {
            RestClient cliente = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:1885/")
            };
            var token = "";
            LoginModel modelo = new LoginModel();
            modelo.Email = username.Text;
            modelo.Passw = password.Password.ToString();
            RestRequest request = new RestRequest { Resource = "api/Login/"};
            request.AddJsonBody(modelo);
            var result = await cliente.Execute(request);
            if (result != null)
            {
                token = Encoding.UTF8.GetString(result.RawBytes,1,
                    result.RawBytes.GetUpperBound(0)-1);
             
                Windows.Storage.ApplicationData.Current.LocalSettings.Values["Token"] = token;
            }
            Debug.WriteLine(token);
            Frame.Navigate(typeof(MainPage));
            
        }

    private void Loginbtn_Click(object sender, RoutedEventArgs e)
        {
          createToken();
        }

    }
}
