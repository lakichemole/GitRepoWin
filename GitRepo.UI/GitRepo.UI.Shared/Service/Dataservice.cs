using Asto.GitApi.DataModel;
using GitRepo.UI.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace GitRepo.UI.Service
{
    public sealed class DataServiceManager
    {
        #region Private variables

        #endregion

        #region Contructor

        private DataServiceManager()
        {
        }
        #endregion
        #region SingleTon

        private static volatile DataServiceManager instance;
        private static object syncRoot = new Object();

        public static DataServiceManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataServiceManager();
                    }
                }

                return instance;
            }

        }
        #endregion

        #region Methods
        public async Task<List<UserModel>> GetSavedUser()
        {
            List<UserModel> _UserList = new List<UserModel>();
            var userStr = await StorageManager.Instance.ReadStringFromLocalFile(UserModel.UserFilename);
            if (!string.IsNullOrEmpty(userStr))
            {
                await Task.Factory.StartNew(() =>
                {
                    UserModel[] users = JsonConvert.DeserializeObject<UserModel[]>(userStr);
                    foreach (var user in users)
                    {
                        _UserList.Add(user);
                    }
                    return _UserList;
                });

            }
            return _UserList;
        }

        public async Task AddSavedUser(UserModel user)
        {
            var _UserList = await this.GetSavedUser();
            if (_UserList != null && !_UserList.Exists((usr) => { return usr.login == user.login; }))
            {
                _UserList.Add(user);
                await Task.Factory.StartNew(async () =>
                {
                    string content = JsonConvert.SerializeObject(_UserList.ToArray());
                    await StorageManager.Instance.SaveStringToLocalFile(UserModel.UserFilename, content);
                });

            }
        }

        public async Task<List<RepositoryModel>> GetRepositoriesFromUser(string login, int page, int pageSize)
        {
            return await Asto.GitApi.Service.DataServiceManager.Instance.GetRepositoriesFromUser(login, page, pageSize);
        }

        public async Task<UserModel> GetUserByLogin(string login)
        {
            return await Asto.GitApi.Service.DataServiceManager.Instance.GetUserByLogin(login);
        }
        #endregion



    }
}
