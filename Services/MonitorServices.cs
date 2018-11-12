using Itsomax.Module.MonitorCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using Itsomax.Module.Core.Interfaces;
using System.Threading.Tasks;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.Core.Data;
using Itsomax.Module.Core.Extensions;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Data;
namespace Itsomax.Module.MonitorCore.Services
{
    public class MonitorServices : IMonitor
    {
        private readonly IRepository<DatabaseSystem> _systemRepository;
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IRepository<Service> _serviceRepository;
        private readonly IRepository<ConfigurationType> _configurationTypeRepository;
        private readonly ILogginToDatabase _logger;
        private readonly ICustomRepositoryFunctions _customRepository;
        private readonly IMonitorCoreRepository _monitorCore;
        private readonly IRepository<DatabaseEnvironment> _dataBaseEnvironment;

        public MonitorServices(IRepository<DatabaseSystem> systemRepository, ILogginToDatabase logger,
            IRepository<Vendor> vendorRepository,IRepository<Service> serviceRepositor
            ,IRepository<ConfigurationType>  configurationTypeRepository,ICustomRepositoryFunctions customRepository,
            IMonitorCoreRepository monitorCore, IRepository<DatabaseEnvironment> dataBaseEnvironment)
        {
            _systemRepository = systemRepository;
            _logger = logger;
            _vendorRepository = vendorRepository;
            _configurationTypeRepository = configurationTypeRepository;
            _serviceRepository = serviceRepositor;
            _customRepository = customRepository;
            _monitorCore = monitorCore;
            _dataBaseEnvironment = dataBaseEnvironment;
        }

        public async Task<SystemSucceededTask> CreateSystem(CreateSystemViewModel model, string userName)
        {
            if (_configurationTypeRepository.GetById(model.ConfigTypeId) == null)
            {
                _logger.ErrorLog("Could not create system " + model.Name, "Create Database System", string.Empty,
                    userName);
                return SystemSucceededTask.Failed("Could not create system "+model.Name+"" +
                                                  ", Please select a configuration",string.Empty, false, true);
            }

            if (_vendorRepository.GetById(model.VendorId) == null)
            {
                _logger.ErrorLog("Could not create system " + model.Name, "Create Database System", string.Empty,
                    userName);
                return SystemSucceededTask.Failed("Could not create system {model.Name}, Please select a vendor",
                    string.Empty, false, true);
            }
            
            if (_dataBaseEnvironment.GetById(model.EnvironmentId) == null)
            {
                _logger.ErrorLog("Could not create system " + model.Name, "Create Database System", string.Empty,
                    userName);
                return SystemSucceededTask.Failed("Could not create system {model.Name}, Please select an rnvironment",
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
                    Vendor = _vendorRepository.GetById(model.VendorId),
                    DatabaseEnvironment = _dataBaseEnvironment.GetById(model.EnvironmentId)
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
                    return SystemSucceededTask.Failed("Could not create system " + model.Name,
                        ex.InnerException.Message, false, true);

                }

                _logger.ErrorLog(ex.Message, "Create Database System", ex.InnerException.Message, userName);
                return SystemSucceededTask.Failed("Could not create system " + model.Name, ex.InnerException.Message,
                    false, true);
            }
        }

        public DatabaseSystem GetDatabaseSystemById(long id,string userName)
        {
            try
            {
                var dbs = _systemRepository.GetById(id);
                _logger.InformationLog("Get Database System by Id Successfully", "Get Database System by Id",
                    string.Empty, userName);
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
                var dbs = _monitorCore.GetSystemListViewModels();
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
        
        public IList<GenericSelectList> EnvironmentSelectList(long id)
        {
            var getConfigList = _dataBaseEnvironment.GetAll();
            var configList = new List<GenericSelectList>();
            if(_dataBaseEnvironment.GetById(id) == null )
                configList.Add(new GenericSelectList {Id = 0,Name = "Select Configuration",Selected = true});
            configList.AddRange(from item in getConfigList
                let selected = _dataBaseEnvironment.GetById(id) != null
                select new GenericSelectList {Id = item.Id, Name = item.DatabaseEnvironmentName, Selected = selected});
            return configList;
        }

        public IList<GenericSelectList> DatabaseSystemList(long id)
        {
            var getSystemList = _systemRepository.GetAll();
            var configList = new List<GenericSelectList>();
            if(_systemRepository.GetById(id) == null)
                configList.Add(new GenericSelectList {Id = 0,Name = "Select Database System",Selected = true});
            configList.AddRange(from item in getSystemList
                let selected = _systemRepository.GetById(id) != null
                select new GenericSelectList {Id = item.Id,Name = item.Name, Selected = selected});
            return configList;
        }

        public IList<GenericSelectList> GetConfigurationByVendor(long vendorId)
        {
            return _monitorCore.GetConfigurationForVendor(vendorId);
        }
        
        
        public EditSystemViewModel GetSystemForEdit(long id, string userName)
        {
            try
            {
                var dbs = _monitorCore.GetSystemsEditViewModel(id);
                _logger.InformationLog("Get Database System: " + dbs.Name, "Get Database System for Edit", string.Empty,
                    userName);
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
                oldSystem.DatabaseEnvironment = _dataBaseEnvironment.GetById(model.EnvironmentId);
                oldSystem.UpdatedOn = DateTimeOffset.Now;
                
                await _systemRepository.SaveChangesAsync();
                _logger.InformationLog("Edit Database System: " + model.Name, "Edit Database System", string.Empty,
                    userName);
                return SystemSucceededTask.Success("System "+model.Name+" edited succesfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Source.Contains("sql"))
                {
                    _logger.ErrorLog(ex.Message, "Edit Database System", ex.InnerException.Message, userName);
                    return SystemSucceededTask.Failed("Edit {oldSystem.Name} unsuccessful", ex.InnerException.Message,
                        false, true);

                }

                _logger.ErrorLog(ex.Message, "Edit Database System", ex.InnerException.Message, userName);
                return SystemSucceededTask.Failed("Edit {oldSystem.Name} unsuccessful", ex.InnerException.Message,
                    false, true);
            }
        }

        public bool DeleteSystem(long id,string userName)
        {
            
            try
            {
                var deleteSystem = _systemRepository.GetById(id);
                _systemRepository.Remove(deleteSystem);
				_systemRepository.SaveChangesAsync();
                _logger.InformationLog("Disable System: " + deleteSystem.Name + " Successfully",
                    "Disable System", string.Empty, userName);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Delete System", ex.InnerException.Message, userName);
                return false;
            }
        }

        public async Task<bool> DisableEnableSystem(long id,string userName)
        {
            try
            {
                var systemEna = _systemRepository.GetById(id);
                if(systemEna.Active)
                {
                    systemEna.Active = false;
					await _systemRepository.SaveChangesAsync();
                    _logger.InformationLog("Disable System: " + systemEna.Name + " Successfully",
                        "Disable System", string.Empty, userName);
                    return true;
                }

                systemEna.Active = true;
                await _systemRepository.SaveChangesAsync();
                _logger.InformationLog("Enable System: " + systemEna.Name + " Successfully",
                    "Enable System", string.Empty, userName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Enable/Disable System", ex.InnerException.Message, userName);
                return false;
            }
        }

        public IEnumerable<ServiceListViewModel> GetServicesList(string userName)
        {
            try
            {
                var servicesList = _monitorCore.GetServicesList();
                _logger.InformationLog("Get Services List","Get Services List",string.Empty,userName);
                return servicesList;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message,"Get Services List",ex.InnerException.Message,userName);
                return null;
            }
            
        }

        public Service GetServiceByName(long id, string userName)
        {
            try
            {
                var service = _serviceRepository.GetById(id);
                if (service == null)
                {
                    _logger.ErrorLog("Service not found","Service not found",string.Empty,userName);
                    return null;
                }
                _logger.InformationLog("Service found "+service.Name,"Get Service",string.Empty,userName);
                return service;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message,"Get Service",ex.InnerException.Message);
                return null;
            }
        }

        public async Task<SystemSucceededTask> CreateService(CreateServiceViewModel model, string userName)
        {
            var newService = new Service
            {
                Active = model.Active,
                DatabaseSystem = _systemRepository.GetById(model.DatabaseSystemId),
                Hostname = model.Hostname,
                Ip = model.Ip,
                LoginName = model.LoginName,
                Name = model.Name,
                Named = model.Named,
                LoginPassword = _customRepository.SetEncryption(model.LoginPassword)
            };
            _serviceRepository.Add(newService);
            
            try
            {
                await _serviceRepository.SaveChangesAsync();
                _logger.InformationLog("Service: " + model.Name + " created successfully", "Create Service",
                    string.Empty, userName);
                return SystemSucceededTask.Success("Service: " + model.Name + " created successfully");
            }
            catch (Exception ex)
            {
                _logger.InformationLog(ex.Message, "Create Service",ex.InnerException.Message, userName);
                return SystemSucceededTask.Failed("Service: " + model.Name + " created unsuccessfully",
                    ex.InnerException.Message, true, false);
            }
        }

        public EditServiceViewModel GetServiceToEdit(long id,string userName)
        {
            try
            {
                var service = _monitorCore.GetServiceForEdit(id);
                if (service != null) return service;
                _logger.ErrorLog("Service not found","Edit Service",string.Empty,userName);
                return null;

            }
            catch (Exception ex)
            {
                _logger.InformationLog(ex.Message, "Edit Service",ex.InnerException.Message, userName);
                return null;
            }
            
        }
        
        public async Task<SystemSucceededTask> EditService(EditServiceViewModel model, string userName)
        {
            var oldService = _serviceRepository.GetById(model.Id);
            if (oldService == null)
            {
                _logger.InformationLog("Service not found", "Edit Service",string.Empty, userName);
                return SystemSucceededTask.Failed("Service: " + model.Name + " edited successful",
                    string.Empty, false, true);
            }

            oldService.Active = model.Active;
            oldService.DatabaseSystem = _systemRepository.GetById(model.DatabaseSystemId);
            oldService.Hostname = model.Hostname;
            oldService.Ip = model.Ip;
            oldService.LoginName = model.LoginName;
            oldService.Name = model.Name;
            oldService.Named = model.Named;
            oldService.LoginPassword = model.LoginPassword == "ChangeMe".ToUpper()
                ? oldService.LoginPassword
                : _customRepository.SetEncryption(model.LoginPassword);
            oldService.UpdatedOn = DateTimeOffset.Now;
            
            try
            {
                await _serviceRepository.SaveChangesAsync();
                _logger.InformationLog("Service: " + model.Name + " edited successfully", "Edit Service",
                    string.Empty, userName);
                return SystemSucceededTask.Success("Service: " + model.Name + " edited successfully");
            }
            catch (Exception ex)
            {
                _logger.InformationLog(ex.Message, "Edit Service",ex.InnerException.Message, userName);
                return SystemSucceededTask.Failed("Service: " + model.Name + " edited successful",
                    ex.InnerException.Message, true, false);
            }
        }
        
        public bool DisableEnableService(long id,string userName)
        {
            try
            {
                var serviceEna = _serviceRepository.GetById(id);
                if(serviceEna.Active)
                {
                    serviceEna.Active = false;
                    _serviceRepository.SaveChangesAsync();
                    _logger.InformationLog("Disable Service: " + serviceEna.Name + " Successfully",
                        "Disable Service", string.Empty, userName);
                    return true;
                }
                else
                {
                    serviceEna.Active = true;
                    _serviceRepository.SaveChangesAsync();
                    _logger.InformationLog("Enable Service: " + serviceEna.Name + " Successfully",
                        "Enable Service", string.Empty, userName);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Enable/Disable Service", ex.InnerException.Message, userName);
                return false;
            }
        }

    }
}
