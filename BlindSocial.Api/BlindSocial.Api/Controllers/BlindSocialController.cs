using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlindSocial.Api.Controllers
{
    public class BlindSocialController : ApiController
    {
        // Anayze api/bytedata
        //public async Task<string> Analyze(byte[] byteData)
        //{
        //    string result = await MakeAnalysisRequest(byteData);
        //    return result;
        //}

        private async Task<AnalysisResult> AnalyzeUrl(string imageUrl)
        {
            //
            // Create Project Oxford Computer Vision API Service client
            //
            VisionServiceClient VisionServiceClient = new VisionServiceClient(ConfigurationManager.AppSettings.Get("subscriptionKey"), ConfigurationManager.AppSettings.Get("uriBase"));

            //
            // Analyze the url for all visual features
            //
            VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };
            AnalysisResult analysisResult = await VisionServiceClient.AnalyzeImageAsync(imageUrl, visualFeatures);
            return analysisResult;
        }

        //public async Task<string> MakeAnalysisRequest(byte[] byteData)
        //{
        //    HttpClient client = new HttpClient();

        //    // Request headers.
        //    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

        //    // Request parameters. A third optional parameter is "details".
        //    string requestParameters = "visualFeatures=Categories,Description,Color&language=en";

        //    // Assemble the URI for the REST API Call.
        //    string uri = uriBase + "?" + requestParameters;

        //    HttpResponseMessage response;

        //    string contentString = string.Empty;

        //    using (ByteArrayContent content = new ByteArrayContent(byteData))
        //    {
        //        // This example uses content type "application/octet-stream".
        //        // The other content types you can use are "application/json" and "multipart/form-data".
        //        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        //        // Execute the REST API call.
        //        response = await client.PostAsync(uri, content);

        //        // Get the JSON response.
        //        contentString = await response.Content.ReadAsStringAsync();

        //    }

        //    return JsonPrettyPrint(contentString);
        //}
        

        ///// <summary>
        ///// Formats the given JSON string by adding line breaks and indents.
        ///// </summary>
        ///// <param name="json">The raw JSON string to format.</param>
        ///// <returns>The formatted JSON string.</returns>
        //private string JsonPrettyPrint(string json)
        //{
        //    if (string.IsNullOrEmpty(json))
        //        return string.Empty;

        //    json = json.Replace(Environment.NewLine, "").Replace("\t", "");

        //    StringBuilder sb = new StringBuilder();
        //    bool quote = false;
        //    bool ignore = false;
        //    int offset = 0;
        //    int indentLength = 3;

        //    foreach (char ch in json)
        //    {
        //        switch (ch)
        //        {
        //            case '"':
        //                if (!ignore) quote = !quote;
        //                break;
        //            case '\'':
        //                if (quote) ignore = !ignore;
        //                break;
        //        }

        //        if (quote)
        //            sb.Append(ch);
        //        else
        //        {
        //            switch (ch)
        //            {
        //                case '{':
        //                case '[':
        //                    sb.Append(ch);
        //                    sb.Append(Environment.NewLine);
        //                    sb.Append(new string(' ', ++offset * indentLength));
        //                    break;
        //                case '}':
        //                case ']':
        //                    sb.Append(Environment.NewLine);
        //                    sb.Append(new string(' ', --offset * indentLength));
        //                    sb.Append(ch);
        //                    break;
        //                case ',':
        //                    sb.Append(ch);
        //                    sb.Append(Environment.NewLine);
        //                    sb.Append(new string(' ', offset * indentLength));
        //                    break;
        //                case ':':
        //                    sb.Append(ch);
        //                    sb.Append(' ');
        //                    break;
        //                default:
        //                    if (ch != ' ') sb.Append(ch);
        //                    break;
        //            }
        //        }
        //    }

        //    return sb.ToString().Trim();
        //}
    
}
}
