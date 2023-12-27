using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFunction.Services
{
    internal class CalculationService
    {
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
