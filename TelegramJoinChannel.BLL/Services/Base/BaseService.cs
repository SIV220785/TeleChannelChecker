using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramJoinChannel.DAL.UnitOfWork;
using TelegramJoinChannel.DAL.UnitOfWork.Base;

namespace TelegramJoinChannel.BLL.Services.Base
{
    internal class BaseService
    {
        private MapperConfiguration _mapperConfiguration;

        public IMapper MapperInstance => _mapperConfiguration?.CreateMapper();

        protected virtual Action<IMapperConfigurationExpression> MapperCustomConfiguration { get; set; } = cfg => { };

        public IUnitOfWork UnitOfWork { get; }
        public BaseService()
        {
            UnitOfWork = new UnitOfWork();
            MapperInitialize();
        }
        private void MapperInitialize()
        {
            _mapperConfiguration = new MapperConfiguration(MapperCustomConfiguration);
        }
    }
}
