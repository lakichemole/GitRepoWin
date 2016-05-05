using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GitRepo.UI.Common
{
    public class BooleanToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
            {

                return (value is bool) && (bool)value ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return (value is bool) && (bool)value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value is Visibility) && (Visibility)value == Visibility.Visible && parameter != null ? true : false;
        }
    }
}
