using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Steps
{
    [Binding]
    public sealed class StepDefinitions
    {
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
        public void WhenICreateAnOrder()
        {
            Console.WriteLine("I create an order");

        }

        [Then("I should be able to retrieve the order details")]
        public void ThenIShouldBeAbleToRetrieveTheOrderDetails()
        {
            Console.WriteLine("I should be able to retrieve the order details");
        }

    }
}