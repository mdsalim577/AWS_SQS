using Amazon.SQS;
using Amazon.SQS.Model;

namespace AWS_SQS.Services;

public class SqsService
{
    private readonly AmazonSQSClient _sqsClient;

    public SqsService(AmazonSQSClient sqsClient)
    {
        _sqsClient = sqsClient;
    }

    public async Task<string> CreateQueueAsync(string queueName)
    {
        var createQueueRequest = new CreateQueueRequest
        {
            QueueName = queueName
        };
        var createQueueResponse = await _sqsClient.CreateQueueAsync(createQueueRequest);
        return createQueueResponse.QueueUrl;
    }

    public async Task SendMessageAsync(string queueUrl, string messageBody)
    {
        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = messageBody
        };
        await _sqsClient.SendMessageAsync(sendMessageRequest);
    }
}