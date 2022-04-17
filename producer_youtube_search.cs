using Confluent.Kafka;
using System;
using Microsoft.Extensions.Configuration;

class Producer_youtube_search {

    static void Main(string[] args)
    {
        var count = 999;
        if (args.Length < 1) {
            Console.WriteLine("Please provide the configuration file path as a command line argument");
        }
        else if (args.Length > 1) {
            count = Convert.ToInt32(args[1]);
        }   

        IConfiguration configuration = new ConfigurationBuilder()
            .AddIniFile(args[0])
            .Build();

        string topic = (args.Length ==3)? args[2] : "youtube_json_2";
	    string text;

        while ((text = Console.ReadLine()) != "exit" && count > 0)
        {
            using (var producer = new ProducerBuilder<string, string>(
                configuration.AsEnumerable()).Build())
            {
                var surrogate_key = text;
                text = Console.ReadLine();
                var numProduced = 0;
                const int numMessages = 5;
                for (int i = 0; i < numMessages; ++i)
                {
                
                    producer.Produce(topic, new Message<string, string> { Key = surrogate_key, Value = text },
                        (deliveryReport) =>
                        {
                            if (deliveryReport.Error.Code != ErrorCode.NoError) {
                                Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            }
                            else {
                                Console.WriteLine($"Produced event to topic {topic}: key = {surrogate_key} value = {text}");
                                numProduced += 1;
                            }
                        });
                }

                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
                if (args.Length > 1)   count--;
            }
        }
    }
}

