using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NLog;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Model;
using qodeless.domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class SessionDeviceAppService : ISessionDeviceAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;
        private readonly ISessionDeviceRepository _sessionDeviceRepository;
        private readonly IRepository<UserViewModel> _iRepository;   
        public SessionDeviceAppService(IMapper mapper, ISessionDeviceRepository SessionDeviceRepository)
        {
            _mapper = mapper;
            _sessionDeviceRepository = SessionDeviceRepository;
        }

        public IEnumerable<SessionDeviceViewModel> GetAll()
        {
            Logger.Info("== Session Device displayed on the index ==");
            var user = _sessionDeviceRepository.GetAllUsers();
            var data = _sessionDeviceRepository.GetAll()
                .Include(_ => _.Device)
                .Select(_ => new SessionDeviceViewModel
                {
                    Id = _.Id,
                    UserPlayId = user.Where(x => x.Id == _.UserPlayId).Select(x => x.Email).FirstOrDefault(),
                    LastIpAddress = _.LastIpAddress,
                    DeviceId = _.DeviceId,
                    DtBegin = _.DtBegin,
                    DtEnd = _.DtEnd,
                    DeviceName = _.Device.Code,
                }).AsNoTracking();
            return data;
        }

        public SessionDeviceViewModel GetById(Guid id)
        {
            return _mapper.Map<SessionDeviceViewModel>(_sessionDeviceRepository.GetById(id));
        }
        public UserViewModel GetByUserId(Guid id)
        {
                return _iRepository.GetAllUsers()
                .Select(_ => new UserViewModel
                {
                    Id = _.Id,
                    Email = _.Email,
                }).FirstOrDefault();
        }


        public IEnumerable<SessionDeviceViewModel> GetAllBy(Func<SessionDevice, bool> exp)
        {
            return _mapper.Map<IEnumerable<SessionDeviceViewModel>>(_sessionDeviceRepository.GetAllBy(exp));
        }

        public UserViewModel GetUserById(string userId)
        {
            return _sessionDeviceRepository.GetUserById(userId);
        }

        public ValidationResult Remove(Guid id)
        {
            Logger.Info("== Removing session device ==");
            var entity = _sessionDeviceRepository.GetById(id);
            var validationResult = new SessionDeviceRemoveValidator().Validate(entity);
            if (validationResult.IsValid)
                _sessionDeviceRepository.SoftDelete(entity);

            return validationResult;
        }
        public ValidationResult Add(SessionDeviceViewModel vm)
        {
           Logger.Info("== Adding session device ==");
            var entity = _mapper.Map<SessionDevice>(vm);
            entity.DtEnd = DateTime.UnixEpoch;
            var validationResult = new SessionDeviceAddValidator().Validate(entity);
            if (validationResult.IsValid)
                _sessionDeviceRepository.Add(entity);

            return validationResult;
        }
        public ValidationResult Update(SessionDeviceViewModel vm)
        {
            Logger.Info("== Updating session device ==");
            var entity = _mapper.Map<SessionDevice>(vm);
            var validationResult = new SessionDeviceUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
                _sessionDeviceRepository.Update(entity);

            return validationResult;
        }
        public void Dispose() => GC.SuppressFinalize(this);
        public IEnumerable<DeviceViewModel> GetDevices()
        {
            return _mapper.Map<IEnumerable<DeviceViewModel>>(_sessionDeviceRepository.GetDevices());
        }

        public IEnumerable<DeviceDropDownViewModel> GetDevicesDrop()
        {
            return _sessionDeviceRepository.GetDevicesDrop();
        }

        public ValidationResult Upsert(SessionDeviceViewModel vm)
        {
            Logger.Info("== Inserting session device ==");
            if (Guid.Empty == vm.Id)
            {
                return  Add(vm);
            }
            else
            {
                return  Update(vm);
            }
        }
    }
}
