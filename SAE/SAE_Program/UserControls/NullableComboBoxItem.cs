using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SAE_Program.UserControls
{
    /// <summary>
    /// Extends the <see cref="ComboBoxItem"/> class.
    /// </summary>
    [Browsable(false)]
    public class NullableComboBoxItem : ComboBoxItem
    {
        /// <summary>
        /// Gets or sets a <see cref="bool"/> value that indicates if this item is highlighted.
        /// </summary>
        public new bool IsHighlighted
        {
            get { return base.IsHighlighted; }
            set { base.IsHighlighted = value; }
        }
    }
}
