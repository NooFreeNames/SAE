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

namespace ControlLib
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class InfoPanel : UserControl
    {
        public InfoPanel()
        {
            InitializeComponent();
        }

        public void Add(InfoPanelItem item)
        {
            var thk = new Thickness();
            
            var propName = new TextBox
            {
                Text = item.PropName,
                BorderThickness = thk,
                IsReadOnly = true,
                FontSize = 20,
                Padding = new Thickness { Left = 5 }
            };

            var propValue = new TextBox()
            {
                Text = item.PropVal?.ToString(),
                BorderThickness = thk,
                IsReadOnly = true,
                FontSize = 20,
                Padding = new Thickness { Left = 5 }
            };

            if (item.DescPropVal != null)
            {
                var tt = new ToolTip();
                tt.Content = item.DescPropVal;
                tt.Width = 200;

                var toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = item.PropVal?.ToString(), FontSize = 18, FontWeight = FontWeights.Bold });
                toolTipPanel.Children.Add(new TextBlock { Text = item.DescPropVal, TextWrapping = TextWrapping.Wrap, FontSize = 14 });
                tt.Content = toolTipPanel;

                propValue.ToolTip = tt;
                propValue.Foreground = new SolidColorBrush(Color.FromRgb(0, 60, 200));

            }

            PropNameList.Children.Add(propName);
            PropValList.Children.Add(propValue);
        }

        public void Clear()
        {
            PropNameList.Children.Clear();
            PropValList.Children.Clear();
        }
    }


}

