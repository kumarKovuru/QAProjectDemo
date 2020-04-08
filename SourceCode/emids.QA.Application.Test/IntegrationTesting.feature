Feature: Integration Testing
	Description:  The purpose of this feature is to create, Read, Update and Delete the patient using Component and API.

@IntegrationTest
 Scenario Outline:1 CREATE - New Patient created successfully with valid data for all the fields
     Given user provides First Name as <firstName>
    And user provides Last Name as <lastName>
    And user provides Member Id as <memberId> 
	And user provides Date of Birth as <dateOfBirth> 
	And user provides Height Id as <height> 
	And user provides PhoneNumber Id as <phoneNumber> 
	And user provides Gender Id as <gender> 
	And user provides Weight Id as <weight>  
	When  User Calls NewPatientRegistration method	
    Then NewPatientRegistration is successful
	Examples:
	| firstName | lastName | memberId | dateOfBirth | height | phoneNumber | gender | weight |
	| Moso      | John     | 667      | 12-09-20    | 160.12 | 1234569990  | Male   | 76.67  |

	@IntegrationTest
 Scenario Outline:2 GET - Get newly created Patient
    When user Calls LastCreatedPatient method
	Then GetLastCreatedPatient is successful

	@IntegrationTest
  Scenario Outline:3 UPDATE - Patient Updated successfully with valid data for all the fields using API
    Given user provides First Name as <firstName>
    And user provides Last Name as <lastName>
    And user provides Member Id as <memberId> 
	And user provides Date of Birth as <dateOfBirth> 
	And user provides Height Id as <height> 
	And user provides PhoneNumber Id as <phoneNumber> 
	And user provides Gender Id as <gender> 
	And user provides Weight Id as <weight> 
	When  User Calls UpdatePatientIntegrationAPI method	
   Then UpdatePatientIntegrationAPI is successful
   	Examples:
	| firstName			| lastName			| memberId | dateOfBirth | height | phoneNumber | gender | weight |
	| Jojo Integration  | John Integration	| 123      | 03-03-20    | 175.12 | 1234567		| female | 50.67  |

