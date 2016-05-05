using GitRepo.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GitRepo.UI.ViewModels.PageKeys;

namespace GitRepo.UI.Pages
{
    public class ViewLocator : IViewLocator
    {

        #region Contructor

        private ViewLocator()
        {
        }
        #endregion
        #region SingleTon

        private static volatile ViewLocator instance;
        private static object syncRoot = new Object();

        public static ViewLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ViewLocator();
                    }
                }

                return instance;
            }

        }
        #endregion

        public Type GetViewFromViewKey(int viewKey) {
            switch (viewKey)
            {
                case (int)Views.AddUser:
                    return typeof(AddUserPage);
                case (int)Views.Repository:
                    return typeof(RepositoryPage);
                case (int)Views.Main:
                default:
                    return typeof(MainPage);
            }
        }
    }
}
