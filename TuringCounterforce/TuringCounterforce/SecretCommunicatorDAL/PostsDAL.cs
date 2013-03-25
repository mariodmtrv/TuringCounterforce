using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Builders;
using SecretCommunicator.PostableDocuments;
using System;

namespace SecretCommunicator.SecretCommunicatorDAL
{
    public class PostsDAL
    {
        public static List<Postable> GetPosts(string channelName, int postsCount)
        {
            var channel = ChannelsDAL.GetChannel(channelName);
            var posts = channel.FindAll().SetSortOrder(SortBy.Descending("_id"));
            int postsC = (int) Math.Min(posts.Count(), postsCount);
            var requiredPosts=posts.Take(postsC);
            List<Postable> resultPosts = new List<Postable>(postsCount);

            foreach (var post in requiredPosts)
            {
                switch (post["DocType"].ToString())
                {
                    case "message":
                        {
                            Postable m = new Message(post["MessageContent"].ToString());
                            resultPosts.Add(m);
                            break;
                        }
                    case "link":
                        {
                            Postable l = new Link(
                                post["Url"].ToString(),
                                post["Description"].ToString());
                            resultPosts.Add(l);
                            break;
                        }
                    case "file":
                        {
                            Postable f = new File(post["Url"].ToString(), post["MimeType"].ToString());
                            resultPosts.Add(f);
                            break;
                        }
                }
            }
            return resultPosts;
        }
    }
}

