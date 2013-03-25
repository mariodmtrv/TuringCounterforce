using System;
using System.IO;
using System.Diagnostics;
using Spring.Social.OAuth1;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using Spring.IO;

namespace SecretCommunicator.SecretCommunicatorDAL
{
    class StorageProvider
    {
        private const string DropboxAppKey = "to67sivktyx0ymj";
        private const string DropboxAppSecret = "l3js162z7uksuim";

        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";

        static void Main()
        {
            var dropbox = Dropbox();

            // Upload a file
            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new FileResource("../../DropboxExample.cs"),
                "/uploads/DropboxExample.cs").Result;
            Console.WriteLine("Uploaded a file: {0}", uploadFileEntry.Path);

            /* Play a bit more with the Dropbox API
		  
		Entry copyEntry = dropbox.CopyAsync("Spring Social/File.txt", "Spring Social/File_copy.txt").Result;
		Entry deleteEntry = dropbox.DeleteAsync("Spring Social/File.txt").Result;
		Entry moveEntry = dropbox.MoveAsync("Spring Social/File_copy.txt", "Spring Social/File.txt").Result;
		dropbox.DownloadFileAsync("Spring Social/File.txt")
			.ContinueWith(task =>
			{
				Console.WriteLine("File '{0}' downloaded ({1})", task.Result.Metadata.Path, task.Result.Metadata.Size);
				// Save file to "C:\Spring Social.txt"
				using (FileStream fileStream = new FileStream(@"C:\Spring Social.txt", FileMode.Create))
				{
					fileStream.Write(task.Result.Content, 0, task.Result.Content.Length);
				}
			});
		Entry folderMetadata = dropbox.GetMetadataAsync("Spring Social").Result;
		IList<Entry> revisionsEntries = dropbox.GetRevisionsAsync("Spring Social/File.txt").Result;
		Entry restoreEntry = dropbox.RestoreAsync("Spring Social/File.txt", revisionsEntries[2].Revision).Result;
		IList<Entry> searchResults = dropbox.SearchAsync("Spring Social/", ".txt").Result;
		DropboxLink shareableLink = dropbox.GetShareableLinkAsync("Spring Social/File.txt").Result;
		DropboxLink mediaLink = dropbox.GetMediaLinkAsync("Spring Social/File.txt").Result;
		Entry uploadImageEntry = dropbox.UploadFileAsync(
			new AssemblyResource("assembly://Spring.ConsoleQuickStart/Spring.ConsoleQuickStart/Image.png"),
			"/Spring Social/Image.png", true, null, CancellationToken.None).Result;
		dropbox.DownloadThumbnailAsync("Spring Social/Image.png", ThumbnailFormat.Png, ThumbnailSize.Medium)
			.ContinueWith(task =>
			{
				Console.WriteLine("Thumbnail '{0}' downloaded ({1})", task.Result.Metadata.Path, task.Result.Metadata.Size);
				// Save file to "C:\Thumbnail_Medium.png"
				using (FileStream fileStream = new FileStream(@"C:\Thumbnail_Medium.png", FileMode.Create))
				{
					fileStream.Write(task.Result.Content, 0, task.Result.Content.Length);
				}
			});
		*/
        }

        private static IDropbox Dropbox()
        {
            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            // Authenticate the application (if not authenticated) and load the OAuth token
            if (!File.Exists(OAuthTokenFileName))
            {
                AuthorizeAppOAuth(dropboxServiceProvider);
            }
            OAuthToken oauthAccessToken = LoadOAuthToken();

            // Login in Dropbox
            IDropbox dropbox = dropboxServiceProvider.GetApi(oauthAccessToken.Value, oauthAccessToken.Secret);

            // Create new folder
            string newFolderName = "New_Folder_" + DateTime.Now.Ticks;
            Entry createFolderEntry = dropbox.CreateFolderAsync(newFolderName).Result;
            Console.WriteLine("Created folder: {0}", createFolderEntry.Path);
            return dropbox;
        }

        private static OAuthToken LoadOAuthToken()
        {
            string[] lines = File.ReadAllLines(OAuthTokenFileName);
            OAuthToken oauthAccessToken = new OAuthToken(lines[0], lines[1]);
            return oauthAccessToken;
        }
  
        private static void AuthorizeAppOAuth(DropboxServiceProvider dropboxServiceProvider)
        {
            // Authorization without callback url
            Console.Write("Getting request token...");
            OAuthToken oauthToken = dropboxServiceProvider.OAuthOperations.FetchRequestTokenAsync(null, null).Result;
            Console.WriteLine("Done.");

            OAuth1Parameters parameters = new OAuth1Parameters();
            string authenticateUrl = dropboxServiceProvider.OAuthOperations.BuildAuthorizeUrl(
                oauthToken.Value, parameters);
            Console.WriteLine("Redirect the user for authorization to {0}", authenticateUrl);
            Process.Start(authenticateUrl);
            Console.Write("Press any key when authorization attempt has succeeded.");
            Console.ReadLine();

            Console.Write("Getting access token...");
            AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, null);
            OAuthToken oauthAccessToken =
                dropboxServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
            Console.WriteLine("Done.");

            string[] oauthData = new string[] { oauthAccessToken.Value, oauthAccessToken.Secret };
            File.WriteAllLines(OAuthTokenFileName, oauthData);
        }
    }
}
