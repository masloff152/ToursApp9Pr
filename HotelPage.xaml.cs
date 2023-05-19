using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ToursApp
{
    /// <summary>
    /// Логика взаимодействия для HotelPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        public HotelPage()
        {
            InitializeComponent();
            // DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotell.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotell));
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotell>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {hotelsForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ToursBaseEntities.GetContext().Hotell.RemoveRange(hotelsForRemoving);
                    ToursBaseEntities.GetContext().SaveChanges();

                    MessageBox.Show("Данные удалены");

                    DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotell.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                ToursBaseEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = ToursBaseEntities.GetContext().Hotell.ToList();
            }
        }
    }
}
