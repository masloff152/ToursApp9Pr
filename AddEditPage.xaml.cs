using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
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

namespace ToursApp
{
    
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Hotell _currentHotel = new Hotell();
        

        public AddEditPage(Hotell selectedHotell)
        {
            InitializeComponent();


            if (selectedHotell != null)
            {
                _currentHotel = selectedHotell;
            }


            DataContext = _currentHotel;
            ComboCountries.ItemsSource = ToursBaseEntities.GetContext().Country.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_currentHotel.id < 0)
                errors.AppendLine("ID должен быть больше нуля");
            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите название отеля");
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Количество звезд - от 1 до 5");
            if (_currentHotel.Country == null)
                errors.AppendLine("Выберите страну");
            

            // ИЗМЕНЕНИЯ ГИТХАБ 

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel.id == 0)
            {
                if (ToursBaseEntities.GetContext().Hotell.Any())
                {
                    _currentHotel.id = ToursBaseEntities.GetContext().Hotell.Max(x => x.id) + 1;
                    ToursBaseEntities.GetContext().Hotell.Add(_currentHotel);
                   // ToursBaseEntities.GetContext().SaveChanges();
                }
                else
                {
                    _currentHotel.id = 1;
                    ToursBaseEntities.GetContext().Hotell.Add(_currentHotel);
                   // ToursBaseEntities.GetContext().SaveChanges();
                }
            }

            try
            {
                ToursBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
