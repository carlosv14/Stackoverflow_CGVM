using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Stackoverflow_CGVM.Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateAnswer : Page
    {
        public Guid questionId;
        public CreateAnswer()
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
            if (e.Parameter != null)
                questionId =Guid.Parse(e.Parameter.ToString());
        }

        public async void generateAnswer()
        {
            AnswerCreateModel modelo = new AnswerCreateModel();
            modelo.AnswerDescription = AnswerD.Text;
            modelo.QuestionId = questionId;
            RestClient cliente = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:1885/")
            };
            Object value = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Token"];
            modelo.token = value.ToString();

            RestRequest request = new RestRequest { Resource = "api/CreateAnswer/" };
            request.AddJsonBody(modelo);
            var result = await cliente.Execute(request);
            if (result.IsSuccess)
            {
                Debug.WriteLine(modelo.token);
                Frame.Navigate(typeof(MainPage),questionId);
            }
        }
        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            generateAnswer();
        }
    }
}
