using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramJoinChannel.DAL.Entities
{
    [Table("Channel")]
    internal class Channels
    {
        
        public int Id { get; set; }
        public long AccessHashChannel { get; set; }
        public int IdChannel { get; set; }
        public string NameChannel { get; set; }
        public string UriChannel { get; set; }
        public string FulluriCannel { get; set; }
        public DateTime AvatarChannel { get; set; }
        public int SubscribersChannel { get; set; }
    }
}
