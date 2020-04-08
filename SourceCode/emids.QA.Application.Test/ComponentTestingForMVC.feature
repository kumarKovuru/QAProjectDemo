Feature: Component Testing For MVC
Description:  The purpose of this feature is to create, Read, Update and Delete the patient using Component.

@MVCTest
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

	@MVCTest
	Scenario:2 GET - Get newly created Patient
    When user Calls LastCreatedPatient method
	Then GetLastCreatedPatient is successful

	@MVCTest
  Scenario Outline:3 UPDATE - Patient Updated successfully with valid data for all the fields
    Given user provides First Name as <firstName>
    And user provides Last Name as <lastName>
    And user provides Member Id as <memberId> 
	And user provides Date of Birth as <dateOfBirth> 
	And user provides Height Id as <height> 
	And user provides PhoneNumber Id as <phoneNumber> 
	And user provides Gender Id as <gender> 
	And user provides Weight Id as <weight>   
	When  User Calls UpdatePatient method	
   Then UpdatePatient is successful
   Examples:
	| firstName		| lastName		| memberId | dateOfBirth | height | phoneNumber | gender | weight |
	| Moso Update   | John update   | 777      | 12-09-85    | 190.12 | 1234563330  | Female | 96.88  |

   @MVCTest
   Scenario:4 GET - Get newly Updated Patient
    When user Calls LastUpdatedPatient method
	Then GetLastUpdatedPatient is successful

	@MVCTest
    Scenario:5 DELETE - Patient Deleted successfully with Patient Id
     When user Calls DeletePatient method
	Then DeletePatient is successful

	@MVCTest
	Scenario:6 GET - Get newly Deleted Patient
    When user Calls LastDeletedPatient method
	Then GetLastDeletedPatient is successful
