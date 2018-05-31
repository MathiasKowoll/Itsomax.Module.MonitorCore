using Itsomax.Module.MonitorCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using Itsomax.Module.Core.Interfaces;
using System.Threading.Tasks;
using Itsomax.Module.Core.Extensions;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Data;

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
            if (_configurationTypeRepository.GetById(model.ConfigTypeId) == null)
            {
                _logger.ErrorLog("Could not create system "+model.Name, "Create Database System", string.Empty, userName);
                return SystemSucceededTask.Failed("Could not create system "+model.Name+", Please select a configuration",
                    string.Empty, false, true);
            }

            if (_vendorRepository.GetById(model.VendorId) == null)
            {
                _logger.ErrorLog("Could not create system "+model.Name, "Create Database System", string.Empty, userName);
                return SystemSucceededTask.Failed("Could not create system {model.Name}, Please select a configuration",
                    string.Empty, false, true);
            }
            try
            {
                var dbSysten = new DatabaseSystem()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Active = model.Active,
                    ConfigurationType = _configurationTypeRepository.GetById(model.ConfigTypeId),
                    Vendor = _vendorRepository.GetById(model.VendorId)
                };

                _systemRepository.Add(dbSysten);
                await _systemRepository.SaveChangesAsync();
                _logger.InformationLog("Create Database System " + model.Name + " successfully", "Create Database System",
                    string.Empty, userName);
                return SystemSucceededTask.Success("Database System "+model.Name +" created succesfully");
            }   
            catch (Exception ex)
            {
                if (ex.InnerException.Source.Contains("sql"))
                {
                    _logger.ErrorLog(ex.Message, "Create Database System", ex.InnerException.Message, userName);
                    return SystemSucceededTask.Failed("Could not create system "+model.Name, ex.InnerException.Message,false, true);

                }

                _logger.ErrorLog(ex.Message, "Create Database System", ex.InnerException.Message, userName);
                return SystemSucceededTask.Failed("Could not create system "+model.Name, ex.InnerException.Message,false, true);
            }
        }

        public DatabaseSystem GetDatabaseSystemById(long id,string userName)
        {
            try
            {
                var dbs = _systemRepository.GetById(id);
                _logger.InformationLog("Get Database System by Id Successfully", "Get Database System by Id",string.Empty, userName);
                return dbs;

            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Get Database System by Id", ex.InnerException.Message, userName);
                throw;
            }
        }

        public IEnumerable<SystemListViewModel> GetSystemList(string userName)
        {
            try
            {
                var dbs = _systemRepository.GetSystemListViewModels();
                _logger.InformationLog("Get Database System selectlist successfully", "Get Database System selectlist",
                    string.Empty, userName);
                return dbs;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Get System List Selectlist", ex.InnerException.Message, userName);
                return null;
            }

        }

        public IList<GenericSelectList> VendorSelectList(long id)
        {
            var getVendorList = _vendorRepository.GetAll();
            var vendorList = new List<GenericSelectList>();
            if(_vendorRepository.GetById(id) == null )
                vendorList.Add(new GenericSelectList {Id = 0,Name = "Select Vendor",Selected = true});
            vendorList.AddRange(from item in getVendorList
                let selected = _vendorRepository.GetById(id) != null
                select new GenericSelectList {Id = item.Id, Name = item.Name, Selected = selected});
            return vendorList;
        }
        
        public IList<GenericSelectList> ConfigurationTypeSelectList(long id)
        {
            var getConfigList = _configurationTypeRepository.GetAll();
            var configList = new List<GenericSelectList>();
            if(_configurationTypeRepository.GetById(id) == null )
                configList.Add(new GenericSelectList {Id = 0,Name = "Select Configuration",Selected = true});
            configList.AddRange(from item in getConfigList
                let selected = _configurationTypeRepository.GetById(id) != null
                select new GenericSelectList {Id = item.Id, Name = item.Name, Selected = selected});
            return configList;
        }
        
        
        public EditSystemViewModel GetSystemForEdit(long id, string userName)
        {
            try
            {
                var dbs = _systemRepository.GetSystemsEditViewModel(id);
                _logger.InformationLog("Get Database System: "+dbs.Name,"Get Database System for Edit",string.Empty,userName);
                return dbs;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Get System", ex.InnerException.Message,userName);
                return null;
            }

        }
        
        public async Task<SystemSucceededTask> EditSystem(EditSystemViewModel model, string userName)
        {
            var oldSystem = _systemRepository.GetById(model.Id);

            if (oldSystem == null)
            {
                _logger.ErrorLog("Edit System: " + model.Name + " does not exists", "Edit Database System",
                    "Database System " + model.Name + "does not exist", userName);
                return SystemSucceededTask.Failed("Edit " + model.Name + " unsuccessful",
                    "Database System " + model.Name + "does not exist", false, true);
            }
            
            try
            {
                oldSystem.Name = model.Name;
                oldSystem.Description = model.Description;
                oldSystem.Active = model.Active;
                oldSystem.Vendor = _vendorRepository.GetById(model.VendorId);
                oldSystem.ConfigurationType = _configurationTypeRepository.GetById(model.ConfigTypeId);
                await _systemRepository.SaveChangesAsync();
                _logger.InformationLog("Edit Database System: "+model.Name, "Edit Database System", string.Empty, userName);
                return SystemSucceededTask.Success("System "+model.Name+" edited succesfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Source.Contains("sql"))
                {
                    _logger.ErrorLog(ex.Message, "Edit Database System", ex.InnerException.Message, userName);
                    return SystemSucceededTask.Failed("Edit {oldSystem.Name} unsuccessful", ex.InnerException.Message,false, true);

                }

                _logger.ErrorLog(ex.Message, "Edit Database System", ex.InnerException.Message, userName);
                return SystemSucceededTask.Failed("Edit {oldSystem.Name} unsuccessful", ex.InnerException.Message,false, true);
            }
        }

        public bool DeleteSystem(long id,string userName)
        {
            var deleteSystem = _systemRepository.GetById(id);
            try
            {
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
                var systemEna = _systemRepository.GetById(id);
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
