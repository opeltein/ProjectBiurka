using Microsoft.EntityFrameworkCore;
using ProjectBiurka2.Models;
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

namespace ProjectBiurka2.Views
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Pracownik pracownik = new Pracownik() { Imie = txtImie.Text, Nazwisko = txtNazwisko.Text };
                dbContext.Pracownicy.AddRange(new Pracownik[] { pracownik });


                dbContext.Biurka.AddRange(new Biurko[]
                  {
                        new Biurko(){Numer = Convert.ToInt32(txtNumer.Text),
                            Pomieszczenie = (Pomieszczenie)cbPomieszczenie.SelectedItem,
                            Producent = (Producent)cbProducent.SelectedItem,
                            Pracownik = pracownik},

                  });



                dbContext.SaveChanges();
                MessageBox.Show("Zapisano");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Numer biurka nie może się powtórzyć");
            }
        }
    }
}
