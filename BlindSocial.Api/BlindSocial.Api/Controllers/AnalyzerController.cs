using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;

namespace BlindSocial.Api.Controllers
{
    public class AnalyzerController : ApiController
    {
        private IVisionServiceClient visionServiceClient;

        public AnalyzerController() : this(new VisionServiceClient(ConfigurationManager.AppSettings.Get("vision:subscriptionKey"), ConfigurationManager.AppSettings.Get("vision:uriBase")))
        {

        }

        private AnalyzerController(IVisionServiceClient visionServiceClient)
        {
            this.visionServiceClient = visionServiceClient;
        }

        [HttpGet]
        public async Task<string> AnalyzeUrl(string imageUrl)
        {
            VisualFeature[] visualFeatures = new VisualFeature[] { VisualFeature.Adult, VisualFeature.Categories, VisualFeature.Color, VisualFeature.Description, VisualFeature.Faces, VisualFeature.ImageType, VisualFeature.Tags };
            AnalysisResult analysisResult = await this.visionServiceClient.AnalyzeImageAsync(imageUrl, visualFeatures);

            return await TranslateText(analysisResult.Description.Captions.First().Text);
        }
        
        private async Task<string> TranslateText(string textToTranslate)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ConfigurationManager.AppSettings.Get("translate:subscriptionKey"));

            HttpResponseMessage response = await client.GetAsync($"{ ConfigurationManager.AppSettings.Get("translate:uriBase") }?to=es-es&text={ System.Net.WebUtility.UrlEncode(textToTranslate) }");

            return XElement.Parse(await response.Content.ReadAsStringAsync()).Value;
        }
    }
}
