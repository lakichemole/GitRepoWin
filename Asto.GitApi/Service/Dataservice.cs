using Asto.GitApi.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Asto.GitApi.Service
{
    public sealed class DataServiceManager
    {
        #region Private variables
        private const string Host = "https://api.github.com";
        private struct JsonResponse
        {
            public string Data;
            public WebHeaderCollection Headers;
            public bool HaveError;
            public string ErrorMessage;
        }

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
        private async Task<JsonResponse> GetJson(string url)
        {
            try
            {

                var request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                request.Method = "GET";
                request.Accept = "application/json";
                WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request);
                var responseStream = responseObject.GetResponseStream();
                using (var sr = new StreamReader(responseStream))
                {

                    return new JsonResponse() { Data = await sr.ReadToEndAsync(), Headers = responseObject.Headers };
                }
            }
            catch (Exception ex)
            {

                return await Task.FromResult<JsonResponse>(new JsonResponse() { HaveError = true, ErrorMessage = ex.Message });
            }
        }

        public async Task<UserModel> GetUserByLogin(string Login)
        {
            var result = await GetJson(string.Format("{0}/users/{1}", Host, Login));
            if (result.HaveError)
            {
                return null;
            }
            else {
                return JsonConvert.DeserializeObject<UserModel>(result.Data);
            }
        }

        public async Task<List<RepositoryModel>> GetRepositoriesFromUser(string Login,int page, int pageSize)
        {
            var result = await GetJson(string.Format("{0}/users/{1}/repos?page={2}&per_page={3}", Host, Login, page,pageSize));
            if (result.HaveError)
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<List<RepositoryModel>>(result.Data);
            }
        }
        #endregion



    }
}
