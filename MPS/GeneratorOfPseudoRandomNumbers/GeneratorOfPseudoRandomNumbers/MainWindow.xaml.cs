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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneratorOfPseudoRandomNumbers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        IGenerator Generator;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnAnalytic_Click(object sender, RoutedEventArgs e)
        {
            Generator = new Analytic(18, 17, 8, 1, Convert.ToInt32(tb_amounttests.Text));
           	var frequencies = Generator.FrequenciesTest();
			lB_Analytic.ItemsSource = frequencies;
			tb_anhi.Text = Generator.CriterionX(frequencies).ToString();
        }

        private void btnScramble_Click(object sender, RoutedEventArgs e)
        {
            Generator = new Shuffle(Math.Sqrt(3) / 3, 6, 18, Convert.ToInt32(tb_amounttests.Text));
			var frequencies = Generator.FrequenciesTest();
			lb_Scramble.ItemsSource = frequencies;
			tb_scrhi.Text = Generator.CriterionX(frequencies).ToString();
        }
    }
}
