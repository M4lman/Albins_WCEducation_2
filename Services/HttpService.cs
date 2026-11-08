using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Client.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Client.Services;

public class HttpService<T> : IDisposable, IHttpService<T> where T : class
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = string.Empty;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public HttpService()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var apiKey = config.GetSection("Settings:ApiKey").Value!;
        _baseUrl = config.GetSection("Settings:BaseUrl").Value!;

        _httpClient = new()
        {
            BaseAddress = new Uri(_baseUrl)
        };
        _httpClient.DefaultRequestHeaders.Add("Cache-Control", "none");
        _httpClient.DefaultRequestHeaders.Add("x-apikey", apiKey);
    }

    public async Task Delete(string resource, string id)
    {
        using var response = await _httpClient.DeleteAsync($"{_baseUrl}/{resource}/{Uri.EscapeDataString(id)}");
        await EnsureSuccess(response);
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<List<T>> Get(string resource)
    {
        using var response = await _httpClient.GetAsync($"{_baseUrl}/{resource}");
        await EnsureSuccess(response);
        var result = await response.Content.ReadAsStringAsync();

        var list = JsonSerializer.Deserialize<List<T>>(result, _options) ?? [];
        return list;
    }

    public async Task<T> Get(string resource, string predicate)
    {
        var query = Uri.EscapeDataString(predicate);
        using var response = await _httpClient.GetAsync($"{_baseUrl}/{resource}?q={query}");
        var result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"RestDB request failed ({response.StatusCode}): {result}");
        }

        var data = JsonSerializer.Deserialize<List<T>>(result, _options);
        if (data is null || data.Count == 0)
        {
            throw new InvalidOperationException($"No matching record was found in '{resource}'.");
        }

        return data[0];
    }

    public async Task Post(string resource, T data)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        using var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{resource}", data, _options);
        await EnsureSuccess(response);
    }

    public async Task Put(string resource, T data, string objectId)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        using var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{resource}/{Uri.EscapeDataString(objectId)}", data, _options);
        await EnsureSuccess(response);
    }

    private static async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var result = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"RestDB request failed ({response.StatusCode}): {result}");
    }
}
