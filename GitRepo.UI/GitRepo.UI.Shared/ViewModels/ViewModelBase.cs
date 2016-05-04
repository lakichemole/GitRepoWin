using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GitRepo.UI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        #region Methods

        public abstract Task LoadDataAsync(object parameter);

        #endregion
        #region Properties

        public INavigationService NavigationService { get; set; }

        private bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                if (_IsLoading != value)
                {
                    _IsLoading = value;
                    NotifyPropertyChanged(() => IsLoading);
                }
            }
        }

        private bool _IsDataLoaded;

        public bool IsDataLoaded
        {
            get { return _IsDataLoaded; }
            set {
                if (_IsDataLoaded != value)
                {
                    _IsDataLoaded = value;
                    NotifyPropertyChanged(() => IsDataLoaded);
                }
            }
        }


        #endregion

        #region Propertychanged

        protected void NotifyPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            NotifyPropertyChanged(propertyName);
        }

        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
