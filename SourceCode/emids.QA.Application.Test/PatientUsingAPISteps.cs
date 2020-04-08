using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace emids.QA.Application.Test
{
    [Binding]
    public class PatientUsingAPISteps
    {
        private HttpResponseMessage _result;
        static int patientId;
        static Common.Patient patient;
        static SystemConfig system = new SystemConfig();

        [When(@"User Calls NewPatientRegistrationAPI method")]
        public void WhenUserCallsNewPatientRegistrationAPIMethod()
        {
            Common.Patient _patient = new Common.Patient()
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(system._appConfig.WebAPIUrl);
                var responseTask = client.PostAsJsonAsync<Common.Patient>("Patient/CreatePatient", _patient);
                responseTask.Wait();
                _result = responseTask.Result;
            }
        }

        [Then(@"NewPatientRegistrationAPI is successful")]
        public void ThenNewPatientRegistrationAPIIsSuccessful()
        {
            using (HttpContent content = _result.Content)
            {
                // ... Read the string.
                Task<string> result = content.ReadAsStringAsync();
                patientId = Convert.ToInt32(result.Result);
            }
            Assert.IsTrue(_result.IsSuccessStatusCode);
        }

        [When(@"user Calls GetPatientByIdAPI method")]
        public void WhenUserCallsGetPatientByIdAPIMethod()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(system._appConfig.WebAPIUrl);
                var responseTask = client.GetAsync("Patient/GetPatientById?id=" + patientId.ToString());
                responseTask.Wait();
                _result = responseTask.Result;
            }
        }

        [Then(@"GetPatientByIdAPI is successful")]
        public void ThenGetPatientByIdAPIIsSuccessful()
        {
            Assert.IsTrue(_result.IsSuccessStatusCode);
            patient = null;
            if (_result.IsSuccessStatusCode)
            {
                var readTask = _result.Content.ReadAsAsync<Common.Patient>();
                readTask.Wait();
                patient = readTask.Result;
            }
            Assert.IsTrue(patient != null);
        }

        [When(@"User Calls UpdatePatientAPI method")]
        public void WhenUserCallsUpdatePatientAPIMethod()
        {
            Common.Patient _patient = new Common.Patient()
            {
                FirstName = Convert.ToString((ScenarioContext.Current["FirstName"])),
                LastName = Convert.ToString((ScenarioContext.Current["LastName"])),
                MemberId = Convert.ToString((ScenarioContext.Current["MemberId"])),
                DateOfBirth = Convert.ToDateTime((ScenarioContext.Current["DateOfBirth"])),
                Gender = Convert.ToString((ScenarioContext.Current["Gender"])),
                Height = Convert.ToSingle((ScenarioContext.Current["Height"])),
                Weight = Convert.ToSingle((ScenarioContext.Current["Weight"])),
                PhoneNumber = Convert.ToString((ScenarioContext.Current["PhoneNumber"])),
                PatientId = patient.PatientId
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(system._appConfig.WebAPIUrl);
                var responseTask = client.PutAsJsonAsync<Common.Patient>("Patient/UpdatePatient", _patient);
                responseTask.Wait();
                _result = responseTask.Result;
            }
        }
        [Then(@"UpdatePatientAPI is successful")]
        public void ThenUpdatePatientAPIIsSuccessful()
        {
            Assert.IsTrue(_result.IsSuccessStatusCode);
        }

        [When(@"user Calls DeletePatientAPI method")]
        public void WhenUserCallsDeletePatientAPIMethod()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(system._appConfig.WebAPIUrl);
                var responseTask = client.DeleteAsync("Patient/DeletePatient?id=" + patient.PatientId.ToString());
                responseTask.Wait();
                _result = responseTask.Result;
            }
        }
        [Then(@"DeletePatientAPI is successful")]
        public void ThenDeletePatientAPIIsSuccessful()
        {
            Assert.IsTrue(_result.IsSuccessStatusCode);
        }

        [Then(@"GetPatientByIdAPI is successful with null")]
        public void ThenGetPatientByIdAPIIsSuccessfulWithNull()
        {
            Assert.IsTrue(_result.IsSuccessStatusCode);
            patient = null;
            if (_result.IsSuccessStatusCode)
            {
                var readTask = _result.Content.ReadAsAsync<Common.Patient>();
                readTask.Wait();
                patient = readTask.Result;
            }
            Assert.IsTrue(patient == null);
        }

    }
}
