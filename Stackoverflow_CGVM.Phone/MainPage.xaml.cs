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
using RestSharp.Portable.Deserializers;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Stackoverflow_CGVM.Phone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public sealed partial class MainPage : Page
    {
        private readonly List<QuestionListModel> Questions;
        private bool areQuestions;
        private Guid questionId;
        public MainPage()
        {
            Questions = new List<QuestionListModel>();
            areQuestions = true;
            this.InitializeComponent();  
            this.NavigationCacheMode = NavigationCacheMode.Required;
            questionsList.SelectionChanged += questionsList_SelectionChanged;
            Answerbtn.Visibility = Visibility.Collapsed;
           setButtonsVisible();
            GetQuestions();
        }

        void questionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (areQuestions)
            {
                if (questionsList.SelectedIndex >= 0)
                {
                    getDetail(Questions.ElementAt(questionsList.SelectedIndex).QuestionId);
                    questionId = Questions.ElementAt(questionsList.SelectedIndex).QuestionId;
                }
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            setButtonsVisible();
            if (e.Parameter != null && e.Parameter.ToString()!="")
            {
                if (e.Parameter.Equals("Created"))
                {
                    questionsList.Items.Clear();
                    Questions.Clear();
                    GetQuestions();

                }
                else
                {
                    questionsList.Items.Clear();
                    getDetail(Guid.Parse(e.Parameter.ToString()));
                }
            }
        }


        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void setButtonsVisible()
        {
            Object value = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Token"];
            if (value == null)
            {
                Login.Visibility = Visibility.Visible;
                Register.Visibility = Visibility.Visible;
                CreateQuestion.Visibility = Visibility.Collapsed;
                Logout.Visibility = Visibility.Collapsed;
            }
            else
            {
                Login.Visibility = Visibility.Collapsed;
                Register.Visibility = Visibility.Collapsed;
                CreateQuestion.Visibility = Visibility.Visible;
                Logout.Visibility = Visibility.Visible;
            } 
        }
        private async void GetQuestions()
        {
            RestClient cliente = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:1885/")
            };
            RestRequest request = new RestRequest { Resource = "api/QuestionIndex" };
            var result = await cliente.Execute(request);
            RestSharp.Portable.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            var list = deserial.Deserialize<IEnumerable<QuestionListModel>>(result);
            
            foreach (var VARIABLE in list)
            {
                if (questionsList.Items != null)
                {
                    questionsList.Items.Add(VARIABLE.Title + " by: " + VARIABLE.OwnerName);
                    Questions.Add(VARIABLE);
                }
            }
            areQuestions = true;
            Answerbtn.Visibility = Visibility.Collapsed;
        }

        private async void getAnswers(Guid questionId)
        {
            RestClient cliente = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:1885/")
            };
            RestRequest request = new RestRequest {Resource = "api/AnswerIndex/" + questionId};
            var result = await cliente.Execute(request);
            RestSharp.Portable.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            var list = deserial.Deserialize<IEnumerable<AnswerListModel>>(result);
            questionsList.Items.Add("-------------------------");
            foreach (var a in list)
            {
                questionsList.Items.Add(a.name);
                questionsList.Items.Add(a.Description);
            }
        }
        private async void getDetail(Guid questionId)
        {
            RestClient cliente = new RestClient()
            {
                BaseUrl = new Uri("http://localhost:1885/")
            };
            RestRequest request = new RestRequest {Resource = "api/QuestionDetail/" + questionId};
            var result = await cliente.Execute(request);
            RestSharp.Portable.Deserializers.JsonDeserializer deserial = new JsonDeserializer();
            var question = deserial.Deserialize<DetailModel>(result);   
            questionsList.Items.Clear();
            questionsList.Items.Add(question.Title);
            questionsList.Items.Add(question.Description);
            areQuestions = false;
            Object value = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Token"];
            Debug.WriteLine(value);
            if (value != null) 
                Answerbtn.Visibility = Visibility.Visible;
            getAnswers(questionId);
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
           Frame.Navigate(typeof(Login));

        }

        private void CreateQuestion_Click(object sender, RoutedEventArgs e)
        {
   
            Frame.Navigate(typeof(CreateQuestion));

        }

        private void Answerbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (CreateAnswer),questionId);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (Register));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Object value = Windows.Storage.ApplicationData.Current.LocalSettings.Values["Token"] = null;
            setButtonsVisible();
            Frame.Navigate(typeof (MainPage));
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage),"Created");

        }

       
    }
}
