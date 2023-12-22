using System.Net.Http;
using System;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using NUnit.Framework;


namespace SpecFlowTests.Steps
{
    [Binding]
    public sealed class StepDefinitions
    {
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _createOrderResponse;
        private HttpResponseMessage _getOrderResponse;
        private readonly ScenarioContext _scenarioContext;

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
            var orderRequestContent = new StringContent("[{\"ProductName\": \"Apple\", \"Quantity\": 2}, {\"ProductName\": \"Banana\", \"Quantity\": 3}]");
            _createOrderResponse = await _client.PostAsync("http://localhost:5179/api/orders", orderRequestContent);
        }

        [Then("I should be able to retrieve the order details")]
        public async void ThenIShouldBeAbleToRetrieveTheOrderDetails()
        {
            // Assuming the order creation was successful and you have the order ID
            var orderId = "0";

            // Adjust the URL based on your API implementation
            _getOrderResponse = await _client.GetAsync($"http://localhost:5179/api/orders/{orderId}");

            Assert.True(_getOrderResponse.IsSuccessStatusCode);

            // You can add more assertions based on the response content if needed
            var responseContent = await _getOrderResponse.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(responseContent);
            Assert.NotNull(order);
            }
    }
}