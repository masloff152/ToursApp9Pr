using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
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
using System.Xml.Serialization;

namespace ToursApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HotelPage());
            Manager.MainFrame = MainFrame;


            // ImportTours();
            ImportHotels();
        }

        public class MyModel
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int ID { get; set; }
        }

        private void ImportHotels()
        {
            // var fileData = File.ReadAllLines(@"E:\Отели\Отели.txt");
            var images = Directory.GetFiles(@"C:\Фото отелей");

            // check github pullrequest

            //var currentType = ToursBaseEntities.GetContext().Hotels.ToList().FirstOrDefault(p => p.Name == tourType);

            var currentHotels = ToursBaseEntities.GetContext().Hotell.ToArray();

            foreach (var item in currentHotels)
            {
                var currentHotelImage = new Hotelimage();
                currentHotelImage.HotelId = item.id;
                currentHotelImage.ImageSource = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(item.Name)));


                if (currentHotelImage.id == 0)
                {
                    if (ToursBaseEntities.GetContext().Hotelimage.Any())
                    {

                        currentHotelImage.id = ToursBaseEntities.GetContext().Hotelimage.Max(x => x.id) + 1;
                        ToursBaseEntities.GetContext().Hotelimage.Add(currentHotelImage);
                        ToursBaseEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        currentHotelImage.id = 1;
                        ToursBaseEntities.GetContext().Hotelimage.Add(currentHotelImage);
                        ToursBaseEntities.GetContext().SaveChanges();
                    }
                }
            }
        }


            private void ImportTours()
        {
            var fileData = File.ReadAllLines(@"C:\Users\79991\Desktop\Университет\НГИЭУ\3 курс\УП 01.01. Маслов Н.С\Импорт данных\Туры.txt");
            var images = Directory.GetFiles(@"C:\Users\79991\Desktop\Университет\НГИЭУ\3 курс\УП 01.01. Маслов Н.С\Импорт данных\Туры фото");
            
            foreach (var line in fileData)
            {
                var data = line.Split('\t');

                var tempTour = new Tour
                {
                    Name = data[0].Replace("\"", ""),
                    TicetCount = int.Parse(data[2]),
                    Price = decimal.Parse(data[3]),
                    IsActual = (data[4] == "0") ? false : true

                };
                foreach (var tourType in data[5].Replace("\"", " ").Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var currentType = ToursBaseEntities.GetContext().Type.ToList().FirstOrDefault(p => p.Name == tourType);
                    if (currentType != null)

                        tempTour.Types.Add(currentType);
                }
                try
                {
                    tempTour.ImagePreviw = File.ReadAllBytes(images.FirstOrDefault(p => p.Contains(tempTour.Name)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

               
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;

            }
            else
            {
                BtnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}