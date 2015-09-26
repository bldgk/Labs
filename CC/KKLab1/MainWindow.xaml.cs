using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using CryptographyLabrary;

namespace KKLab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WindowTypeEncrypt WindowTypeEncrypt;
        WindowCalc WindowCalc;


        Cipher Cipher;
        TextBox textbox = null;
        public MainWindow()
        {
            InitializeComponent();
        }



        private void button_encrypt_Click(object sender, RoutedEventArgs e)
        {


        }


        private void mi_typeencrypt_Click(object sender, RoutedEventArgs e)
        {
            string s = "00000000000000010000001000001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001000010010000100100001001";
            MessageBox.Show(Convert.ToString(s.Length));

            WindowTypeEncrypt = new WindowTypeEncrypt();
            WindowTypeEncrypt.Owner = this;
            WindowTypeEncrypt.ShowDialog();
            if (WindowTypeEncrypt.DialogResult.HasValue && WindowTypeEncrypt.DialogResult.Value)
            {
                if (WindowTypeEncrypt.radioButton_cezar.IsChecked == true)
                {
                    Cipher = new Cesar();
                }
                else if (WindowTypeEncrypt.radioButton_tritemius.IsChecked == true)
                {
                    Cipher = new Tritemius();
                }
                else if (WindowTypeEncrypt.radioButton_xor.IsChecked == true)
                {
                    Cipher = new XOR();
                }
                else if (WindowTypeEncrypt.radioButton_Cycle.IsChecked == true)
                {
                    Cipher = new Cycle("1 0 1 1");
                }
                else if (WindowTypeEncrypt.radioButton_shtirl.IsChecked == true)
                {
                    Cipher = new Shtirliz();
                }
                else if (WindowTypeEncrypt.radioButton_des.IsChecked == true)
                {
                    Cipher = new DES();
                }
                Cipher.Alphabet = WindowTypeEncrypt.AlphabetString.ToArray();
            }
        }
        #region File
        private void mi_newfile_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabitem = null;
            Grid tabitem_grid = null;


            try
            {
                textbox = new TextBox();
                textbox.TextWrapping = TextWrapping.Wrap;
                textbox.AcceptsReturn = true;

                tabitem_grid = new Grid();
                tabitem_grid.Children.Add(textbox);

                tabitem = new TabItem();
                tabitem.Header = "Untitled";
                tabitem.Content = tabitem_grid;

                tabControl_files.Items.Add(tabitem);
                tabControl_files.SelectedItem = tabitem;
            }
            catch { }
        }
        
        private void mi_savefile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                using (StreamWriter file = new System.IO.StreamWriter(dialog.FileName))
                {
                    file.WriteLine(Cipher.Encryption(textbox.Text.ToLower()));
                }
            }

        }

        private void mi_openfile_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabitem = null;
            Grid tabitem_grid = null;


            try
            {
                string filename = "";
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Text documents (.txt)|*.txt";

                Nullable<bool> result = dialog.ShowDialog();

                if (result == true)
                {
                    filename = dialog.SafeFileName;
                }

                textbox = new TextBox();
                textbox.TextWrapping = TextWrapping.Wrap;
                textbox.AcceptsReturn = true;

                tabitem_grid = new Grid();
                tabitem_grid.Children.Add(textbox);

                tabitem = new TabItem();
                tabitem.Header = filename;
                tabitem.Content = tabitem_grid;

                tabControl_files.Items.Add(tabitem);
                tabControl_files.SelectedItem = tabitem;
                using (StreamReader file = new StreamReader(dialog.FileName))
                {
                    textbox.Text = Cipher.Decryption(file.ReadLine());

                }
            }
            catch { }
        }
        #endregion
        private void mi_calculator_Click(object sender, RoutedEventArgs e)
        {
            WindowCalc = new WindowCalc();
            WindowCalc.Show();
        }
    }
}