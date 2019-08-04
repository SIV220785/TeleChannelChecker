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
    public class ChannelsService
    {
        private readonly IChannelsService _channelService;
        private ObservableCollection<ChannelsInfo> _channelsInfos;

        public ChannelsService(IUnityContainer container)
        {
            _channelService = container.Resolve<IChannelsService>();
            _channelsInfos = new ObservableCollection<ChannelsInfo>();
        }

        public ObservableCollection<ChannelsInfo> GetChannelsInfo()
        {
            if (_channelsInfos != null)
            {
                var result = _channelService.GetAll();
                if (result.IsSuccess)
                {
                    return new ObservableCollection<ChannelsInfo>(result.Value);
                }
            }
            return _channelsInfos;
        }


        public void Add(ChannelsInfo itemInfo)
        {
            var result = _channelService.Add(itemInfo);
            if (result.IsSuccess)
            {
                return;
            }
            throw new Exception(result.Message);
        }

        public ChannelsInfo Create(ChannelsInfo itemInfo)
        {

            var result = _channelService.Create(itemInfo);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            throw new Exception(result.Message);
        }

        public void Updete(ChannelsInfo itemInfo)
        {
            var result = _channelService.Update(itemInfo);
            if (result.IsSuccess)
            {
                return;
            }
            throw new Exception(result.Message);
        }
    }
}
