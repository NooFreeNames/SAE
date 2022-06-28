using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SAE_Program.UserControls
{
    public class ValidationTextBox : TextBox
    {
        string? _prevText;

        public ValidationTextBox()
        {
            _prevText = Text;
        }
        public string? DefaultValue { get; set; } = null;

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            if (string.IsNullOrWhiteSpace(Text))
            {
                Text = DefaultValue;
            }

            if (Validation.GetHasError(this))
            {
                Text = _prevText;
                CaretIndex = Text?.Length ?? default;
            }
            else
            {
                _prevText = Text;
            }
        }
    }
}
