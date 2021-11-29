using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Kafka.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kafka.Producer.Api.Application.Services
{
    public class KafkaProducerService
    {
        private readonly ProducerConfig _config;
        private readonly SchemaRegistryConfig _registryConfig;
        private readonly JsonSerializerConfig jsonSerializerConfig = new JsonSerializerConfig
        {
            // optional Avro serializer properties:
            BufferBytes = 100
        };

        public const string _topic = "ratings";

        public KafkaProducerService()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "broker:9092"
            };

            _registryConfig = new SchemaRegistryConfig
            {
                Url = "schema-registry:8081"
            };

           
        }


        public async Task SendMessage(User user)
        {
           
            using (var schemaRegistry = new CachedSchemaRegistryClient(_registryConfig))
            {
                using (var producer = new ProducerBuilder<Null, string>(_config)                   
                    .Build())
                {
                    await producer.ProduceAsync(_topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(user) });
                }
            }
        }
    }
}
