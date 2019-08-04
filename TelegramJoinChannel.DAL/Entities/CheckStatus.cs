using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramJoinChannel.DAL.Entities
{
    [Table("CheckStatus")]
    internal class CheckStatus
    {
        public int Id { get; set; }
        public DateTime CheckDate { get; set; }
        public int ChannelsCount { get; set; }
        public int NoUri { get; set; }
        public int ChangeName { get; set; }
        public int ChangeAvatar { get; set; }
    }
}
