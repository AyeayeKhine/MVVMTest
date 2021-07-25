using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MVVMTest.Controls
{
    public class SampleValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            var textValue = args.NewTextValue;
            bool isValid = !string.IsNullOrEmpty(textValue) && textValue.Length >= 5;
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
            ((Entry)sender).BackgroundColor = isValid ? Color.Default : Color.Red;
        }
    }

}
