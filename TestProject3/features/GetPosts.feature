Feature: Feature1

A short summary of the feature

@positive @smoke @positive @regretion @integration @JIRA-7777
Scenario: Get By id
	Given I have a id with value 1
	When  I send a GET request
	Then I expected a valid code response
