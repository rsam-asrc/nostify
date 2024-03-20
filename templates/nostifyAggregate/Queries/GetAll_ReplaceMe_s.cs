using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using nostify;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace _ReplaceMe__Service;

public class GetAll_ReplaceMe_s
{

    private readonly HttpClient _client;
    private readonly INostify _nostify;
    public GetAll_ReplaceMe_s(HttpClient httpClient, INostify nostify)
    {
        this._client = httpClient;
        this._nostify = nostify;
    }

    [Function(nameof(GetAll_ReplaceMe_s))]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "_ReplaceMe_")] HttpRequestData req,
        ILogger log)
    {
        Container currentStateContainer = await _nostify.GetCurrentStateContainerAsync<_ReplaceMe_>();
        List<_ReplaceMe_> allList = await currentStateContainer
                            .GetItemLinqQueryable<_ReplaceMe_>()
                            .ReadAllAsync();


        return new OkObjectResult(allList);
    }
}

