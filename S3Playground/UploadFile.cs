using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;

namespace S3Playground;

public static class UploadFile
{
    public static async Task Upload()
    {
        var basicCredentials = new BasicAWSCredentials("", "");

        var s3client = new AmazonS3Client(basicCredentials, RegionEndpoint.USWest1);

        await using var inputStream = new FileStream("./face.jpg", FileMode.Open, FileAccess.Read);

        var putObjectRequest = new PutObjectRequest
        {
            BucketName = "learning",
            Key = "files/f.csv", // this is the location/name of the file in S3 bucket
            ContentType = "text/csv",
            InputStream = inputStream
        };

        await s3client.PutObjectAsync(putObjectRequest);
    }
}