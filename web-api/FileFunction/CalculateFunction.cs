using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using FileFunction.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;


namespace FileFunction
{
    public class CalculateFunction
    {
        [FunctionName("CalculateFunction")]
        public static async Task Run(
        [BlobTrigger("uploads/{file}", Connection = "AzureWebJobsStorage")] Stream blobStream, string file,
        ILogger log)
        {
            log.LogInformation($"C# Blob trigger function processed blob\n Name:{file} \n Size: {blobStream.Length} Bytes");

            var resultService = new CalculationService();

            try
            {
                // Pass the stream to your existing logic for sum calculation
                int sum = await resultService.CalculateFromStreamAsync(new StreamReader(blobStream));
                log.LogInformation($"Sum calculated: {sum}");
            }
            catch (FormatException)
            {
                log.LogError("Wrong input format. Please provide a valid file.");
            }
            catch (Exception ex)
            {
                log.LogError($"Error calculating sum: {ex.Message}");
            }
        }
    }
}
