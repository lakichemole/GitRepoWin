using Asto.GitApi.DataModel;
using GitRepo.UI.Common;
using GitRepo.UI.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace GitRepo.UI.ViewModels
{
    public class AddUserViewModel : ViewModelBase
    {

        public override async Task LoadDataAsync(object parameter = null)
        {
            _HaveError = false;
        }
        
        private string _UserLogin = "technoweenie";

        public string UserLogin
        {
            get { return _UserLogin; }
            set
            {
                if (_UserLogin != value)
                {
                    _UserLogin = value;
                    NotifyPropertyChanged(() => UserLogin);
                }
            }
        }

        private bool _HaveError;

        public bool HaveError
        {
            get { return _HaveError; }
            set { _HaveError = value;
                NotifyPropertyChanged(() => HaveError);
            }
        }



        private RelayCommand _SearchUserCommand;

        public RelayCommand SearchUserCommand
        {
            get {
                if (_SearchUserCommand == null) {
                    _SearchUserCommand = new RelayCommand(async () => {
                        HaveError = false;
                        this.IsLoading = true;
                        var user = await DataServiceManager.Instance.GetUserByLogin(UserLogin);
                        if (user == null)
                        {
                            HaveError = true;
                        }
                        else {
                            await DataServiceManager.Instance.AddSavedUser(user);
                            this.NavigationService.GoBack();
                        }

                        this.IsLoading = false;
                    });
                }
                return _SearchUserCommand; }
        }


    }
}
