using System;
using System.IO;
using System.Net;

public class ApiClient
{
    private string apiKey;
    public ApiClient(string apiKey)
    {
        this.apiKey = apiKey;
    }

    public string CallApi(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.Headers["WEATHER-API-KEY"] = apiKey;

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseText = streamReader.ReadToEnd();
                    return responseText;
                }
            }
            else
            {
                return "Error: " + response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
}