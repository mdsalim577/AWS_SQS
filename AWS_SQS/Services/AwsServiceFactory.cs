using Amazon;
using Amazon.SQS;

namespace AWS_SQS.Services;

public class AwsServiceFactory
{
    public static AmazonSQSClient CreateSqsClient(string accessKey, string secretKey, string region)
    {
        return new AmazonSQSClient(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
    }
}