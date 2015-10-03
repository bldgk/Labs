using System;
using System.Windows;
using CryptographyLabrary;

namespace KKLab1
{
    /// <summary>
    /// Interaction logic for WindowCalc.xaml
    /// </summary>
    /// 
    public partial class WindowCalc : Window
    {
        public WindowCalc()
        {
            InitializeComponent();

        }
      


        private void bnt_remainder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ModCalculator Calculation = new ModCalculator(Convert.ToInt64(tb_Base.Text), Convert.ToInt64(tb_Degree.Text), Convert.ToInt64(tb_Divider.Text));
                tb_remainder.Text = Calculation.GetRemainder().ToString();
            }
            catch { }
        }
    }
}