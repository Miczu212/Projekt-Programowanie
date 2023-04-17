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
        string sciezka,sciezka2;

        bool pierwszyraz = false,podzial=false;
        string czyzmiana,czyzmiana2; // ta zmienna jest używana by sprawdzic czy zawartosc textboxa sie zmienila od czasu wczytania textboxa
        
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
                        if(podzial==true)
                        {
                            czyzmiana2 = temp2.Text;
                            File.WriteAllText(sciezka2, temp2.Text);
                        }
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
                podziel.Visibility = Visibility.Visible;
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
        private void wybierzplik2 (object s, RoutedEventArgs e) //funkcja która pozwoli wczytać plik z górnego menu do textboxa
        {
            Zapisz_Jako_2.Visibility = Visibility.Visible;
            temp2.Visibility = Visibility.Visible;
            tempo2.Visibility = Visibility.Hidden;
            var plikotwarcie = new Microsoft.Win32.OpenFileDialog();
            plikotwarcie.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (plikotwarcie.ShowDialog() == true)
            {
                sciezka2 = plikotwarcie.FileName;

                try
                {
                    var tekst = File.ReadAllText(sciezka2);
                    temp2.Text = tekst;
                    czyzmiana2 = tekst;
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
                if(podzial==true)
                {
                    czyzmiana2 = temp2.Text;
                    File.WriteAllText(sciezka2, temp2.Text);
                }
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
        private void Zapisjako2(object s, RoutedEventArgs e) //funkcja która pozwoli zapisać plik nad którym obecnie pracujemy w innym folderze
        {   
            var sciezkatemp = sciezka2;
            var zapissciezka = new Microsoft.Win32.SaveFileDialog();
            zapissciezka.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (zapissciezka.ShowDialog() == true)
            {
                sciezka2 = zapissciezka.FileName;

                try
                {
                    czyzmiana2 = temp2.Text;
                    File.WriteAllText(sciezka2, temp2.Text);
                    File.Delete(sciezkatemp);
                    MessageBox.Show("pomyslnie zapisano");
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Blad podczas zapisywania pliku:");
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //Musze dodac okno które wyskakuje w którym mozna zmienic styl aplikacji
        }

        private void Nowy(object s, RoutedEventArgs e) // Funkcja która pozwoli stworzyc nowy plik, trzeba jednak wpierw wybrac folder w którym się zapisze i nazwe
        {   
            if (pierwszyraz == false)
            {
                podziel.Visibility = Visibility.Visible;
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
                   

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Blad podczas zapisywania pliku:");
                }
            }
        }
        private void Nowy2(object s, RoutedEventArgs e) // Funkcja która pozwoli stworzyc nowy plik, trzeba jednak wpierw wybrac folder w którym się zapisze i nazwe
        {
            Zapisz_Jako_2.Visibility = Visibility.Visible;
            tempo2.Visibility = Visibility.Hidden;
            temp2.Visibility = Visibility.Visible;
            var zapissciezka = new Microsoft.Win32.SaveFileDialog();
            zapissciezka.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (zapissciezka.ShowDialog() == true)
            {
                sciezka2 = zapissciezka.FileName;

                try
                {
                    File.WriteAllText(sciezka2, string.Empty);
                    temp2.Text = string.Empty;
                    czyzmiana2 = temp2.Text;
                  

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Blad podczas zapisywania pliku:");
                }
            }
        }

        private void Podziel(object s, RoutedEventArgs e) //funkcja do zaimplementowania w bliskiej przyszłości
        {
            //podzial na 2 osobne okna nad którymi mozna pracowac
            nowy2.Visibility = Visibility.Visible;
            podzial = true;
            podziel.Visibility = Visibility.Hidden;
            okno.Visibility=Visibility.Hidden;
            temp1.Text = okno.Text;
            okno.Name = "oknowyl";
            otworz2.Visibility = Visibility.Visible;
            tempo2.Visibility = Visibility.Visible;
            temp1.Visibility = Visibility.Visible;
            temp1.Name = "okno";
        }
    }
}


