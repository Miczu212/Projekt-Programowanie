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
    /// <summary>
    /// Logika interakcji dla klasy ustawienia.xaml
    /// </summary>
    public partial class ustawienia : Window
    {
        string wybor="4";
       
        public ustawienia()
        {
            
            InitializeComponent();
        }

        private void RadioButton_Checked(object s, RoutedEventArgs e)
        {
            wybor = "4";
        }

        private void RadioButton_Checked_1(object s, RoutedEventArgs e)
        {
            wybor = "3";
        }

        private void RadioButton_Checked_2(object s, RoutedEventArgs e)
        {
            wybor = "2";
        }

        private void RadioButton_Checked_3(object s, RoutedEventArgs e)
        {
            wybor = "1";
        }

        private void Button_Click(object s, RoutedEventArgs e)
        {
            string path1 = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "/config.txt";
            File.WriteAllText(path1,wybor);
            MessageBox.Show("Pomyslnie zapisano");
        }
    }
}
