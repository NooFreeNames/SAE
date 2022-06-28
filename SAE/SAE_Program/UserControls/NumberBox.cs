using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SAE_Program.UserControls
{
    public class NumberBox : ValidationTextBox
    {
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (Text.Length == 9)
            {
                
            }
            
        }
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            var a = Text;
        }
    }
}
