using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.BLL.DTO;
using TelegramJoinChannel.BLL.DTO.Helpers;

namespace TelegramJoinChannel.BLL.Interfaces
{
    interface IChannelsService
    {
        ResultOperationInfo<IEnumerable<ChannelsInfo>> GetAll();
        ResultOperationInfo<ChannelsInfo> GetId(int itemId);

        ResultOperationInfo Add(ChannelsInfo item);
        ResultOperationInfo Update(ChannelsInfo item);
        ResultOperationInfo Delete(int itemId);

        ResultOperationInfo<ChannelsInfo> Create(ChannelsInfo itemInfo);
    }
}
