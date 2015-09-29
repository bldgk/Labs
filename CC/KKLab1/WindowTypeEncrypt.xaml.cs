using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace KKLab1
{
    /// <summary>
    /// Interaction logic for WindowTypeEncrypt.xaml
    /// </summary>
    public partial class WindowTypeEncrypt : Window
    {
        public String AlphabetString
        {
            get; set;
        }

       // public 
        public WindowTypeEncrypt()
        {
            InitializeComponent();
        }
        private void radioButton_kirilica_Checked(object sender, RoutedEventArgs e)
        {
            AlphabetString = "абвгдеёжзийклмнопрстуфхцчшщьыъэюя ";//"абвгдеєэжзиійклмнопрстуфхцчшщьъюя ";
            
        }

        private void radioButton_latin_Checked(object sender, RoutedEventArgs e)
        {
            AlphabetString = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRTSUVWXYZ";
            
        }
        private void radioButton_other_Checked(object sender, RoutedEventArgs e)
        {
            AlphabetString = textBox_alphabet.Text;
        }
        private void button_ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void radioButton_cezar_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void radioButton_tritemius_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void WindowTypeEncrypt1_Loaded(object sender, RoutedEventArgs e)
        {
            AlphabetString = "";
        }

        private void radioButton_xor_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton_Cycle_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton_shtirl_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton_des_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void radioButton_RSA_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
