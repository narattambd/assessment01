using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

[Route("api/weather")]
[ApiController]
public class WeatherController : ControllerBase
{
    private const string BaseUrl = "http://www.bom.gov.au/fwo/IDS60901/IDS60901.{0}.json";

    [HttpGet("{stationId}")]
    public async Task<IActionResult> GetAllWeatherData(string stationId)
    {
        try
        {
            string apiUrl = string.Format(BaseUrl, stationId);
            var data = await GetWeatherData(apiUrl);

            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{stationId}/{observationType}")]
    public async Task<IActionResult> GetSpecificWeatherData(string stationId, string observationType)
    {
        try
        {
            string apiUrl = string.Format(BaseUrl, stationId);
            var data = await GetWeatherData(apiUrl);

            if (data.ContainsKey(observationType))
            {
                return Ok(data[observationType]);
            }
            else
            {
                return NotFound($"Observation type '{observationType}' not found for station '{stationId}'.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    private async Task<Dictionary<string, string>> GetWeatherData(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Assuming the API response is in JSON format
                string jsonResponse = await response.Content.ReadAsStringAsync();
                return ParseJsonResponse(jsonResponse);
            }
            else
            {
                throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
            }
        }
    }

    private Dictionary<string, string> ParseJsonResponse(string jsonResponse)
    {
        // Implement the logic to parse the JSON response and extract weather observation data
        // Return a dictionary with observation types and their corresponding values
        // This is just a placeholder and may not work for your specific API response structure
        return new Dictionary<string, string>();
    }
}














