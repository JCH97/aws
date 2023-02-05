using System.Text;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3Playground;

public static class DownloadFile
{
    public static async Task Download()
    {
        var basicCredentials = new BasicAWSCredentials("", "");
        var s3client = new AmazonS3Client(basicCredentials, RegionEndpoint.USWest1);

        var getObjectRequest = new GetObjectRequest
        {
            BucketName = "learning",
            Key = "files/f.csv"
        };

        var response = await s3client.GetObjectAsync(getObjectRequest);
        using var memoryStream = new MemoryStream();

        await response.ResponseStream.CopyToAsync(memoryStream);

        var text = Encoding.Default.GetString(memoryStream.ToArray());
    }
}