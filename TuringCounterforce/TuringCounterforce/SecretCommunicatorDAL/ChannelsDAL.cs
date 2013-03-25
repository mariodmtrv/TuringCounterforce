using System;
using System.Data;
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.IO;

namespace SecretCommunicator.SecretCommunicatorDAL
{
    internal class ChannelsDAL
    {
        internal const string ConfigChannel = "ChannelsConfiguration";

        internal static MongoCollection<BsonDocument> GetChannel(string channelName)
        {
            var db = AccessDatabase.ConnectToDatabase();
            var channel = db.GetCollection(channelName);
            return channel;
        }

        public static string GetChannelSalt(string channelName)
        {
            var db = AccessDatabase.ConnectToDatabase();
            var config = GetChannel
                (
                    ConfigChannel
                );
            var query = Query.EQ("Name", channelName);
            var channelData = config.FindOne(query);
            if (channelData != null)
            {
                return channelData["Salt"].ToString();
            }
            else
            {
                throw new ArgumentException("Channel does not exists!");
            }
        }

        public static bool SetChannelData(string name, string password, string salt)
        {
            var config = ChannelsDAL.GetChannel(ChannelsDAL.ConfigChannel);
            var queryBySalt = Query.EQ("Salt", salt);
            var channelData = config.FindOne(queryBySalt);
            var queryByName = Query.EQ("Name", name);

            if (config.Find(queryByName).Size() == 0)
            {
                channelData["Name"] = name;
                channelData["Password"] = password;
                config.Save(channelData);
                var db = AccessDatabase.ConnectToDatabase();
                var newChannel = db.CreateCollection(name);
                DropboxDAL.CreateFolder(name);
                return true;
            }
            else
            {
                config.Remove(queryBySalt);
                throw new DuplicateNameException("Channel with this name already exists!");
            }
            return false;
        }

        public static void CreateEmptyChannel(Channel newChannel)
        {
            var db = AccessDatabase.ConnectToDatabase();
            var config = GetChannel
                (
                    ConfigChannel
                );
            config.Insert(newChannel);
        }

        public static bool LogIntoChannel(string channelName, string password)
        {
            var config = GetChannel(ConfigChannel);
            var query = Query.EQ("Name", channelName);
            var channelCandidate = config.FindOne(query);
            if (channelCandidate != null)
            {
                if (channelCandidate["Password"].ToString() == password)
                {
                    return true;
                }
                throw new InvalidDataException("Invalid channel or password");
            }

            else
            {
                throw new InvalidDataException("Invalid channel or password");
            }
        }
    }
}
