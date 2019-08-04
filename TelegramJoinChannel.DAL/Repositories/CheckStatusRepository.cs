﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.DAL.Entities;
using TelegramJoinChannel.DAL.Repositories.Base;

namespace TelegramJoinChannel.DAL.Repositories
{
    internal class CheckStatusRepository : GenericRepository<CheckStatus>
    {
        public CheckStatusRepository(DbContext db) : base(db)
        { }
    }
}
