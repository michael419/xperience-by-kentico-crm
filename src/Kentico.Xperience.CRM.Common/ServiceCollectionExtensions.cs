﻿using Kentico.Xperience.CRM.Common.Admin;
using Kentico.Xperience.CRM.Common.Configuration;
using Kentico.Xperience.CRM.Common.Synchronization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kentico.Xperience.CRM.Common;

/// <summary>
/// Contains extension methods for CRM services registration on startup
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds common services for CRM integration. This method is usually used from specific CRM integration library
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddKenticoCrmCommonFormLeadsIntegration(
        this IServiceCollection services)
    {
        services.TryAddSingleton<ILeadsIntegrationValidationService, LeadIntegrationValidationService>();

        services.TryAddSingleton<ICRMModuleInstaller, CRMModuleInstaller>();
        services.TryAddSingleton<IFailedSyncItemService, FailedSyncItemService>();
        services.TryAddSingleton<ICRMSyncItemService, CRMSyncItemService>();
        services.TryAddSingleton<ICRMSettingsService, CRMSettingsService>();

        return services;
    }
}