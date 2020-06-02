using JARS.Core.Client;
using ServiceStack;
using System;
using System.Globalization;

namespace JARS.Test.SS.Clients
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceClientHelper sch = new ServiceClientHelper();
            ServerEventsClient eventsClient = GlobalContext.Instance.CreateServerEventsClient("test");
            eventsClient.OnMessage = msg => Console.WriteLine($"OnMessage - {msg.Data}");
            eventsClient.OnJoin = jn => Console.WriteLine($"OnJoin - {jn.DisplayName}");
            eventsClient.OnCommand = cm => Console.WriteLine($"OnCommand - {cm.Json}");
            //eventsClient.OnReconnect => Console.WriteLine($"OnReconnect - ");
            eventsClient.OnException = ex => Console.WriteLine($"OnException - {ex.Message}");
            eventsClient.Start();           

            string key = ""; 
            while (key != "exit")
            {
                key = Console.ReadLine();

                if (key == "id")
                    Console.WriteLine("My Id:{0}", eventsClient.ConnectionInfo.Id);
                if (key == "notify")
                    foreach (var sub in eventsClient.GetChannelSubscribers())
                    {
                        Console.WriteLine($"Get Channel subs:{sub.Channels} - {sub.UserId}");
                    }
                if (key == "data")
                {
                    Console.WriteLine($"Post a SyncEventData object to the test channel.");
                    //eventsClient.ServiceClient.Post(new SyncEventData
                    //{
                    //    Channel = "test",
                    //    Selector = "test",
                    //    //From = eventsClient.SubscriptionId,
                    //    FromUserId = eventsClient.ConnectionInfo.UserId,
                    //    Message = $"From Client {eventsClient.ConnectionInfo.Id}",
                    //    EntityInfo = new EventEntityInfo
                    //    {
                    //        EntityType = "".GetType(),
                    //        EntityRecordId = 1,
                    //        ActionTriggered = "CREATE"
                    //    }
                    //});
                }
                if (key.StartsWithIgnoreCase("sub"))
                {
                    Console.WriteLine($"Subscribe and Post a SyncEventData object to the {key.Substring(key.IndexOf("-"))} channel.");
                    eventsClient.SubscribeToChannels(key.Substring(key.IndexOf("-")));
                    //eventsClient.ServiceClient.Post(new SyncEventData
                    //{
                    //    Channel = key.Substring(key.IndexOf("-")),
                    //    Selector = "test",
                    //    //From = eventsClient.SubscriptionId,
                    //    FromUserId = eventsClient.ConnectionInfo.UserId,
                    //    Message = $"From Client {eventsClient.ConnectionInfo.Id} on chanel{eventsClient.ConnectionInfo.Channel}",
                    //    EntityInfo = new EventEntityInfo
                    //    {
                    //        EntityType = "".GetType(),
                    //        EntityRecordId = 1,
                    //        ActionTriggered = "CREATE"
                    //    }
                    //});
                }
                if (key.StartsWith("unsub", true, CultureInfo.CurrentCulture))
                {
                    Console.WriteLine($"Un-Subscribe and Post a SyncEventData object to the {key.Substring(key.IndexOf("-"))} channel.");
                    eventsClient.UnsubscribeFromChannels(key.Substring(key.IndexOf("-")));
                    //this should fail?
                    //eventsClient.ServiceClient.Post(new SyncEventData
                    //{
                    //    Channel = key.Substring(key.IndexOf("-")),
                    //    Selector = "test",
                    //    //From = eventsClient.SubscriptionId,
                    //    FromUserId = eventsClient.ConnectionInfo.UserId,
                    //    Message = $"From Client {eventsClient.ConnectionInfo.Id} on chanel{eventsClient.ConnectionInfo.Channel}",
                    //    EntityInfo = new EventEntityInfo
                    //    {
                    //        EntityType = "".GetType(),
                    //        EntityRecordId = 1,
                    //        ActionTriggered = "CREATE"
                    //    }
                    //});
                }

            }


        }
    }
}
