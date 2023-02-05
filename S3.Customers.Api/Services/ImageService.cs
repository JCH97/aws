using Amazon.S3;
using Amazon.S3.Model;

namespace S3.Customers.Api.Services;

public interface IImageService
{
    Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file);

    Task<GetObjectResponse> GetImageAsync(Guid id);

    Task<DeleteObjectResponse> DeleteImageAsync(Guid id);
}

public class ImageService : IImageService
{
    private readonly IAmazonS3 _s3Client;

    public ImageService(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file)
    {
        var putObjectRequest = new PutObjectRequest
        {
            BucketName = "learning", //pass the bucket name by configuration,
            Key = $"images/{id}",
            ContentType = file.ContentType,
            InputStream = file.OpenReadStream(),
            Metadata =
            {
                ["x-amz-meta-originalname"] = file.FileName,
                ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
            }
        };

        return await _s3Client.PutObjectAsync(putObjectRequest);
    }

    public async Task<GetObjectResponse> GetImageAsync(Guid id)
    {
        var getObjectRequest = new GetObjectRequest
        {
            BucketName = "learning",
            Key = $"images/{id}"
        };

        return await _s3Client.GetObjectAsync(getObjectRequest);
    }

    public async Task<DeleteObjectResponse> DeleteImageAsync(Guid id)
    {
        var deleteObjectRequest = new DeleteObjectRequest()
        {
            BucketName = "learning",
            Key = $"images/{id}"
        };

        return await _s3Client.DeleteObjectAsync(deleteObjectRequest);
    }
}