using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string stationId = "94672"; // Adelaide Airport station ID
        string jsonUrl = $"http://www.bom.gov.au/fwo/IDS60901/IDS60901.{stationId}.json";

        try
        {
            List<double> temperatures = await GetTemperatureData(jsonUrl);

            Console.WriteLine(temperatures);

            if (temperatures.Count > 0)
            {
                double averageTemperature = CalculateAverageTemperature(temperatures);
                Console.WriteLine($"Average Temperature for the Previous 72 Hours: {averageTemperature:F2} °C");
            }
            else
            {
                Console.WriteLine("No temperature data available.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }



    /*
    static async Task<List<double>> GetTemperatureData(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            // Add headers if required, e.g., user-agent
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                List<double> temperatures = ParseJsonResponse(jsonResponse);

                return temperatures;
            }
            else
            {
                throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
            }
        }
    }

*/


        static async Task<List<double>> GetTemperatureData(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<double> temperatures = ParseJsonResponse(jsonResponse);

                    return temperatures;
                }
                else
                {
                    throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                }
            }
        }


        
    static List<double> ParseJsonResponse(string jsonResponse)
    {
        // Implement the logic to parse the JSON response and extract temperature data
        // Return a list of temperature values
        // This is just a placeholder and may not work for your specific API response structure
        return new List<double>();
    }

    static double CalculateAverageTemperature(List<double> temperatures)
    {
        // Implement the logic to calculate the average temperature from the list of temperature values
        // Return the average temperature
        if (temperatures.Count > 0)
        {
            double sum = 0;

            foreach (double temperature in temperatures)
            {
                sum += temperature;
            }

            return sum / temperatures.Count;
        }
        else
        {
            
            return 0.0;
        }
    }
}
