// Group 1 - James Odeyale, Mark Dracopoulos, Ashok Chakravarthi, Aravind Kumar

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
