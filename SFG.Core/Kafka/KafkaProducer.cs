using System;
using Confluent.Kafka;

namespace SFG.Core.Kafka
{
	public class KafkaProducer
	{
        private readonly ProducerConfig _config;
        private readonly ProducerConfig _config1;
        private readonly IProducer<string, string> _producer;

        public KafkaProducer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092" // Change this to your Kafka server
            };

            _producer = new ProducerBuilder<string, string>(_config).Build();

            //_config1 = new ProducerConfig
            //{
            //    BootstrapServers = "localhost:9091" // Change this to your Kafka server
            //};

            //_producer = new ProducerBuilder<string, string>(_config1).Build();
        }

        public async Task ProduceAsync(string topic, string message)
        {
            var result = await _producer.ProduceAsync(topic, new Message<string, string> { Value = message });
            Console.WriteLine($"Delivered message '{result.Value}' to '{result.TopicPartitionOffset}'");
        }

        public void Dispose() => _producer.Dispose();
    }
}

