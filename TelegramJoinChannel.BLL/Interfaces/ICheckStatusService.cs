using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.BLL.DTO;
using TelegramJoinChannel.BLL.DTO.Helpers;

namespace TelegramJoinChannel.BLL.Interfaces
{
    interface ICheckStatusService
    {
        ResultOperationInfo<IEnumerable<CheckStatusInfo>> GetAll();
        ResultOperationInfo<CheckStatusInfo> GetId(int itemId);

        ResultOperationInfo Add(CheckStatusInfo item);
        ResultOperationInfo Update(CheckStatusInfo item);
        ResultOperationInfo Delete(int itemId);

        ResultOperationInfo<CheckStatusInfo> Create(CheckStatusInfo itemInfo);
    }
}
