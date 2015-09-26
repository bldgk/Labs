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
    /// Interaction logic for WindowCalc.xaml
    /// </summary>
    /// 
    public partial class WindowCalc : Window
    {
        public WindowCalc()
        {
            InitializeComponent();

        }
        
        private void btn_7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_9_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bnt_remainder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Expression Expression = new Expression(Convert.ToInt32(tb_Base.Text), Convert.ToInt32(tb_Degree.Text), Convert.ToInt32(tb_Divider.Text));
                tb_remainder.Text = Expression.GetRemainder().ToString();
            }
            catch { }
        }
    
    }
    public class Expression
    {
        Int32 Base { get; set; }
        Int32 Degree { get; set; }
        Int32 Remainder { get; set; }
        Int32 Divider { get; set; }
        List<Char> DegreeBinary { get; set; }


        public Expression()
        {
            this.Base = 0;
            this.Degree = 0;
            this.Divider = 0;
            this.Remainder = 0;
            this.DegreeBinary = Convert.ToString(this.Degree, 2).ToList();
        }
        public Expression(Int32 Base, Int32 Degree, Int32 Divider)
        {
            this.Base = Base;
            this.Degree = Degree;
            this.Divider = Divider;
            this.Remainder = this.Base;
            this.DegreeBinary = Convert.ToString(this.Degree, 2).ToList();
        }
        public Int32 GetRemainder()
        {
            String B_row = String.Empty;
            String A_row = Base.ToString();

            for (Int32 i = 1; i < DegreeBinary.Count(); i++)
            {

                Remainder = NextRemainder(Remainder, DegreeBinary[i]);
                A_row += Remainder.ToString();
                B_row += DegreeBinary[i];
            }
            return Remainder;
        }

        public Int32 NextRemainder(Int32 CurrentRemainder, Char B)
        {
            if (B == '1')
                return (Int32)(Math.Pow(CurrentRemainder, 2) * Base) % Divider;
            else
                return (Int32)(Math.Pow(CurrentRemainder, 2)) % Divider;

        }
    }
}
