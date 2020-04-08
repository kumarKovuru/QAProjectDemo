using emids.QA.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace emids.QA.Application.Test
{
    [Binding]
    public class PatientUsingComponentSteps
    {
        private ActionResult _result;
        static Common.Patient _patient;
        private HttpResponseMessage _resultAPI;
        static SystemConfig system = new SystemConfig();

        [Given(@"user provides First Name as (.*)")]
        public void GivenUserProvidesFirstNameAs(string firstName)
        {
            if (!ScenarioContext.Current.ContainsKey("FirstName"))
            {
                ScenarioContext.Current.Add("FirstName", firstName);
            }
            else
            {
                ScenarioContext.Current["FirstName"] = firstName;
            }
        }

        [Given(@"user provides Last Name as (.*)")]
        public void GivenUserProvidesLastNameAs(string lastName)
        {
            if (!ScenarioContext.Current.ContainsKey("LastName"))
            {
                ScenarioContext.Current.Add("LastName", lastName);
            }
            else
            {
                ScenarioContext.Current["LastName"] = lastName;
            }
        }

        [Given(@"user provides Member Id as (.*)")]
        public void GivenUserProvidesMemberIdAs(string memberId)
        {
            if (!ScenarioContext.Current.ContainsKey("MemberId"))
            {
                ScenarioContext.Current.Add("MemberId", memberId);
            }
            else
            {
                ScenarioContext.Current["MemberId"] = memberId;
            }
        }

        [Given(@"user provides Date of Birth as (.*)")]
        public void GivenUserProvidesDateOfBirthAs(string dateOfBirth)
        {
            if (!ScenarioContext.Current.ContainsKey("DateOfBirth"))
            {
                ScenarioContext.Current.Add("DateOfBirth", dateOfBirth);
            }
            else
            {
                ScenarioContext.Current["DateOfBirth"] = dateOfBirth;
            }
        }

        [Given(@"user provides Height Id as (.*)")]
        public void GivenUserProvidesHeightIdAs(Decimal height)
        {
            if (!ScenarioContext.Current.ContainsKey("Height"))
            {
                ScenarioContext.Current.Add("Height", height);
            }
            else
            {
                ScenarioContext.Current["Height"] = height;
            }
        }

        [Given(@"user provides PhoneNumber Id as (.*)")]
        public void GivenUserProvidesPhoneNumberIdAs(int phoneNumber)
        {
            if (!ScenarioContext.Current.ContainsKey("PhoneNumber"))
            {
                ScenarioContext.Current.Add("PhoneNumber", phoneNumber);
            }
            else
            {
                ScenarioContext.Current["PhoneNumber"] = phoneNumber;
            }
        }

        [Given(@"user provides Gender Id as (.*)")]
        public void GivenUserProvidesGenderIdAs(string gender)
        {
            if (!ScenarioContext.Current.ContainsKey("Gender"))
            {
                ScenarioContext.Current.Add("Gender", gender);
            }
            else
            {
                ScenarioContext.Current["Gender"] = gender;
            }
        }

        [Given(@"user provides Weight Id as (.*)")]
        public void GivenUserProvidesWeightIdAs(Decimal weight)
        {
            if (!ScenarioContext.Current.ContainsKey("Weight"))
            {
                ScenarioContext.Current.Add("Weight", weight);
            }
            else
            {
                ScenarioContext.Current["Weight"] = weight;
            }
        }

        [When(@"User Calls NewPatientRegistrationAPIPending method")]
        public void WhenUserCallsNewPatientRegistrationAPIPendingMethod()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"User Calls NewPatientRegistration method")]
        public void WhenUserCallsNewPatientRegistrationMethod()
        {

            PatientController con = new PatientController(system._appConfig);
            Common.Patient patient = new Common.Patient()
            {
                FirstName = Convert.ToString((ScenarioContext.Current["FirstName"])),
                LastName = Convert.ToString((ScenarioContext.Current["LastName"])),
                MemberId = Convert.ToString((ScenarioContext.Current["MemberId"])),
                DateOfBirth = Convert.ToDateTime((ScenarioContext.Current["DateOfBirth"])),
                Gender = Convert.ToString((ScenarioContext.Current["Gender"])),
                Height = Convert.ToSingle((ScenarioContext.Current["Height"])),
                Weight = Convert.ToSingle((ScenarioContext.Current["Weight"])),
                PhoneNumber = Convert.ToString((ScenarioContext.Current["PhoneNumber"])),
            };
            _result = con.Create(patient);

        }

        [Then(@"NewPatientRegistration is successful")]
        public void ThenNewPatientRegistrationIsSuccessful()
        {
            var result = _result as RedirectToActionResult;
            var actionName = result.ActionName;
            Assert.AreEqual("Index", actionName.ToString());
        }

        [When(@"user Calls LastCreatedPatient method")]
        public void WhenUserCallsLastCreatedPatientMethod()
        {
            PatientController con = new PatientController(system._appConfig);
            var actionResult = con.Index();
            var viewResult = actionResult as ViewResult;
            var patients = ((List<Common.Patient>)viewResult.Model);
            _patient = patients.OrderByDescending(x => x.PatientId).FirstOrDefault();
        }

        [Then(@"GetLastCreatedPatient is successful")]
        public void ThenGetLastCreatedPatientIsSuccessful()
        {
            Assert.AreEqual(true, _patient != null);
        }

        [When(@"User Calls UpdatePatient method")]
        public void WhenUserCallsUpdatePatientMethod()
        {
            PatientController con = new PatientController(system._appConfig);
            Common.Patient patient = new Common.Patient()
            {
                FirstName = Convert.ToString((ScenarioContext.Current["FirstName"])),
                LastName = Convert.ToString((ScenarioContext.Current["LastName"])),
                MemberId = Convert.ToString((ScenarioContext.Current["MemberId"])),
                DateOfBirth = Convert.ToDateTime((ScenarioContext.Current["DateOfBirth"])),
                Gender = Convert.ToString((ScenarioContext.Current["Gender"])),
                Height = Convert.ToSingle((ScenarioContext.Current["Height"])),
                Weight = Convert.ToSingle((ScenarioContext.Current["Weight"])),
                PhoneNumber = Convert.ToString((ScenarioContext.Current["PhoneNumber"])),
                PatientId = _patient.PatientId
            };
            _result = con.Edit(patient);
        }


        [Then(@"UpdatePatient is successful")]
        public void ThenUpdatePatientIsSuccessful()
        {
            var result = _result as RedirectToActionResult;
            var actionName = result.ActionName;
            Assert.AreEqual("Index", actionName.ToString());
        }

        [When(@"user Calls LastUpdatedPatient method")]
        public void WhenUserCallsLastUpdatedPatientMethod()
        {
            PatientController con = new PatientController(system._appConfig);
            var actionResult = con.GetById(_patient.PatientId);
            var viewResult = actionResult as ViewResult;
            _patient = null;
            _patient = (Common.Patient)viewResult.Model;
        }

        [Then(@"GetLastUpdatedPatient is successful")]
        public void ThenGetLastUpdatedPatientIsSuccessful()
        {
            Assert.AreEqual(true, _patient != null);
        }

        [When(@"user Calls DeletePatient method")]
        public void WhenUserCallsDeletePatientMethod()
        {
            PatientController con = new PatientController(system._appConfig);
            _result = con.Delete(_patient.PatientId);
        }

        [Then(@"DeletePatient is successful")]
        public void ThenDeletePatientIsSuccessful()
        {
            var result = _result as RedirectToActionResult;
            var actionName = result.ActionName;
            Assert.AreEqual("Index", actionName.ToString());
        }

        [When(@"user Calls LastDeletedPatient method")]
        public void WhenUserCallsLastDeletedPatientMethod()
        {
            PatientController con = new PatientController(system._appConfig);
            var actionResult = con.GetById(_patient.PatientId);
            var viewResult = actionResult as ViewResult;
            _patient = null;
            _patient = (Common.Patient)viewResult.Model;
        }

        [Then(@"GetLastDeletedPatient is successful")]
        public void ThenGetLastDeletedPatientIsSuccessful()
        {
            Assert.AreEqual(true, _patient == null);
        }

        [When(@"User Calls UpdatePatientIntegrationAPI method")]
        public void WhenUserCallsUpdatePatientIntegrationAPIMethod()
        {
            Common.Patient patient = new Common.Patient()
            {
                FirstName = Convert.ToString((ScenarioContext.Current["FirstName"])),
                LastName = Convert.ToString((ScenarioContext.Current["LastName"])),
                MemberId = Convert.ToString((ScenarioContext.Current["MemberId"])),
                DateOfBirth = Convert.ToDateTime((ScenarioContext.Current["DateOfBirth"])),
                Gender = Convert.ToString((ScenarioContext.Current["Gender"])),
                Height = Convert.ToSingle((ScenarioContext.Current["Height"])),
                Weight = Convert.ToSingle((ScenarioContext.Current["Weight"])),
                PhoneNumber = Convert.ToString((ScenarioContext.Current["PhoneNumber"])),
                PatientId = _patient.PatientId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(system._appConfig.WebAPIUrl);
                var responseTask = client.PutAsJsonAsync<Common.Patient>("Patient/UpdatePatient", patient);
                responseTask.Wait();
                _resultAPI = responseTask.Result;
            }
        }
        [Then(@"UpdatePatientIntegrationAPI is successful")]
        public void ThenUpdatePatientIntegrationAPIIsSuccessful()
        {
            Assert.IsTrue(_resultAPI.IsSuccessStatusCode);
        }

    }

}
