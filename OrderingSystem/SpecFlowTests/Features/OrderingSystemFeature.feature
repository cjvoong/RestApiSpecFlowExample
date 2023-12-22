Feature: Ordering System API

@mytag
Scenario: Check API
    Given the API is running
    When I create an order
    Then I should be able to retrieve the order details
