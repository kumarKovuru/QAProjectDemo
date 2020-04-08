Feature: Component Testing For API
Description:  The purpose of this feature is to create, Read, Update and Delete the patient using API calls.

@APITesting
  Scenario Outline:1 CREATE - New Patient created successfully with valid data for all the fields using API
    Given user provides First Name as <firstName>
    And user provides Last Name as <lastName>
    And user provides Member Id as <memberId> 
	And user provides Date of Birth as <dateOfBirth> 
	And user provides Height Id as <height> 
	And user provides PhoneNumber Id as <phoneNumber> 
	And user provides Gender Id as <gender> 
	And user provides Weight Id as <weight> 
	When  User Calls NewPatientRegistrationAPI method	
    Then NewPatientRegistrationAPI is successful
	Examples:
	| firstName | lastName | memberId | dateOfBirth | height | phoneNumber | gender | weight |
	| Jojo      | John     | 456      | 03-04-20    | 170.12 | 1234567890  | Male   | 56.67  |

	@APITesting
	Scenario:2 GET - Get newly created Patient using API.
    When user Calls GetPatientByIdAPI method
	Then GetPatientByIdAPI is successful

	@APITesting
  Scenario Outline:3 UPDATE - Patient Updated successfully with valid data for all the fields using API
    Given user provides First Name as <firstName>
    And user provides Last Name as <lastName>
    And user provides Member Id as <memberId> 
	And user provides Date of Birth as <dateOfBirth> 
	And user provides Height Id as <height> 
	And user provides PhoneNumber Id as <phoneNumber> 
	And user provides Gender Id as <gender> 
	And user provides Weight Id as <weight> 
	When  User Calls UpdatePatientAPI method	
   Then UpdatePatientAPI is successful
   	Examples:
	| firstName			| lastName			| memberId | dateOfBirth | height | phoneNumber | gender | weight |
	| Jojo Updated      | John Updated		| 123      | 03-03-20    | 175.12 | 1234567		| female | 50.67  |

   @APITesting
   Scenario:4 GET - Get newly Updated Patient using API
    When user Calls GetPatientByIdAPI method
	Then GetPatientByIdAPI is successful

	@APITesting
    Scenario:5 DELETE - Patient Deleted successfully with Patient Id using API
     When user Calls DeletePatientAPI method
	Then DeletePatientAPI is successful

	@APITesting
	Scenario:6 GET - Get newly Deleted Patient using API
    When user Calls GetPatientByIdAPI method
	Then GetPatientByIdAPI is successful with null

	@APITesting
	Scenario:7 Pending Test
    Given user provides First Name as <firstName>
    And user provides Last Name as <lastName>
    And user provides Member Id as <memberId> 
	And user provides Date of Birth as <dateOfBirth> 
	And user provides Height Id as <height> 
	And user provides PhoneNumber Id as <phoneNumber> 
	And user provides Gender Id as <gender> 
	And user provides Weight Id as <weight>
	When  User Calls NewPatientRegistrationAPIPending method	
    Then NewPatientRegistrationAPI is successful
	Examples:
	| firstName | lastName | memberId | dateOfBirth | height | phoneNumber | gender | weight |
	| Jojo      | John     | 456      | 03-04-20    | 170.12 | 1234567890  | Male   | 56.67  |

	@APITesting
	Scenario:8 Failure Test
    Given user provides First Name as <firstName>
    And user provides Last Name as <lastName>
    And user provides Member Id as <memberId> 
	And user provides Date of Birth as <dateOfBirth> 
	And user provides Height Id as <height> 
	And user provides PhoneNumber Id as <phoneNumber> 
	And user provides Gender Id as <gender> 
	And user provides Weight Id as <weight>
	When  User Calls NewPatientRegistrationAPI method	
    Then NewPatientRegistrationAPI is successful
	Examples:
	| firstName | lastName | memberId | dateOfBirth | height | phoneNumber | gender | weight |
	| Jojo      | John     | 456      | 13-04-20    | 170.12 | 1234567890  | Male   | 56.67  |