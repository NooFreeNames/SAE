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
using System.Threading;

namespace text
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            string text = "Нахско - дагестанские языки(также восточнокавказские) — языковая семья, распространённая в восточной части Северного Кавказа (в Дагестане, Чечне и Ингушетии), отчасти в Азербайджане и Грузии, а также в диаспорах разных стран. По разным оценкам, число говорящих на языках семьи варьируется от 2,6 до 4,3 миллионов человек. На некоторых из них говорит не более нескольких сотен человек. Нахско-дагестанские языки разделились из общего праязыка к концу III тысячелетия до нашей эры. За своё существование они подверглись значительному влиянию иранских и тюркских языков, а также арабского и, с XX века, русского. Существует несколько гипотез, объединяющих нахско-дагестанские семьи в макросемьи, в том числе с другими языками Кавказа, но ни одна из них не является общепризнанной. Вследствие географических и культурных особенностей региона дагестанские языки существовали в относительной изоляции друг от друга, что привело к значительному языковому разнообразию. Большинство нахско-дагестанских языков бесписьменные. Генеалогическая классификация внутри семьи и разделение на языки и диалекты являются предметом научных дискуссий. Традиционно семья делится на шесть ветвей. Нахско-дагестанские языки выделяются сравнительно богатым набором согласных и широким распространением фарингализации. В грамматике отличительными чертами семьи являются эргативно-абсолютивное кодирование аргументов глагола, две системы падежей (обычных и локативных) и наличие категории именного класса.";
            int start = 0;
            const int count = 10;
            Show();
            string text2;
            for (int i = 0; i < text.Length / count; i += count)
            {
                text2 = text.Substring(i, i + count);

                InvalidateVisual();
                Thread.Sleep(200);
                
            }
        }
    }
}
