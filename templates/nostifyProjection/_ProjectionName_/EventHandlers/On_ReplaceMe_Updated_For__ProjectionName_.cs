using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using nostify;
using Newtonsoft.Json;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json.Linq;

namespace _ReplaceMe__Service;

public class On_ReplaceMe_Updated_For__ProjectionName_
{
    private readonly INostify _nostify;
    private readonly HttpClient _httpClient;

    public On_ReplaceMe_Updated_For__ProjectionName_(INostify nostify, HttpClient httpClient)
    {
        this._nostify = nostify;
        _httpClient = httpClient;
    }

    [Function(nameof(On_ReplaceMe_Updated_For__ProjectionName_))]
    public async Task Run([KafkaTrigger("BrokerList",
                "Update__ReplaceMe_",
//-:cnd:noEmit
                #if DEBUG
                Protocol = BrokerProtocol.NotSet,
                AuthenticationMode = BrokerAuthenticationMode.NotSet,
                #else
                Username = "KafkaApiKey",
                Password = "KafkaApiSecret",
                Protocol =  BrokerProtocol.SaslSsl,
                AuthenticationMode = BrokerAuthenticationMode.Plain,
                #endif
//+:cnd:noEmit
                ConsumerGroup = "_ProjectionName_")] NostifyKafkaTriggerEvent triggerEvent,
        ILogger log)
    {
        Event? newEvent = triggerEvent.GetEvent();
        try
        {
            if (newEvent != null)
            {
                //Update projection container
                Container projectionContainer = await _nostify.GetProjectionContainerAsync<_ProjectionName_>();
                var updatedProj = await projectionContainer.ApplyAndPersistAsync<_ProjectionName_>(newEvent);
                await updatedProj.InitAsync(_nostify, _httpClient);
            }                       

        }
        catch (Exception e)
        {
            await _nostify.HandleUndeliverableAsync(nameof(On_ReplaceMe_Updated_For__ProjectionName_), e.Message, newEvent);
        }

        
    }
    
}

