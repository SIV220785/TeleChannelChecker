using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.DAL.Entities;
using TelegramJoinChannel.DAL.Repositories.Base;

namespace TelegramJoinChannel.DAL.Repositories
{
    internal class ChannelsRepository : GenericRepository<Channels>
    {
        public ChannelsRepository(DbContext db) : base(db)
        { }
    }
}
