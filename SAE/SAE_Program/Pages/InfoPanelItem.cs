using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SAE_Program.Pages
{
    public class InfoPanelItem
    {
        public InfoPanelItem(string propName, object? propVal, string? descPropVal = null)
        {
            PropName = propName;
            PropVal = propVal;
            DescPropVal = descPropVal;

            if (DescPropVal == null)
            {
                NextFG = new SolidColorBrush(Colors.Black);
                NextTT = null;
            }
            else
            {
                NextFG = new SolidColorBrush(Colors.Blue);
                var tt = new ToolTip
                {
                    Content = DescPropVal,
                    Width = 200
                };

                var toolTipPanel = new StackPanel();
                toolTipPanel.Children.Add(new TextBlock { Text = PropVal?.ToString(), FontSize = 18, FontWeight = FontWeights.Bold });
                toolTipPanel.Children.Add(new TextBlock { Text = DescPropVal, TextWrapping = TextWrapping.Wrap, FontSize = 14 });
                tt.Content = toolTipPanel;
                NextTT = tt;
            }
        }

        public string PropName { get; set; }
        public object? PropVal { get; set; }
        public string? DescPropVal { get; set; }


        public Brush NextFG { get; private set; }
        public ToolTip? NextTT { get; private set; }
    }
}
