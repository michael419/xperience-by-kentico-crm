﻿using Kentico.Xperience.CRM.Salesforce.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Salesforce.OpenApi;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Web;
using SalesforceApiClient = Salesforce.OpenApi.SalesforceApiClient;

namespace Kentico.Xperience.CRM.Salesforce.Synchronization;

internal class SalesforceApiService : ISalesforceApiService
{
    private readonly HttpClient httpClient;
    private readonly ILogger<SalesforceApiService> logger;
    private readonly IOptionsSnapshot<SalesforceIntegrationSettings> integrationSettings;
    private readonly SalesforceApiClient apiClient;

    public SalesforceApiService(
        HttpClient httpClient,
        ILogger<SalesforceApiService> logger,
        IOptionsSnapshot<SalesforceIntegrationSettings> integrationSettings
    )
    {
        this.httpClient = httpClient;
        this.logger = logger;
        this.integrationSettings = integrationSettings;

        apiClient = new SalesforceApiClient(httpClient);
    }

    public async Task<SaveResult> CreateLeadAsync(LeadSObject lead)
    {
        return await apiClient.LeadPOSTAsync(MediaTypeNames.Application.Json, lead);
    }

    public async Task UpdateLeadAsync(string id, LeadSObject leadSObject)
    {
        await apiClient.LeadPATCHAsync(id, MediaTypeNames.Application.Json, leadSObject);
    }

    /// <summary>
    /// Method for get entity by external ID is not generated by BETA OpenApi 3 definition
    /// Could be better solution when this endpoint will be generated in <see cref="SalesforceApiClient"/>
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="externalId"></param>
    /// <returns></returns>
    public async Task<string?> GetLeadIdByExternalId(string fieldName, string externalId)
    {
        var apiVersion = integrationSettings.Value.ApiConfig.ApiVersion.ToString("F1", CultureInfo.InvariantCulture);
        using var request =
            new HttpRequestMessage(HttpMethod.Get,
                $"/services/data/v{apiVersion}/sobjects/lead/{fieldName}/{externalId}?fields=Id");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var lead = await response.Content.ReadFromJsonAsync<LeadSObject>();
            return lead?.Id;
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            logger.LogWarning("Lead not found for external field name: '{ExternalFieldName}' and value: '{ExternalId}'",
                fieldName, externalId);
            return null;
        }
        else
        {
            string responseMessage = await response.Content.ReadAsStringAsync();
            throw new ApiException("Unexpected response", (int)response.StatusCode, responseMessage, null!, null);
        }
    }

    public async Task<LeadSObject?> GetLeadById(string id, string? fields = null)
        => await apiClient.LeadGET2Async(id, fields);

    public async Task<string?> GetLeadByEmail(string email)
    {
        var apiVersion = integrationSettings.Value.ApiConfig.ApiVersion.ToString("F1", CultureInfo.InvariantCulture);
        using var request =
            new HttpRequestMessage(HttpMethod.Get,
                $"/services/data/v{apiVersion}/query?q=SELECT+Id+FROM+Lead+WHERE+Email='{HttpUtility.UrlEncode(email)}'+ORDER+BY+CreatedDate+DESC");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        var response = await httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var queryResult = await response.Content.ReadFromJsonAsync<QueryResult<LeadSObject>>();
            return queryResult?.Records.FirstOrDefault()?.Id;
        }
        else
        {
            string responseMessage = await response.Content.ReadAsStringAsync();
            throw new ApiException("Unexpected response", (int)response.StatusCode, responseMessage, null!, null);
        }
    }
}