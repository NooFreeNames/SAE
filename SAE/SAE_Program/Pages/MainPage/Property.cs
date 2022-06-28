using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SAE_Program.Pages
{
    public class Property
    {
        public Property(string name, object? value, string? description = null)
        {
            Name = name;
            Value = value;
            Description = description;
        }

        public string Name { get; set; }
        public object? Value { get; set; }
        public string? Description { get; set; }
        public bool HasValue
        {
            get { return Value != null; }
        }
        public bool HasDescription
        {
            get { return Description != null; }
        }
    }
}
