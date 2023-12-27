using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FileFunction
{
    public static class UploadFunction
    {
        [FunctionName("UploadFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "files/upload")] HttpRequest req,
            [Blob("uploads/{rand-guid}", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream blobStream,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var formCollection = await req.ReadFormAsync();
                var file = formCollection.Files[0];

                await file.CopyToAsync(blobStream);

                return new OkObjectResult("File uploaded successfully!");
            }
            catch (Exception ex)
            {
                log.LogError($"Error uploading file: {ex.Message}");
                return new BadRequestObjectResult("File upload failed. Please try again.");
            }

        }
    }
}