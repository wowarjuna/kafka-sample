using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Kafka.Common;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Consumer.Api.HostedService
{
    public class KafkaBackgroundService : BackgroundService
    {
        private IConsumer<string, string> _consumer;
        private readonly ConsumerConfig _config;
        const string topic = "ratings";

        public KafkaBackgroundService()
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = "broker:9092",
                EnableAutoCommit = false,
                EnableAutoOffsetStore = false,
                MaxPollIntervalMs = 300000,
                GroupId = "consumer-group",
                // Read messages from start if no commit exists.
                AutoOffsetReset = AutoOffsetReset.Earliest,


            };


        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            using (var _consumer = new ConsumerBuilder<Null, User>(_config)             
                .SetValueDeserializer(new JsonDeserializer<User>().AsSyncOverAsync())
                .Build())
            {
                _consumer.Subscribe(topic);

                while (true)
                {
                    try
                    {
                        var consumeResult = _consumer.Consume(cancellationToken);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Kafka message -> Key:{consumeResult?.Message?.Key}, Value:{consumeResult?.Message?.Value.id}");
                        Console.ForegroundColor = ConsoleColor.White;

                        _consumer.Commit(consumeResult);
                    }
                    catch (Exception ex)
                    {
                    }

                }


            }


            return Task.CompletedTask;



        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Unsubscribe();
            return Task.CompletedTask;
        }
    }
}
