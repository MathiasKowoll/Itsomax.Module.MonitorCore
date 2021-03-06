﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Itsomax.Module.Core.Extensions;
using Itsomax.Module.Core.ViewModels;
using Itsomax.Module.MonitorCore.Models.DatabaseManagement;
using Itsomax.Module.MonitorCore.ViewModels.DatabaseManagement;

namespace Itsomax.Module.MonitorCore.Interfaces
{
    public interface IMonitor
    {
        Task<SystemSucceededTask> CreateSystem(CreateSystemViewModel model, string userName);
        IEnumerable<SystemListViewModel> GetSystemList(string userName);
        EditSystemViewModel GetSystemForEdit(long id, string userName);
        Task<SystemSucceededTask> EditSystem(EditSystemViewModel model, string userName);
        bool DeleteSystem(long id, string userName);
        Task<bool> DisableEnableSystem(long id, string userName);
        IList<GenericSelectList> ConfigurationTypeSelectList(long id);
        IList<GenericSelectList> VendorSelectList(long id);
        IList<GenericSelectList> DatabaseSystemList(long id);
        IList<GenericSelectList> GetConfigurationByVendor(long vendorId);
        IList<GenericSelectList> EnvironmentSelectList(long id);
        DatabaseSystem GetDatabaseSystemById(long id, string userName);
        IEnumerable<ServiceListViewModel> GetServicesList(long? systemId, string userName);
        Service GetServiceByName(long id, string userName);
        Task<SystemSucceededTask> CreateService(CreateServiceViewModel model, string userName);
        Task<SystemSucceededTask> EditService(EditServiceViewModel model, string userName);
        EditServiceViewModel GetServiceToEdit(long id, string userName);
        Service GetServiceById(long id, string userName);
        IEnumerable<InstanceListViewModel> GetInstanceList(long? systemId, string userName);
        Task<SystemSucceededTask> CreateInstance(CreateInstanceViewModel model, string userName);
        Task<SystemSucceededTask> EditInstance(EditInstanceViewModel model, string userName);
        EditInstanceViewModel GetInstanceToEdit(long id, string userName);
        IList<GenericSelectList> ServiceList(long id);
        Task<bool> DisableEnableService(long id, string userName);
        Task<bool> DisableEnableInstance(long id, string userName);
        Instance GetInstanceById(long id, string userName);

    }
}
