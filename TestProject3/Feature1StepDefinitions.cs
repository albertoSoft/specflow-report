using Gherkin;
using Newtonsoft.Json.Linq;
using NUnit.Framework.Constraints;
using RestSharp;
using System;
using System.Net;
using System.Text.Json.Nodes;
using TechTalk.SpecFlow;

namespace TestProject3
{
    [Binding]
    public class Feature1StepDefinitions
    {
        RestClient client = new RestClient("https://demostore.gatling.io/");
        RestRequest request = new RestRequest("api/product/{productId}", Method.Get);
        RestResponse response;
        

       [Given(@"I have a id with value (.*)")]
        public void GivenIHaveAIdWithValue(int p0)
        {
            request.AddUrlSegment("productId", p0);
        }

        [When(@"I send a GET request")]
        public void WhenISendAGETRequest()
        {
            response = client.Execute(request);
        }

        [Then(@"I expected a valid code response")]
        public void ThenIExpectedAValidCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I expected product name is ""([^""]*)""")]
        public void ThenIExpectedProductNameIs(string p0)
        {
            JObject jsonObject = JObject.Parse(response.Content);
            string result = jsonObject.SelectToken("name").ToString();
            Assert.That(result, Is.EqualTo(p0), "Name is not correct");
        }




        //Scenario 2 : 
        RestRequest requestPost = new RestRequest("api/product", Method.Post);
        string token;

        [Given(@"I have correct product data")]
        public void GivenIHaveCorrectProductData()
        {
            var productData = new
            {
                name = "Casual Black-Blue2",
                slug = "casual-black-blue2",
                description = "<p>2Some casual black &amp; blue glasses</p>",
                image = "casual-blackblue-open.jpg",
                price = "25.99",
                categoryId = "5"
            };
            requestPost.AddJsonBody(productData);
            
        }

        [Given(@"I don´t have token")]
        public void GivenIDonTHaveToken()
        {
            token = null;
            requestPost.AddHeader("Authorization", $"Bearer {token}");
        }

        [When(@"I send a POST request")]
        public void WhenISendAPOSTRequest()
        {
            response = client.Execute(requestPost);
        }

        [Then(@"I expected a forbidden code response")]
        public void ThenIExpectedAForbiddenCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [Then(@"I expected error value is  ""([^""]*)""")]
        public void ThenIExpectedErrorValueIs(string forbidden)
        {
            var jsonObject = JObject.Parse(response.Content);
            var result = jsonObject.SelectToken("error").ToString();
            Assert.That(result, Is.EqualTo("Forbidden"), "error is not Forbidden");
        }

        // Scenario 3 :

        [Given(@"I have valid token")]
        public void GivenIHaveValidToken()
        {
            RestRequest requestAuth = new RestRequest("/api/authenticate", Method.Post);
            requestAuth.AddJsonBody(new { username = "admin", password = "admin" });
            var responseAuth = client.Execute(requestAuth);
            Assert.That(responseAuth.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var jsonObject = JObject.Parse(responseAuth.Content);
            var token = jsonObject.SelectToken("token").ToString();

            requestPost.AddHeader("Authorization", $"Bearer {token}");
        }

        [Then(@"I expected a created code response")]
        public void ThenIExpectedACreatedCodeResponse()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"I expected id returned is (.*)")]
        public void ThenIExpectedIdReturnedIs(string p0)
        {
            JObject jsonObject = JObject.Parse(response.Content);
            string result = jsonObject.SelectToken("id").ToString();
            Assert.That(result, Is.EqualTo(p0), "id is not 0");
        }


    }
}
