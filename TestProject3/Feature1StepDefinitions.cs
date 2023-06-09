using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace TestProject3
{
    [Binding]
    public class Feature1StepDefinitions
    {
        RestClient client = new RestClient("https://jsonplaceholder.typicode.com/");
        RestRequest request = new RestRequest("posts/{postId}", Method.Get);
        RestResponse response;

        [Given(@"I have a id with value (.*)")]
        public void GivenIHaveAIdWithValue(int p0)
        {
            request.AddUrlSegment("postId", p0);
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
    }
}
