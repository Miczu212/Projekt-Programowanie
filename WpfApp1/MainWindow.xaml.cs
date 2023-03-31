using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        string sciezka;
        bool pierwszyraz = false;
        string czyzmiana; // ta zmienna jest używana by sprawdzic czy zawartosc textboxa sie zmienila od czasu wczytania textboxa
        
        private void sprawdzenie(object s, System.ComponentModel.CancelEventArgs e)//trzeba zrobić sprawdzenie czy plik został zapisany a jak nie to wyswietlic messageboxa
        {
            if (czyzmiana != okno.Text && pierwszyraz!=false)
            {
                MessageBoxResult czyzapisac = MessageBox.Show("Zmiany nie zostaną zapisane, czy przed wyjściem chciałbyś zapisać plik ?", "Wyjście", MessageBoxButton.YesNo);
                if (czyzapisac == MessageBoxResult.Yes)
                {
                    try
                    {
                        czyzmiana = okno.Text;
                        File.WriteAllText(sciezka, okno.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Blad podczas zapisywania pliku:");
                    }
                }
            }
        }
        private void skalowane(object s, SizeChangedEventArgs e)
        {
            double w = e.NewSize.Height / e.PreviousSize.Height;
            double sz = e.NewSize.Width / e.PreviousSize.Width;

            // Przeskaluj elementy interfejsu użytkownika proporcjonalnie do nowego rozmiaru okna
            okno.Height *= w;
            okno.Width *= sz;
        }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += czasrozpoczecia;
        }
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private void czasrozpoczecia(object s, RoutedEventArgs e) // ta funkcja jest wywoływana podczas startu programu
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += zegar;
            timer.Start();
        }

        private void zegar(object s, EventArgs e)
        {
            TimeSpan czas = stopwatch.Elapsed;
            czaslabel.Content = string.Format("{0:00}:{1:00}:{2:00}", czas.Hours, czas.Minutes, czas.Seconds);
        }
    
    private void wybierzplik(object s, RoutedEventArgs e) //funkcja która pozwoli wczytać plik z górnego menu do textboxa
        {
             if(pierwszyraz==false)
            {
                pierwszyraz = true;
                okno.Visibility = Visibility.Visible;
                Zapisz.Visibility = Visibility.Visible;
                Zapisz_Jako.Visibility = Visibility.Visible;
                start.Visibility = Visibility.Hidden;
            }
            var plikotwarcie = new Microsoft.Win32.OpenFileDialog();
            plikotwarcie.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (plikotwarcie.ShowDialog() == true)
            {
                sciezka = plikotwarcie.FileName;

                try
                {
                    var tekst = File.ReadAllText(sciezka);
                    okno.Text = tekst;
                    czyzmiana = tekst;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Blad podczas otwierania pliku:");
                }
            }
        }
        private void Zapis(object s, RoutedEventArgs e) //funkcja która pozwoli zapisać plik nad którym obecnie pracujemy
        {
            try
            {
                czyzmiana = okno.Text;
                File.WriteAllText(sciezka, okno.Text);
                MessageBox.Show("pomyslnie zapisano");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Blad podczas zapisywania pliku:");
            }
        }
        private void Zapisjako(object s, RoutedEventArgs e) //funkcja która pozwoli zapisać plik nad którym obecnie pracujemy w innym folderze
        {
            var sciezkatemp = sciezka;
            var zapissciezka = new Microsoft.Win32.SaveFileDialog();
            zapissciezka.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (zapissciezka.ShowDialog() == true)
            {
                sciezka = zapissciezka.FileName;

                try
                {
                    czyzmiana = okno.Text;
                    File.WriteAllText(sciezka, okno.Text);
                    File.Delete(sciezkatemp);
                    MessageBox.Show("pomyslnie zapisano");
                }
                 
                catch (Exception ex)
                {
                    MessageBox.Show("Blad podczas zapisywania pliku:");
                }
            }
        }
        private void Nowy(object s, RoutedEventArgs e) // Funkcja która pozwoli stworzyc nowy plik, trzeba jednak wpierw wybrac folder w którym się zapisze i nazwe
        {
            if (pierwszyraz == false)
            {
                pierwszyraz = true;
                okno.Visibility = Visibility.Visible;
                Zapisz.Visibility = Visibility.Visible;
                Zapisz_Jako.Visibility = Visibility.Visible;
                start.Visibility = Visibility.Hidden;
            }
            var zapissciezka = new Microsoft.Win32.SaveFileDialog();
            zapissciezka.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (zapissciezka.ShowDialog() == true)
            {
                sciezka = zapissciezka.FileName;

                try
                {
                    File.WriteAllText(sciezka, string.Empty);
                    okno.Text = string.Empty;
                    czyzmiana = okno.Text;
                    MessageBox.Show("pomyslnie zapisano");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Blad podczas zapisywania pliku:");
                }
            }
        }

        private void Button_Click(object s, RoutedEventArgs e) //funkcja do zaimplementowania
        {   
            //podzial na 2 osobne okna nad którymi mozna pracowac
            okno.Visibility=Visibility.Hidden;
            okno.Name = "oknowyl";
            temp1.Visibility = Visibility.Visible;
            temp2.Visibility = Visibility.Visible;
            temp1.Name = "okno";
            
           
        }
    }
}


