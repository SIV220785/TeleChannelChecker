using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramJoinChannel.PL.Desktop.Model
{
    public class AddChannels
    {
        public string UriChannel { get; set; }
        public override string ToString()
        {
            return $"{UriChannel}";
        }
    }
}
