﻿using System;
using System.ComponentModel.DataAnnotations;
using Itsomax.Data.Infrastructure.Models;

namespace Itsomax.Module.MonitorCore.Models.DatabaseManagement
{
    public abstract class ServiceInstance : EntityBase
    {
        public ServiceInstance()
        {
            UpdatedOn = DateTimeOffset.Now;
            CreatedOn = DateTimeOffset.Now;
        }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Ip { get; set; }
        [MaxLength(200)]
        [Required]
        public string Hostname { get; set; }
        [MaxLength(100)]
        public string Named { get; set; }
        [MaxLength(100)]
        [Required]
        public string LoginName { get; set; }
        [Required]
        public byte[] LoginPassword { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public DateTimeOffset UpdatedOn { get; set; }
        [Required]
        public DateTimeOffset CreatedOn { get; }
    }
}