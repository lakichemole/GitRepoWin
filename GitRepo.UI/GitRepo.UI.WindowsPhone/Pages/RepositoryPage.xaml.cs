using GitRepo.UI.Common;
using GitRepo.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GitRepo.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RepositoryPage : PageBase
    {

        public RepositoryPage()
        {
            this.InitializeComponent();
            this.DataContext = new RepositoryViewModel();
            this.Init(this.DataContext as ViewModelBase);
            repoListView.Loaded += RepoListView_Loaded;
        }

        private void RepoListView_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer viewer = GetScrollViewer(repoListView);
            viewer.ViewChanged += RepositoryPage_ViewChanged;
        }

        // method to pull out a ScrollViewer
        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer) return depObj as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }
        
        private void RepositoryPage_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            ScrollViewer view = (ScrollViewer)sender;
            double progress = view.VerticalOffset / view.ScrollableHeight;
            System.Diagnostics.Debug.WriteLine(progress);
            if (progress > 0.7)
            {
                ((RepositoryViewModel)this.CurrentViewModel).LoadMoreRepositories();
            }
        }




    }
}
