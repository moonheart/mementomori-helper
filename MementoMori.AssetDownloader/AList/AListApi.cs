using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MementoMori.AssetDownloader.Alist;

internal class AListApi
{
    private readonly HttpClient _httpClient;

    public AListApi(string baseUrl)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    private async Task<TResp?> Post<TReq, TResp>(string url, TReq request)
    {
        var response = await _httpClient.PostAsJsonAsync(url, request);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Response<TResp>>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return result == null ? default : result.Data;
    }

    public async Task AuthLogin(string username, string password)
    {
        var result = await Post<AuthLoginRequest, AuthLoginResponse>("api/auth/login", new AuthLoginRequest
        {
            Username = username,
            Password = password
        });

        if (result == null) throw new Exception("AList Login failed");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"{result.Token}");
    }

    public async Task<FsGetResponse?> FsGet(string path)
    {
        return await Post<FsGetRequest, FsGetResponse>("api/fs/get", new FsGetRequest
        {
            Path = path
        });
    }

    public async Task<FsMkdirResponse?> FsMkdir(string path)
    {
        return await Post<FsMkdirRequest, FsMkdirResponse>("api/fs/mkdir", new FsMkdirRequest
        {
            Path = path
        });
    }

    public async Task FsPut(string path, byte[] data, string contentType)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, "api/fs/put");
        httpRequestMessage.Content = new ByteArrayContent(data);
        // use url encoded path
        httpRequestMessage.Content.Headers.Add("File-Path", UrlEncoder.Default.Encode(path));
        // content Type and content length are required
        httpRequestMessage.Content.Headers.Add("Content-Type", contentType);
        httpRequestMessage.Content.Headers.Add("Content-Length", data.Length.ToString());

        var response = await _httpClient.SendAsync(httpRequestMessage);
        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Response<object>>(content, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }

    public async Task<FsListResponse?> FsList(string path)
    {
        return await Post<FsListRequest, FsListResponse>("api/fs/list", new FsListRequest
        {
            Path = path, Page = 1, PerPage = 1000, Refresh = false
        });
    }
}

internal class AuthLoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

internal class AuthLoginResponse
{
    public string Token { get; set; }
}

internal class FsGetRequest
{
    public string Path { get; set; }
}

internal class FsGetResponse : FsItem
{
    [JsonPropertyName("raw_url")]
    public string RawUrl { get; set; }

    public string Readme { get; set; }
    public string Provider { get; set; }
    public object? Related { get; set; }
}

internal class FsMkdirRequest
{
    public string Path { get; set; }
}

internal class FsMkdirResponse
{
}

internal class FsListRequest
{
    public string Path { get; set; }
    public string Password { get; set; }
    public int Page { get; set; }
    [JsonPropertyName("per_page")]
    public int PerPage { get; set; }
    public bool Refresh { get; set; }
}

internal class FsListResponse
{
    public string Provider { get; set; }
    public bool Write { get; set; }
    public string Readme { get; set; }
    public int Total { get; set; }
    public List<FsItem> Content { get; set; }

}

internal class FsItem
{
    public string Name { get; set; }
    public long Size { get; set; }

    [JsonPropertyName("is_dir")]
    public bool IsDir { get; set; }

    public string Modified { get; set; }
    public string Sign { get; set; }
    public string Thumb { get; set; }
    public int Type { get; set; }

}

internal class Response<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
}