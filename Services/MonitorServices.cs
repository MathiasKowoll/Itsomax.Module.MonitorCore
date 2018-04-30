using Itsomax.Module.MonitorCore.Interfaces;
using System;
using System.Collections.Generic;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;
using Itsomax.Data.Infrastructure.Data;
using Itsomax.Module.Core.Interfaces;
using System.Linq;

namespace Itsomax.Module.MonitorCore.Services
{
    public class MonitorServices : IMonitor
    {
        private readonly IRepository<DatabaseSystem> _systemRepository;
        private readonly ILogginToDatabase _logger;

        public MonitorServices(IRepository<DatabaseSystem> systemRepository, ILogginToDatabase logger)
        {
            _systemRepository = systemRepository;
            _logger = logger;
        }

        public bool CreateSystem(CreateSystemViewModel model, string userName)
        {
            try
            {
                var dbSysten = new DatabaseSystem()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Active = model.Active
                };

                _systemRepository.Add(dbSysten);
				_systemRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex.Message, "Create Database System", ex.InnerException.Message, userName);
                return false;
            }

        }

        public IEnumerable<SystemListViewModel> GetSystemList(string userName)
        {
            try
            {
                var list = _systemRepository.Query().ToList().Select(x => new SystemListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Active = x.Active
                });
                return list;
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
