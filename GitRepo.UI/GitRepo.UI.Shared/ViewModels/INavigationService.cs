using System;
using System.Collections.Generic;
using System.Text;

namespace GitRepo.UI.ViewModels
{
    public interface INavigationService
    {
        void Navigate(int pageId, object parameter = null);

        void GoBack();
    }
}
