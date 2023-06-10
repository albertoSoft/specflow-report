Feature: Feature1

A short summary of the feature

@positive @smoke @positive @regretion @integration @JIRA-7777
Scenario: Get Product By id
	Given I have a id with value 17
	When  I send a GET request
	Then I expected a valid code response
	And  I expected product name is "Casual Black-Blue"

 @negative
Scenario: No product is created without token
	Given I have correct product data
	But I don´t have token
	When I send a POST request
	Then I expected a forbidden code response
	And  I expected error value is  "Forbidden"

	@positive
Scenario: New product is created without token
	Given I have correct product data
	And I have valid token
	When I send a POST request
	Then I expected a created code response
	And  I expected id returned is 0