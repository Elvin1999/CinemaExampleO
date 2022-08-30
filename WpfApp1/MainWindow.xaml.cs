using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public string ImagePath { get; set; }
        public string Minute { get; set; }
        public string Description { get; set; }

        public dynamic Data { get; set; }
        public dynamic SingleData { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Salam");
        }
      
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            //  MessageBox.Show("Enter");
            //btn.Background = Brushes.Red;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            //btn.Background = Brushes.Orange;
        }
        HttpClient httpClient { get; set; }=new HttpClient();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
       
            var name=movieTextBox.Text;

            HttpResponseMessage response=new HttpResponseMessage();
            response=httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=ddee1dae&s={name}&plot=full").Result;
            var str=response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);

            response = httpClient.GetAsync($@"http://www.omdbapi.com/?apikey=ddee1dae&t={Data.Search[0].Title}&plot=full").Result;
            str = response.Content.ReadAsStringAsync().Result;
            SingleData = JsonConvert.DeserializeObject(str);


            movieImage.Source = SingleData.Poster;
            movieImage2.Source = SingleData.Poster;
            Minute = SingleData.RunTime;
            Description = SingleData.Genre;

            movieLabel.Content = Minute + "  " + Description;

            movieTextBox.Foreground = Brushes.White;
            searchBtn.Foreground = Brushes.White;
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Title = this.ActualWidth.ToString();
             // if(this.ActualWidth >1000 && this.ActualWidth <= 1500)
          //  {
                movieImage2.Width = this.ActualWidth;
                movieImage.Width = this.ActualWidth/2;
            movieImage2.Height = this.ActualHeight;
            movieImage.Height = this.ActualHeight / 2;
            // }
        }
    }
}
