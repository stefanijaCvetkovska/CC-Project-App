using System.Diagnostics;
using System.Text;
using web_api.Models;

namespace web_api.Services
{
    public class ResultService: IResultService
    {
        public async Task<Result> CalculateAsync(IFormFile file)
        {
            try
            {
                using (var streamReader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
                {
                    int sum = await CalculateFromStreamAsync(streamReader);

                    return new Result
                    {
                        Status = 0,
                        Message = "Calculation successful",
                        Sum = sum
                    };
                }
            }
            catch (FormatException)
            {
                return new Result { Status = 1, Message = "Wrong input format. Please provide a valid file." };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in CalculateAsync: {ex}");
                return new Result { Status = 2, Message = "Error in calculation. Please check the file content." };
            }
        }

        public async Task<int> CalculateFromStreamAsync(StreamReader streamReader)
        {
            int sum = 0;

            // Concurrently read and sum the numbers asynchronously
            await foreach (var line in LinesAsync(streamReader))
            {
                if (int.TryParse(line, out int number))
                {
                    sum += number;
                }
            }

            return sum;
        }

        private async IAsyncEnumerable<string> LinesAsync(StreamReader streamReader)
        {
            string line;
            while ((line = await streamReader.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }
    }
}
