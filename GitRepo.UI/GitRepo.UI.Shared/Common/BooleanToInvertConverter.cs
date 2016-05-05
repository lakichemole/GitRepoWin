using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GitRepo.UI.Common
{
    public class BooleanToInvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool) ? !((bool)value) : false;
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value is bool) ? !((bool)value) : false;
        }
        
    }
}
