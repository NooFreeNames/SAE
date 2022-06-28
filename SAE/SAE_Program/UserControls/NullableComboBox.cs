using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SAE_Program.UserControls
{
    /// <summary>
    /// Extends the <see cref="ComboBox"/> class
    /// </summary>
    public class NullableComboBox : ComboBox
    {

        private NullableComboBoxItem _comboBoxNullItem;
        private ScrollViewer _scrollViewer;

        /// <summary>
        /// Identifies the <see cref="NullItemTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NullItemTemplateProperty =
            DependencyProperty.Register("NullItemTemplate", typeof(DataTemplate), typeof(NullableComboBox));

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> that is used to 
        /// visualize a null value in the dropdown.
        /// This is a dependency property.
        /// </summary>
        [Category("Common")]
        public DataTemplate NullItemTemplate
        {
            get { return (DataTemplate)GetValue(NullItemTemplateProperty); }
            set { SetValue(NullItemTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="SelectionBoxTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionBoxTemplateProperty =
            DependencyProperty.Register("SelectionBoxTemplate", typeof(DataTemplate), typeof(NullableComboBox));

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> that is used to 
        /// visualize a item in the selection box
        /// This is a dependency property.
        /// </summary>
        [Category("Common")]
        public DataTemplate SelectionBoxTemplate
        {
            get { return (DataTemplate)GetValue(SelectionBoxTemplateProperty); }
            set { SetValue(SelectionBoxTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="SelectionBoxNullItemTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionBoxNullItemTemplateProperty =
            DependencyProperty.Register("SelectionBoxNullItemTemplate", typeof(DataTemplate), typeof(NullableComboBox));

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate"/> that is used to 
        /// visualize a item in the selection box
        /// This is a dependency property.
        /// </summary>
        [Category("Common")]
        public DataTemplate SelectionBoxNullItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectionBoxNullItemTemplateProperty); }
            set { SetValue(SelectionBoxNullItemTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="SelectionBoxNullValueText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectionBoxNullValueTextProperty =
            DependencyProperty.Register("SelectionBoxNullValueText", typeof(string), typeof(NullableComboBox),
                                        new FrameworkPropertyMetadata("None"));

        /// <summary>
        /// Gets or sets the text that is used to represent 
        /// a null value in selectionbox of the combobox.
        /// This is a dependency property.
        /// </summary>
        [Category("Common")]
        public string SelectionBoxNullValueText
        {
            get { return (string)GetValue(SelectionBoxNullValueTextProperty); }
            set { SetValue(SelectionBoxNullValueTextProperty, value); }
        }




        /// <summary>
        /// Identifies the <see cref="NullValueText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NullValueTextProperty =
            DependencyProperty.Register("NullValueText", typeof(string), typeof(NullableComboBox),
                                        new FrameworkPropertyMetadata("None"));

        /// <summary>
        /// Gets or sets the text that is used to represent a null value 
        /// in the dropdown portion of the combobox.
        /// This is a dependency property.
        /// </summary>
        [Category("Common")]
        public string NullValueText
        {
            get { return (string)GetValue(NullValueTextProperty); }
            set { SetValue(NullValueTextProperty, value); }
        }




        private static readonly DependencyPropertyKey HighlightedItemPropertyKey =
            DependencyProperty.RegisterReadOnly("HighlightedItemProperty", typeof(object), typeof(ComboBox),
                                                new FrameworkPropertyMetadata(null));
        /// <summary>
        /// Identifies the <see cref="HighlightedItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HighlightedItemProperty =
            HighlightedItemPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets a <see cref="bool"/> value that indicates if the null item is highlighted.
        /// </summary>
        [Browsable(false)]
        public object HighlightedItem
        {
            get { return GetValue(HighlightedItemProperty); }
            private set { SetValue(HighlightedItemPropertyKey, value); }
        }


        static NullableComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NullableComboBox), new FrameworkPropertyMetadata(typeof(NullableComboBox)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            var comboBoxItem = new NullableComboBoxItem();
            RegisterEventHandlerForWhenIsHighlightedChanges(comboBoxItem);
            return comboBoxItem;
        }

        private void RegisterEventHandlerForWhenIsHighlightedChanges(NullableComboBoxItem comboBoxItem)
        {
            DependencyPropertyDescriptor.FromProperty(ComboBoxItem.IsHighlightedProperty, typeof(NullableComboBoxItem)).
                    AddValueChanged(comboBoxItem, (o, e) => OnComboBoxItemHighlighted(o as NullableComboBoxItem));
        }


        private void OnComboBoxItemHighlighted(NullableComboBoxItem comboBoxItem)
        {
            HighlightedItem = comboBoxItem.DataContext;
            RemoveHighlightFromComboBoxNullItem();
        }

        private void RemoveHighlightFromComboBoxNullItem()
        {
            _comboBoxNullItem.IsHighlighted = false;
        }



        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GetComboBoxNullItemFromTemplate();
            RegisterEventHandlersForComboBoxNullItem();

            GetScrollViewerFromTemplate();
            RegisterEventHandlersForScrollViewer();

        }

        private void RegisterEventHandlersForScrollViewer()
        {
            _scrollViewer.KeyDown += (o, e) => OnScrollViewerKeyDown(e);
        }


        private void RegisterEventHandlersForComboBoxNullItem()
        {
            _comboBoxNullItem.AddHandler(MouseEnterEvent,
                                         new MouseEventHandler((o, e) => OnComboBoxNullItemMouseEnter()),
                                         handledEventsToo: true);
            _comboBoxNullItem.AddHandler(MouseLeftButtonDownEvent,
                                         new MouseButtonEventHandler((o, e) => OnComboBoxNullItemMouseLeftButtonDown()),
                                         handledEventsToo: true);
        }

        private void OnComboBoxNullItemMouseLeftButtonDown()
        {
            ClearSelectedItem();
            IsDropDownOpen = false;
        }


        private void GetComboBoxNullItemFromTemplate()
        {
            _comboBoxNullItem = GetTemplateChild("PART_NullValue") as NullableComboBoxItem;
        }

        private void GetScrollViewerFromTemplate()
        {
            _scrollViewer = GetTemplateChild("DropDownScrollViewer") as ScrollViewer;

        }

        protected override void OnKeyDown(KeyEventArgs keyEventArgs)
        {
            if (!IsDropDownOpen)
            {
                if (ArrowKeyDownWasPressed(keyEventArgs) || ArrowKeyRightWasPressed(keyEventArgs))
                    HandleArrowKeyDownOrRight(keyEventArgs);
                if (ArrowKeyUpWasPressed(keyEventArgs) || ArrowKeyLeftWasPressed(keyEventArgs))
                    HandleArrowKeyUpOrLeft(keyEventArgs);
            }
            if (!keyEventArgs.Handled)
                base.OnKeyDown(keyEventArgs);
        }

        private void HandleArrowKeyUpOrLeft(KeyEventArgs keyEventArgs)
        {
            if (IsFirstItemSelected)
            {
                ClearSelectedItem();
            }
        }

        private void HandleArrowKeyDownOrRight(KeyEventArgs keyEventArgs)
        {
            if (IsNothingSelected && HasItems)
            {
                SelectFirstItem();
                IndicateThatTheKeyEventHasBeenHandled(keyEventArgs);
            }
        }

        protected bool IsNothingSelected
        {
            get { return SelectedItem == null; }

        }

        private void ClearSelectedItem()
        {
            SelectedItem = null;
        }

        protected bool IsFirstItemSelected
        {
            get { return SelectedIndex == 0; }

        }



        private void SelectFirstItem()
        {
            SelectedIndex = 0;
        }


        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }

        private void OnScrollViewerKeyDown(KeyEventArgs keyEventArgs)
        {
            if (ArrowKeyDownWasPressed(keyEventArgs))
                HandleScrollViewerArrowKeyDown(keyEventArgs);
            if (ArrowKeyUpWasPressed(keyEventArgs))
                HandleScrollViewerArrowKeyUp(keyEventArgs);
            if (EnterKeyWasPressed(keyEventArgs))
            {
                HandleScrollViewerEnterKey();
            }

        }

        private void HandleScrollViewerEnterKey()
        {
            if (IsComboBoxNullItemHighlighted)
            {
                ClearSelectedItem();
                IsDropDownOpen = false;
            }
        }

        private void HandleScrollViewerArrowKeyDown(KeyEventArgs keyEventArgs)
        {
            if (IsComboBoxNullItemHighlighted && HasItems)
            {
                RemoveHighlightFromComboBoxNullItem();
                HighlightTheFirstComboBoxItem();
                IndicateThatTheKeyEventHasBeenHandled(keyEventArgs);
            }
        }

        private void HandleScrollViewerArrowKeyUp(KeyEventArgs keyEventArgs)
        {
            if (IsFirstComboBoxItemIsHighLighted)
            {
                RemoveHighlightFromCurrentlyHighlightedItem();
                HighlightNullItem();
                IndicateThatTheKeyEventHasBeenHandled(keyEventArgs);
            }
        }

        protected bool IsFirstComboBoxItemIsHighLighted
        {
            get
            {
                if (HighlightedItem == null)
                    return false;
                NullableComboBoxItem comboBoxItem = GetHighlightedComboBoxItem();
                return ItemContainerGenerator.IndexFromContainer(comboBoxItem) == 0;
            }

        }




        private void IndicateThatTheKeyEventHasBeenHandled(KeyEventArgs keyEventArgs)
        {
            keyEventArgs.Handled = true;
        }

        private void HighlightTheFirstComboBoxItem()
        {
            var NullableComboBoxItem =
                (NullableComboBoxItem)ItemContainerGenerator.ContainerFromIndex(0);
            NullableComboBoxItem.IsHighlighted = true;
            NullableComboBoxItem.Focus();
        }

        private static bool ArrowKeyDownWasPressed(KeyEventArgs keyEventArgs)
        {
            return (keyEventArgs.Key == Key.Down);
        }

        private static bool ArrowKeyUpWasPressed(KeyEventArgs keyEventArgs)
        {
            return (keyEventArgs.Key == Key.Up);
        }

        private static bool ArrowKeyLeftWasPressed(KeyEventArgs keyEventArgs)
        {
            return (keyEventArgs.Key == Key.Left);
        }

        private static bool ArrowKeyRightWasPressed(KeyEventArgs keyEventArgs)
        {
            return (keyEventArgs.Key == Key.Right);
        }

        private static bool EnterKeyWasPressed(KeyEventArgs keyEventArgs)
        {
            return (keyEventArgs.Key == Key.Enter);
        }

        protected bool IsComboBoxNullItemHighlighted
        {
            get { return _comboBoxNullItem.IsHighlighted; }

        }


        private void OnComboBoxNullItemMouseEnter()
        {
            _comboBoxNullItem.BringIntoView();
            RemoveHighlightFromCurrentlyHighlightedItem();
            HighlightNullItem();
        }

        protected override void OnDropDownOpened(EventArgs e)
        {
            if (IsNothingSelected)
                HighlightNullItem();
            base.OnDropDownOpened(e);
        }


        private void RemoveHighlightFromCurrentlyHighlightedItem()
        {
            if (HighlightedItem != null)
            {
                NullableComboBoxItem highlightedComboBoxItem = GetHighlightedComboBoxItem();
                highlightedComboBoxItem.IsHighlighted = false;
            }
        }

        private NullableComboBoxItem GetHighlightedComboBoxItem()
        {
            return (NullableComboBoxItem)ItemContainerGenerator.ContainerFromItem(HighlightedItem);
        }

        private void HighlightNullItem()
        {
            _comboBoxNullItem.IsHighlighted = true;
            _comboBoxNullItem.Focus();
        }
    }
}
