Feature: TestAmazonScenario
	In order to test the product search and cart services


@smoke
Scenario: Test Product and Cart service
	Given I have product 'bucket' to search
	And I Select Sort By filter 'Price: Low to High' option
	And  Take the No.of Products from list and add to cart
	Then the cart should match the products selected in product search

@regression
Scenario: Test Product and Cart service test two
	Given I have product 'bat' to search
	And I Select Sort By filter 'Price: Low to High' option
	And  Take the No.of Products from list and add to cart
	Then the cart should match the products selected in product search