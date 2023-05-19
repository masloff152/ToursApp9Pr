using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    {
        public ToursPage()
        {
            InitializeComponent();


            var allTypes = ToursBaseEntities.GetContext().Tour.ToList();
            allTypes.Insert(0, new Tour
            {
                Name = "Все типы"
            });
            ComboType.ItemsSource = allTypes;

            checkActual.IsChecked = true;

            ComboType.SelectedIndex = 0;


            UpdateTours();
        }


        private void UpdateTours()
        {
            var currentTours = ToursBaseEntities.GetContext().Tour.ToList();

            if (ComboType.SelectedIndex > 0)
            {
                currentTours = currentTours.Where(p => p.Types.Contains(ComboType.SelectedItem as Type)).ToList();
            }

            currentTours = currentTours.Where(p => p.Name.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (checkActual.IsChecked.Value)
                currentTours = currentTours.Where(p => p.IsActual).ToList();

            LViewTours.ItemsSource = currentTours.OrderBy(p => p.TicetCount).ToList();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void checkActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }
    }
}
