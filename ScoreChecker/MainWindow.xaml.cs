// James Odeyale

using System.Windows;

namespace ScoreChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM vm = new VM();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void CrunchData_Click(object sender, RoutedEventArgs e)
        {
            vm.Crunch();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            vm.Clear();
        }
    }
}
