using Itsomax.Module.MonitorCore.Interfaces;
using System;
using System.Collections.Generic;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using Itsomax.Module.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Itsomax.Module.Core.Extensions;
using Itsomax.Module.MonitorCore.Data;
using Microsoft.EntityFrameworkCore;

namespace Itsomax.Module.MonitorCore.Services
{
    public class MonitorServices : IMonitor
    {
        private readonly IDatabaseSystemRepository _systemRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IConfigurationTypeRepository _configurationTypeRepository;
        private readonly ILogginToDatabase _logger;

        public MonitorServices(IDatabaseSystemRepository systemRepository, ILogginToDatabase logger,
            IVendorRepository vendorRepository,IServiceRepository serviceRepositor
            ,IConfigurationTypeRepository configurationTypeRepository)
        {
            _systemRepository = systemRepository;
            _logger = logger;
            _vendorRepository = vendorRepository;
            _configurationTypeRepository = configurationTypeRepository;
            _serviceRepository = serviceRepositor;
        }

        public async Task<SystemSucceededTask> CreateSystem(CreateSystemViewModel model, string userName)
        {
            try
            {
                var dbSysten = new DatabaseSystem()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Active = model.Active,
                    ConfigurationTypeId = model.ConfigTypeId,
                    Vendor = _vendorRepository.GetById(model.VendorId)
                };

                _systemRepository.Add(dbSysten);
                await _systemRepository.SaveChangesAsync();
                return SystemSucceededTask.Success("System {model.Name} created succesfully");
            }   
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.Source.Contains("sql"))
                {
                    _logger.ErrorLog(ex.Message, "Create Database System", ex.InnerException.Message, userName);
                    return SystemSucceededTask.Failed("Could not create system {model.Name}", ex.InnerException.Message,false, true);

                }
                else
                {
                    _logger.ErrorLog(ex.Message, "Create Database System", ex.InnerException.Message, userName);
                    return SystemSucceededTask.Failed("Could not create system {model.Name}", ex.InnerException.Message,false, true);
                }
            }
        }

        public IEnumerable<SystemListViewModel> GetSystemList(string userName)
        {
            try
            {
                return _systemRepository.GetSystemListViewModels();
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Get System List", ex.InnerException.Message, userName);
                return null;
            }

        }
        public EditSystemViewModel GetSystemForEdit(long id, string userName)
        {
            try
            {
                var system = _systemRepository.Query().FirstOrDefault(x => x.Id == id);
                var editSystem = new EditSystemViewModel()
                {
                    Id = system.Id,
                    Name = system.Name,
                    Description = system.Description,
                    Active = system.Active
                };
                return editSystem;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Get System", ex.InnerException.Message);
                return null;
            }

        }

        public DatabaseSystem GetSystem(long id, string userName)
        {
            try
            {
                var system = _systemRepository.Query().FirstOrDefault(x => x.Id == id);
                
                return system;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Get System", ex.InnerException.Message);
                return null;
            }

        }

        public DatabaseSystem GetSystem(long id)
        {
            return GetSystem(id, string.Empty);
        }

        public bool EditSystem(EditSystemViewModel model, string userName)
        {
            try
            {
                var oldsystem = GetSystem(model.Id);
                oldsystem.Name = model.Name;
                oldsystem.Description = model.Description;
                oldsystem.Active = model.Active;
                _systemRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Edit System", ex.InnerException.Message, userName);
                return false;
            }
        }

        public bool DeleteSystem(long id,string userName)
        {
            try
            {
                var deleteSystem = GetSystem(id);
                _systemRepository.Remove(deleteSystem);
				_systemRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Delete System", ex.InnerException.Message, userName);
                return false;
            }
        }

        public bool DisableEnableSystem(long id,string userName)
        {
            try
            {
                var systemEna = GetSystem(id);
                if(systemEna.Active)
                {
                    systemEna.Active = false;
					_systemRepository.SaveChangesAsync();
                    return true;
                }
                else
                {
                    systemEna.Active = true;
					_systemRepository.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Enable/Disable System", ex.InnerException.Message, userName);
                return false;
            }
        }
    }
}
