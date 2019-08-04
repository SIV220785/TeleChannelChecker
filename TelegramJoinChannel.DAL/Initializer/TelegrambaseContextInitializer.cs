using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.DAL.Contexts;
using TelegramJoinChannel.DAL.Entities;

namespace TelegramJoinChannel.DAL.Initializer
{
    internal class TelegrambaseContextInitializer : DropCreateDatabaseAlways<TelegrambaseContext>
    {
        protected override void Seed(TelegrambaseContext db)
        {
            Channels channels = new Channels()
            {
                AvatarChannel = DateTime.Now,
                NameChannel = "Test",
                SubscribersChannel = 10000,
                UriChannel = "Channels"
            };

            CheckStatus checkStatus = new CheckStatus()
            {
                ChangeAvatar = 2,
                ChannelsCount = 1,
                CheckDate = DateTime.Now,
                ChangeName = 1,
                NoUri = 0
            };

            db.Channels.Add(channels);
            db.CheckStatuses.Add(checkStatus);
            db.SaveChanges();
        }
    }
}
