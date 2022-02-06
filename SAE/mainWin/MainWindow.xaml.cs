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

namespace mainWin
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            const float sizeFactor = 0.7F;
            double ScreenW = SystemParameters.PrimaryScreenWidth;
            double ScreenH = SystemParameters.PrimaryScreenHeight;

            /*Setting the window size*/
            MainWin.Width = ScreenW * sizeFactor;
            MainWin.Height = ScreenH * sizeFactor;

            /*Setting the window in the center of the screen*/
            MainWin.Left = (ScreenW - MainWin.Width) / 2;
            MainWin.Top = (ScreenH - MainWin.Height) / 2;
        }
    }

}
