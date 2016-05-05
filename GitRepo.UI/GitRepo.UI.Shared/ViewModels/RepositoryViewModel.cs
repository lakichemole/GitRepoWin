using Asto.GitApi.DataModel;
using GitRepo.UI.Common;
using GitRepo.UI.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GitRepo.UI.ViewModels
{
    public class RepositoryViewModel : ViewModelBase
    {

        private const int pageSize = 10;
        private int currentPage { get; set; }

        private bool noMoreResult { get; set; }

        public override async Task LoadDataAsync(object parameter = null)
        {
            if (!this.IsDataLoaded)
            {
                if (parameter != null && parameter is UserModel)
                {
                    CurrentUser = (UserModel)parameter;
                    currentPage = 0;
                    noMoreResult = false;
                    Title = string.Format(this.ResourceLoader.GetString("RepositoryTitle"), CurrentUser.login);
                    _RepoList.Clear();
                    LoadMoreRepositories();
                }
            }
            this.IsDataLoaded = true;
        }

        public async void LoadMoreRepositories() {
            if (!this.IsLoading && !noMoreResult)
            {
                this.IsLoading = true;
                var repos = await DataServiceManager.Instance.GetRepositoriesFromUser(CurrentUser.login, currentPage++, pageSize);
                foreach (var repo in repos)
                {
                    _RepoList.Add(repo);
                }
                if (repos.Count < pageSize)
                {
                    noMoreResult = true;
                }
                this.IsLoading = false;
            }
        }

        private UserModel _CurrentUser;

        public UserModel CurrentUser
        {
            get { return _CurrentUser; }
            set { _CurrentUser = value; }
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                NotifyPropertyChanged(() => Title);
            }
        }



        private ObservableCollection<RepositoryModel> _RepoList = new ObservableCollection<RepositoryModel>();

        public ObservableCollection<RepositoryModel> RepoList
        {
            get { return _RepoList; }
            set { _RepoList = value; }
        }

        private RelayCommand _ItemCommand;

        public RelayCommand ItemCommand
        {
            get
            {
                if (_ItemCommand == null)
                {
                    _ItemCommand = new RelayCommand(async (item) =>
                    {
                        var url = ((RepositoryModel)item).html_url;
                        if (!string.IsNullOrEmpty(url))
                        {
                            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
                        }
;
                    });
                }
                return _ItemCommand;
            }
        }

    }
}
