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
                Calculation Calculation = new Calculation(Convert.ToInt64(tb_Base.Text), Convert.ToInt64(tb_Degree.Text), Convert.ToInt64(tb_Divider.Text));
                tb_remainder.Text = Calculation.GetRemainder().ToString();
            }
            catch { }
        }
    }
}