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
    public class MainViewModel : ViewModelBase
    {

        public override async Task LoadDataAsync(object parameter = null)
        {
            if (!this.IsDataLoaded)
            {
                this.IsLoading = true;
                var users = await DataServiceManager.Instance.GetSavedUser();
                _UserList.Clear();
                foreach (var user in users)
                {
                    _UserList.Add(user);
                }

                this.IsLoading = false;
            }
            this.IsDataLoaded = true;
        }


        private ObservableCollection<UserModel> _UserList = new ObservableCollection<UserModel>();

        public ObservableCollection<UserModel> UserList
        {
            get { return _UserList; }
            set { _UserList = value; }
        }

        private RelayCommand _AddUserCommand;

        public RelayCommand AddUserCommand
        {
            get {
                if (_AddUserCommand == null) {
                    _AddUserCommand = new RelayCommand(() => {
                        this.IsDataLoaded = false;
                        this.NavigationService.Navigate((int)PageKeys.Views.AddUser);
                    });
                }
                return _AddUserCommand; }
        }

        private RelayCommand _ItemCommand;

        public RelayCommand ItemCommand
        {
            get
            {
                if (_ItemCommand == null)
                {
                    _ItemCommand = new RelayCommand((item) => {
                        this.NavigationService.Navigate((int)PageKeys.Views.Repository, item as UserModel);
                    });
                }
                return _ItemCommand;
            }
        }

        


    }
}
