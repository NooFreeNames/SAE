using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title2", typeof(string), typeof(MainWindow), new FrameworkPropertyMetadata("asdasda"));

        public event PropertyChangedEventHandler? PropertyChanged;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViweModel();
        }

        private void UserControl1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((ViweModel)DataContext).Title2 = "sadasdsadasdasd";
        }
    }

    public class ViweModel: INotifyPropertyChanged
    {
        string title = "as";
        public string Title2
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title2)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
