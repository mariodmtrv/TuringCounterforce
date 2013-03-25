using System;
using System.Configuration;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.Social.OAuth1;

namespace SecretCommunicator.SecretCommunicatorDAL
{
    public class DropboxDAL
    {
 
        private  static string DropboxAppKey = "";
        private static string DropboxAppSecret = "";
        public const string FolderName = "TuringCounterforce";
        private static Entry createdFolderEntry;
        public static string SrvPath;


        public static IDropbox LogInDropBox()
        {
          //  SrvPath = Request.ServerVariables["APPL_PHYSICAL_PATH"];
            if(ConfigurationManager.AppSettings["IS_ON_APPHARBOR"]=="Yes")
            {
                DropboxAppKey = ConfigurationManager.AppSettings["DropboxAppKey"];
                DropboxAppSecret = ConfigurationManager.AppSettings["DropboxAppSecret"];
            }
            try
            {
                DropboxServiceProvider dropboxServiceProvider =
                        new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.Full);

                OAuthToken oauthAccessToken = LoadOAuthToken();

                IDropbox dropbox = (IDropbox)dropboxServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);
                return dropbox;
                
            }
            catch (Exception)
            {
                // throw new Exception(ex.Message);
            }
            return null;
        }
        public static void CreateFolder(string channelName)
        {
           var dropbox=LogInDropBox();
            createdFolderEntry = dropbox.CreateFolderAsync(FolderName+"/"+channelName).Result;
        }


        public DropboxDAL()
        {
            //
        }

        private static OAuthToken LoadOAuthToken()
        {
            string value = "";
            string secret = "";
            if(ConfigurationManager.AppSettings["IS_ON_APPHARBOR"]=="Yes")
            {
                value = ConfigurationManager.AppSettings["OAuthKey"];
                secret=ConfigurationManager.AppSettings["OAuthSecret"];
            }
            OAuthToken oauthAccessToken = new OAuthToken(value, secret);
            return oauthAccessToken;
        }

    }
}
    
