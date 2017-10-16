#r "C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Net.Http.dll"

using System.Net;
using System.Net.Http;

async Task PostJsonData(string url, string jsonContent)
{
    Console.WriteLine($"Post request, Url={url}, JsonContent={jsonContent}");

    var client = new HttpClient();
    var response = await client.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));
    if (response.StatusCode != HttpStatusCode.OK)
    {
        throw new Exception("Couldn't post json data");
    }
    else
    {
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Post response, Content={content}");
    }
}
