using Microsoft.EntityFrameworkCore;
using ProjectBiurka2.Models;

using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Linq;
using System.Collections.ObjectModel;
using System;

namespace ProjectBiurka2.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : Window
    {
        private readonly MyContext dbContext = new MyContext();
        private CollectionViewSource categoryViewSource;
        private FindView findView;
        private AddView addView;

        public HomeView()
        {
            InitializeComponent();
            categoryViewSource = (CollectionViewSource)FindResource(nameof(categoryViewSource));
            bool WasCreated = dbContext.Database.EnsureCreated();


            // baza zostala utworzona (jest teraz pusta) wiec dodajmy rekordy
            if (WasCreated)
            {

                Producent[] producents = new Producent[]
                    {
                        new Producent(){Nazwa = "Darex"},
                        new Producent(){Nazwa = "MebWay"},
                        new Producent(){Nazwa = "Elzab"}
                    };

                dbContext.Producenci.AddRange(producents);
                dbContext.SaveChanges();


                Pomieszczenie[] pomieszczenies = new Pomieszczenie[]
                    {
                        new Pomieszczenie(){Nazwa = "Hala 1"},
                        new Pomieszczenie(){Nazwa = "Programisci"},
                        new Pomieszczenie(){Nazwa = "Sekretariat"}
                    };

                dbContext.Pomieszczenia.AddRange(pomieszczenies);
                dbContext.SaveChanges();

                Pracownik[] pracowniks = new Pracownik[]
                   {
                        new Pracownik(){Imie = "Jan", Nazwisko="Kowalski"},
                        new Pracownik(){Imie = "Anna", Nazwisko="Nowak"}

                   };

                dbContext.Pracownicy.AddRange(pracowniks);
                dbContext.SaveChanges();


                dbContext.Biurka.AddRange(new Biurko[]
                   {
                        new Biurko(){Numer = 1, Pomieszczenie = pomieszczenies[0], Producent = producents[0], Pracownik = pracowniks[0]},
                        new Biurko(){Numer = 2, Pomieszczenie = pomieszczenies[1], Producent = producents[1], Pracownik = pracowniks[1]},

                   });

                dbContext.SaveChanges();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dbContext.Database.EnsureCreated();

            dbContext.Biurka.Load();
            dbContext.Pracownicy.Load();
            dbContext.Producenci.Load();
            dbContext.Pomieszczenia.Load();


            categoryViewSource.Source =
                 dbContext.Biurka.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dbContext.SaveChanges();
                MessageBox.Show("Zapisano");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Numer biurka nie może się powtórzyć");
            }
        }

        private void Button_Znajdz_Click(object sender, RoutedEventArgs e)
        {
            findView = new FindView();
            findView.Closing += FindView_Closing;
            findView.Show();
        }

        private void Button_Dodaj_Click(object sender, RoutedEventArgs e)
        {
            addView = new AddView();
            //addView.Closing += FindView_Closing;
            addView.Show();
        }


        private void Button_Wszystkie_Click(object sender, RoutedEventArgs e)
        {
            dbContext.Biurka.Load();
            dbContext.Pracownicy.Load();
            dbContext.Producenci.Load();
            dbContext.Pomieszczenia.Load();

            categoryViewSource.Source =
                dbContext.Biurka.Local.ToObservableCollection();
        }

        private void FindView_Closing(object sender, CancelEventArgs e)
        {
            if ((findView.txtNumer.Text != "") && (findView.txtImie.Text != "") && (findView.txtNazwisko.Text != ""))
            {
                var biurko = from b in dbContext.Biurka
                             where b.Numer == Convert.ToInt32(findView.txtNumer.Text) &&
                             b.Pracownik.Imie == findView.txtImie.Text &&
                             b.Pracownik.Nazwisko == findView.txtNazwisko.Text
                             select b;


                categoryViewSource.Source = biurko.ToList();
            }
            else if ((findView.txtNumer.Text == "") && (findView.txtImie.Text != "") && (findView.txtNazwisko.Text != ""))
            {
                var biurko = from b in dbContext.Biurka
                             where
                             b.Pracownik.Imie == findView.txtImie.Text &&
                             b.Pracownik.Nazwisko == findView.txtNazwisko.Text
                             select b;


                categoryViewSource.Source = biurko.ToList();
            }
            else if ((findView.txtNumer.Text != "") && (findView.txtImie.Text == "") && (findView.txtNazwisko.Text != ""))
            {
                var biurko = from b in dbContext.Biurka
                             where b.Numer == Convert.ToInt32(findView.txtNumer.Text) &&
                             b.Pracownik.Nazwisko == findView.txtNazwisko.Text
                             select b;


                categoryViewSource.Source = biurko.ToList();
            }
            else if ((findView.txtNumer.Text != "") && (findView.txtImie.Text != "") && (findView.txtNazwisko.Text == ""))
            {
                var biurko = from b in dbContext.Biurka
                             where b.Numer == Convert.ToInt32(findView.txtNumer.Text) &&
                             b.Pracownik.Imie == findView.txtImie.Text
                             select b;


                categoryViewSource.Source = biurko.ToList();
            }
            else if ((findView.txtNumer.Text == "") && (findView.txtImie.Text == "") && (findView.txtNazwisko.Text != ""))
            {
                var biurko = from b in dbContext.Biurka
                             where
                             b.Pracownik.Nazwisko == findView.txtNazwisko.Text
                             select b;


                categoryViewSource.Source = biurko.ToList();
            }
            else if ((findView.txtNumer.Text == "") && (findView.txtImie.Text != "") && (findView.txtNazwisko.Text == ""))
            {
                var biurko = from b in dbContext.Biurka
                             where
                             b.Pracownik.Imie == findView.txtImie.Text
                             select b;


                categoryViewSource.Source = biurko.ToList();
            }
            else if ((findView.txtNumer.Text != "") && (findView.txtImie.Text == "") && (findView.txtNazwisko.Text == ""))
            {
                var biurko = from b in dbContext.Biurka
                             where b.Numer == Convert.ToInt32(findView.txtNumer.Text)
                             select b;


                categoryViewSource.Source = biurko.ToList();
            }
            else
            {
                return;
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }
    }
}