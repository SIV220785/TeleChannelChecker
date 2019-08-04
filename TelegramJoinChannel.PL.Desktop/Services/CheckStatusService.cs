using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.BLL.DTO;
using TelegramJoinChannel.BLL.Interfaces;
using Unity;

namespace TelegramJoinChannel.PL.Desktop.Services
{
    public class CheckStatusService
    {
        private readonly ICheckStatusService _checkStatusService;

        private ObservableCollection<CheckStatusInfo> _checkStatusInfos;

        public CheckStatusService(IUnityContainer container)
        {
            _checkStatusService = container.Resolve<ICheckStatusService>();
            _checkStatusInfos = new ObservableCollection<CheckStatusInfo>();
        }

        public ObservableCollection<CheckStatusInfo> GetCheckChannelsInfo()
        {
            if (_checkStatusInfos != null)
            {
                var result = _checkStatusService.GetAll();
                if (result.IsSuccess)
                {
                    return new ObservableCollection<CheckStatusInfo>(result.Value);
                }
            }
            return _checkStatusInfos;
        }

        public CheckStatusInfo Create(CheckStatusInfo itemInfo)
        {

            var result = _checkStatusService.Create(itemInfo);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            throw new Exception(result.Message);
        }

    }
}
