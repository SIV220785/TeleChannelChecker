using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.BLL.DTO;
using TelegramJoinChannel.BLL.DTO.Helpers;
using TelegramJoinChannel.BLL.Interfaces;
using TelegramJoinChannel.BLL.Services.Base;
using TelegramJoinChannel.DAL.Entities;

namespace TelegramJoinChannel.BLL.Services
{
    internal class CheckStatusService : BaseService, ICheckStatusService
    {
        protected override Action<IMapperConfigurationExpression> MapperCustomConfiguration =>
        cfg =>
        {
            cfg.CreateMap<CheckStatus, CheckStatusInfo>().ReverseMap();
        };

        public ResultOperationInfo<IEnumerable<CheckStatusInfo>> GetAll()
        {
            var collection = UnitOfWork.GetRepository<CheckStatus>().GetAllIncluding();
            var collectionInfo = MapperInstance.Map<IEnumerable<CheckStatus>, IEnumerable<CheckStatusInfo>>(collection);
            return new ResultOperationInfo<IEnumerable<CheckStatusInfo>>(collectionInfo, true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo<CheckStatusInfo> GetId(int itemId)
        {
            var item = UnitOfWork.GetRepository<CheckStatus>().GetIncluding(itemId);
            var itemInfo = MapperInstance.Map<CheckStatus, CheckStatusInfo>(item);
            return new ResultOperationInfo<CheckStatusInfo>(itemInfo, true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo Add(CheckStatusInfo itemInfo)
        {
            var item = MapperInstance.Map<CheckStatusInfo, CheckStatus>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<CheckStatus>().Add(item);
            return addedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo<CheckStatusInfo> Create(CheckStatusInfo itemInfo)
        {
            var item = MapperInstance.Map<CheckStatusInfo, CheckStatus>(itemInfo);
            var addedItem = UnitOfWork.GetRepository<CheckStatus>().Add(item);
            var addedItemInfo = MapperInstance.Map<CheckStatus, CheckStatusInfo>(addedItem);
            return addedItem == null
                ? new ResultOperationInfo<CheckStatusInfo>(null, false, Localization.Error_OperationComplited)
                : new ResultOperationInfo<CheckStatusInfo>(addedItemInfo, true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo Delete(int itemId)
        {
            var deletedRows = UnitOfWork.GetRepository<CheckStatus>().DeleteBy(itemId);
            return deletedRows == 0
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }

        public ResultOperationInfo Update(CheckStatusInfo itemInfo)
        {
            var item = MapperInstance.Map<CheckStatusInfo, CheckStatus>(itemInfo);
            var updatedItem = UnitOfWork.GetRepository<CheckStatus>().Update(item, item.Id);
            return updatedItem == null
                ? new ResultOperationInfo(false, Localization.Error_OperationComplited)
                : new ResultOperationInfo(true, Localization.Success_OperationComplited);
        }
    }
}
