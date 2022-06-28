using System;
using System.Windows;
using System.Windows.Controls;

namespace SAE_Program.UserControls
{
    /// <summary>
    /// Логика взаимодействия для NumberBox1.xaml
    /// </summary>
    public partial class NumberBox1 : UserControl
    {
        public static readonly DependencyProperty NumberProp;

        static NumberBox1()
        {
            NumberProp = DependencyProperty.Register(
            nameof(Number2),
            typeof(string),
            typeof(NumberBox1),
            new FrameworkPropertyMetadata("asdsa") { IsNotDataBindable = false, BindsTwoWayByDefault = true }); ;
            var a = new FrameworkPropertyMetadata();
        }
        //bool IsFloatNumber { get => Number2 is float || Number2 is double || Number2 is decimal; }
        public string Number2
        {
            get => (string)GetValue(NumberProp);
            set
            {
                SetValue(NumberProp, value);
            }
        }

        public NumberBox1()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ValidationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var num = Number2.ToString("", NumberFormatInfo.InvariantInfo);
            //if (!num.Contains('.'))
            //{
            //    num += ".0";
            //}
            //((ValidationTextBox)sender).Text = num;
        }
    }
}