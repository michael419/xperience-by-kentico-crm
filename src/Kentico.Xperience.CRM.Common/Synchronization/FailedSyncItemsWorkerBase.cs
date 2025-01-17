﻿using CMS.Base;
using CMS.Core;
using Kentico.Xperience.CRM.Common.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Kentico.Xperience.CRM.Common.Synchronization;

/// <summary>
/// Base class for thread workers which try to synchronize previously failed leads (biz form items)
/// Concrete implementation for each CRM must exists
/// </summary>
/// <typeparam name="TWorker"></typeparam>
/// <typeparam name="TService"></typeparam>
/// <typeparam name="TSettings"></typeparam>
/// <typeparam name="TApiConfig"></typeparam>
public abstract class FailedSyncItemsWorkerBase<TWorker, TService, TSettings, TApiConfig> : ThreadWorker<TWorker>
    where TWorker : ThreadWorker<TWorker>, new()
    where TService : ILeadsIntegrationService
    where TSettings : CommonIntegrationSettings<TApiConfig>
    where TApiConfig : new()
{
    protected override int DefaultInterval => 60000;
    private ILogger<TWorker> logger = null!;

    protected override void Initialize()
    {
        base.Initialize();
        logger = Service.Resolve<ILogger<TWorker>>();
    }

    /// <summary>Method processing actions.</summary>
    protected override void Process()
    {
        Debug.WriteLine($"Worker {GetType().FullName} running");

        try
        {
            using var serviceScope = Service.Resolve<IServiceProvider>().CreateScope();

            var settings = serviceScope.ServiceProvider.GetRequiredService<IOptionsSnapshot<TSettings>>().Value;
            if (!settings.FormLeadsEnabled) return;

            var failedSyncItemsService = Service.Resolve<IFailedSyncItemService>();

            ILeadsIntegrationService? leadsIntegrationService = null;

            foreach (var syncItem in failedSyncItemsService.GetFailedSyncItemsToReSync(CRMName))
            {
                leadsIntegrationService ??= serviceScope.ServiceProvider
                    .GetRequiredService<TService>();

                var bizFormItem = failedSyncItemsService.GetBizFormItem(syncItem);
                if (bizFormItem is null)
                {
                    continue;
                }

                leadsIntegrationService.SynchronizeLeadAsync(bizFormItem).ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error occured during running '{GetType().Name}'");
        }
    }

    /// <summary>
    /// Finishes the worker process. Implement this method to specify what the worker must do in order to not lose its internal data when being finished. Leave empty if no extra action is required.
    /// </summary>
    protected override void Finish()
    {
    }

    protected abstract string CRMName { get; }
}