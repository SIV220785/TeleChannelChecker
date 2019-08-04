using AutoMapper;
using System;
using System.Collections.Generic;
using TelegramJoinChannel.BLL.DTO;
using TelegramJoinChannel.BLL.DTO.Helpers;
using TelegramJoinChannel.BLL.Interfaces;
using TelegramJoinChannel.BLL.Services.Base;
using TelegramJoinChannel.DAL.Entities;

namespace TelegramJoinChannel.BLL.Services
{
    internal class ChannelsService : BaseService, IChannelsService
    {

        protected override Action<IMapperConfigurationExpression> MapperCustomConfiguration =>
         cfg =>
         {
             cfg.CreateMap<Channels, ChannelsInfo>().ReverseMap();
         };

        public ResultOperationInfo<IEnumerable<ChannelsInfo>> GetAll()
        {
            var collection = UnitOfWork.GetRepository<Channels>().GetAllIncluding();
            var collectionInfo = MapperInstance.Map<IEnumerable<Channels>, IEnumerable<ChannelsInfo>>(collection);
            return new ResultOperationInfo<IEnumerable<ChannelsInfo>>(collectionInfo, true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo<ChannelsInfo> GetId(int itemId)
        {
            var item = UnitOfWork.GetRepository<Channels>().GetIncluding(itemId);
            var itemInfo = MapperInstance.Map<Channels, ChannelsInfo>(item);
            return new ResultOperationInfo<ChannelsInfo>(itemInfo, true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo Add(ChannelsInfo itemInfo)
        {
            var item = MapperInstance.Map<ChannelsInfo, Channels>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<Channels>().Add(item);
            return addedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo<ChannelsInfo> Create(ChannelsInfo itemInfo)
        {
            var item = MapperInstance.Map<ChannelsInfo, Channels>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<Channels>().Add(item);
            var addedItemInfo = MapperInstance.Map<Channels, ChannelsInfo>(addedItem);
            return addedItem == null
                ? new ResultOperationInfo<ChannelsInfo>(null, false, Localization.Error_OperationComplited)
                : new ResultOperationInfo<ChannelsInfo>(addedItemInfo, true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo Delete(int itemId)
        {
            var deletedRows = UnitOfWork.GetRepository<Channels>().DeleteBy(itemId);
            return deletedRows == 0
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo Update(ChannelsInfo itemInfo)
        {
            var item = MapperInstance.Map<ChannelsInfo, Channels>(itemInfo);
            var updatedItem = UnitOfWork.GetRepository<Channels>().Update(item, item.Id);
            return updatedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }

    }
}
