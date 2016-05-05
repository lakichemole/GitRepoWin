using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asto.GitApi.DataModel
{
    public partial class UserModel
    {
        public const string UserFilename = "users.json";

        private string _ImageUrl;

        public string ImageUrl
        {
            get { return _ImageUrl; }
        }

    }
}
