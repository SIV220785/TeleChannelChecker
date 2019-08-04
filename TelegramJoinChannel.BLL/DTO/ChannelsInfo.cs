using System;

namespace TelegramJoinChannel.BLL.DTO
{
    public class ChannelsInfo
    {
        public int Id { get; set; }
        public string NameChannel { get; set; }
        public DateTime AvatarChannel { get; set; }
        public int SubscribersChannel { get; set; }
        public string UriChannel { get; set; }
        public string FullUriCannel { get; set; }
        public long AccessHashChannel { get; set; }
        public int IdChannel { get; set; }
        public string Status { get; set; }

    }
}
