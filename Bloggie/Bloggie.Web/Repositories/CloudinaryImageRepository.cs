﻿
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
