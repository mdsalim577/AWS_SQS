using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using AWS_SQS.Services;


namespace AWS_SQS;

public class Program
{
    static async Task Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var awsAccessKey = configuration["AWS:AccessKey"];
        var awsSecretKey = configuration["AWS:SecretKey"];
        var region = configuration["AWS:Region"];
        var queueName = configuration["SQS:QueueName"];
        var messageBody = configuration["SQS:MessageBody"];

        if (awsAccessKey != null && awsSecretKey != null && region != null && queueName != null &&
            messageBody != null)
        {
            Console.WriteLine("Creating sqs client");
            var sqsClient = AwsServiceFactory.CreateSqsClient(awsAccessKey, awsSecretKey, region);
            var sqsService = new SqsService(sqsClient);

            // Create SQS Queue
            var queueUrl = await sqsService.CreateQueueAsync(queueName);

            Console.WriteLine("Sending message to queue!");
            // Send Message to SQS Queue
            await sqsService.SendMessageAsync(queueUrl, messageBody);
        }

        Console.WriteLine("Message sent successfully!");
    }
}