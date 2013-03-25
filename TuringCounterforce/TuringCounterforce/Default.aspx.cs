using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spring.IO;
using Spring.Social.Dropbox.Api;
using SecretCommunicator.SecretCommunicatorDAL;
using SecretCommunicator.PostableDocuments;

namespace SecretCommunicator
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    bool isLogged = false;
                    string channelName = channelNameSpanFileSecret.Text;
                    string channelPassword = filePasswordSecret.Text;
                    isLogged = ChannelsDAL.LogIntoChannel(channelName,channelPassword);
                    {
                        string unique = DateTime.Now.Ticks.ToString();
                        string filename = unique.ToString()+Path.GetFileName(FileUploadControl.FileName);
                        var filepath = Server.MapPath("~/App_Data/")+ filename;
                        
                        FileUploadControl.SaveAs(filepath);
                        string fileMimetype = FileUploadControl.PostedFile.ContentType;
                        var path = string.Format("{0}/{1}/{2}", DropboxDAL.FolderName, channelName, filename);
                      var dropbox=DropboxDAL.LogInDropBox();
                        Entry uploadFileEntry = dropbox.UploadFileAsync(
                            new FileResource(filepath),
                            path).Result;

                        //   WebRequestMethods.File.Delete(filepath);

                        Task<DropboxLink> shareableLink = dropbox.GetMediaLinkAsync(path);

                        shareableLink.Result.ExpireDate.AddDays(2);

                        string filePath = shareableLink.Result.Url;

                        PostableDocuments.File newFile = new PostableDocuments.File(filePath, fileMimetype);
                        newFile.Upload(channelName);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("The file could not be uploaded. The following error occured {0}", ex.Message));
                }
            }
        }

    }
}