using Amazon.S3;
using Amazon.S3.Model;
using WeddingManager.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace WeddingManager.Infraestructure.Storage
{
    public class S3FileStorage : IFileStorage
    {
        private readonly IAmazonS3 _s3client;
        private readonly string _bucketName;

        public S3FileStorage(IAmazonS3 s3client, IConfiguration configuration)
        {
            _s3client = s3client;
            _bucketName = configuration["AWS:BucketName"]
                ?? throw new InvalidOperationException("AWS BucketName is not configured");
        }

        public async Task<string> UploadAsync(Stream file, string key, string contentType)
        {
            
            
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = file,
                ContentType = contentType,
        

            };

            await _s3client.PutObjectAsync(request);

            return key;
        }
    }
}