using System.Net.Http;
using System;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using NUnit.Framework;
using RestSharp;
using domain;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using SpecFlow.Internal.Json;
using RestSharp.Serializers.Json;
using System.Collections.Generic;


namespace SpecFlowTests.Steps
{
    [Binding]
    public sealed class StepDefinitions
    {
        private RestResponse _response;

        private readonly ScenarioContext _scenarioContext;
        private string locationValue;
        public StepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the API is running")]
        public void GivenTheApiIsRunning()
        {
            Console.WriteLine("The api is running");
        }

        [When("I create an order")]
        public async void WhenICreateAnOrder()
        {
            HeaderParameter locationHeader = null;
            var orderRequestContent = "[{\"ProductName\": \"Apple\", \"Quantity\": 2}, {\"ProductName\": \"Banana\", \"Quantity\": 3}]";
            var client = new RestClient("http://localhost:5179");
            var request = new RestRequest("api/orders", Method.Post);
            // Set the request content type and add JSON data
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", orderRequestContent, ParameterType.RequestBody);
            _response = client.Execute(request);
            _response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Access the Location header
            foreach (var header in _response.Headers)
            {
                if (header.Name.Equals("Location", StringComparison.OrdinalIgnoreCase))
                {
                    locationHeader = header;
                    break;
                }
            }

            locationHeader.Should().NotBeNull();
            Console.WriteLine(locationHeader.Value.ToString());
            locationValue = locationHeader.Value.ToString();    
        }

        [Then("I should be able to retrieve the order details")]
        public void ThenIShouldBeAbleToRetrieveTheOrderDetails()
        {
            // Assuming the order creation was successful and you have the order ID
            var orderId = locationValue.Substring(locationValue.LastIndexOf('/') + 1);

            var client = new RestClient("http://localhost:5179");
            var request = new RestRequest($"api/orders/{orderId}", Method.Get);

            // Execute the request
            _response = client.Execute(request);

            // Assert the response status code
            _response.StatusCode.Should().Be(HttpStatusCode.OK);
            var unescapedString = _response.Content.Replace("\\", "");
            var startIndex = 1;
            var endIndex = unescapedString.Length - 1;
            unescapedString = unescapedString.Substring(startIndex, endIndex - startIndex);
            // Console.WriteLine(unescapedString);
            // Deserialize the response content to an Order object  
            var order = JsonConvert.DeserializeObject<Order>(unescapedString);

            // Assert order details
            order.Should().NotBeNull();

            Console.WriteLine(order.GetOrderSummary());
        }
    }
}