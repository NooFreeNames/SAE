using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SAE_DB;

namespace SAE_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoPage.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        public InfoPage(CelestialObject? celestialObject = null)
        {
            InitializeComponent();
            DataContext = new ViewingPageViewModel();
        }
        public CustomPopupPlacement[] PlaceToolTip(Size popupSize, Size targetSize, Point offset)
        {
            var placement1 = new CustomPopupPlacement(
                new Point(offset.X, offset.Y + targetSize.Height),
                PopupPrimaryAxis.Vertical);

            var placement2 = new CustomPopupPlacement(
                new Point(offset.X, -offset.Y - popupSize.Height),
                PopupPrimaryAxis.Horizontal);

            var ttplaces = new CustomPopupPlacement[] { placement1, placement2 };

            return ttplaces;
        }
    }
}
