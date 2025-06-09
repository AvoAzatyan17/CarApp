using Minio;
using Minio.DataModel.Args;

namespace CarApp.Storage;

public class MinioStorageService
{
    private readonly MinioClient _minioClient;

    public MinioStorageService(MinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    private const string BucketName = "car-images";

    public async Task UploadFileAsync(IFormFile file, string objectName)
    {
        using var stream = file.OpenReadStream();
        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(BucketName)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(file.Length)
            .WithContentType(file.ContentType));
    }

    public string GetFileUrl(string objectName)
    {
        return $"http://localhost:9000/{BucketName}/{objectName}";
    }

}