using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project2.Views
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : Window
    {
        private readonly MyContext dbContext = new MyContext();
        private CollectionViewSource producenciViewSource;
        private CollectionViewSource pomieszczeniaViewSource;

        public AddView()
        {
            InitializeComponent();
            producenciViewSource = (CollectionViewSource)FindResource(nameof(producenciViewSource));
            pomieszczeniaViewSource = (CollectionViewSource)FindResource(nameof(pomieszczeniaViewSource));


            dbContext.Producenci.Load();
            dbContext.Pomieszczenia.Load();

            producenciViewSource.Source = dbContext.Producenci.Local.ToObservableCollection();
            pomieszczeniaViewSource.Source = dbContext.Pomieszczenia.Local.ToObservableCollection();
        }

    }
}
