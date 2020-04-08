using emids.QA.Application.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace emids.QA.Application.Test
{
    [TestFixture]
    class UnitTestCases
    {
        static Common.Patient patient;
        PatientController controller;
        SystemConfig system;

        [SetUp]
        public void Setup()
        {
            system = new SystemConfig();
            controller = new PatientController(system._appConfig);
        }

        [TearDown]
        public void Teardown()
        {
            controller.Dispose();
        }

        [TestCase("John", "Abraham", "Mem1234", "12/12/2000", "Male", 6.2F, 70.5F, "1234567890"), Order(1)]
        public void Test_Add_Patient_with_validData(string firstName, string lastName, string memberId, DateTime dateOfBirth, string gender, float height, float weight, string phoneNumber)
        {
            patient = new Common.Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                MemberId = memberId,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Height = height,
                Weight = weight,
                PhoneNumber = phoneNumber,
            };
            var result = controller.Create(patient);
            var _result = result as RedirectToActionResult;
            var actionName = _result.ActionName;
            Assert.AreEqual("Index", actionName.ToString());
        }

        [Test, Order(2)]
        public void Test_Get_Patient_List()
        {
            var actionResult = controller.Index();
            var viewResult = actionResult as ViewResult;
            var patients = ((List<Common.Patient>)viewResult.Model);
            patient = patients.OrderByDescending(x => x.PatientId).FirstOrDefault();
            Assert.AreEqual(true, patient != null);
        }

        [TestCase("John", "Abraham", "Mem1234", "12/12/2000", "Male", 6.2F, 70.5F, "1234567890"), Order(3)]
        public void Test_Update_Patient_with_ValidData(string firstName, string lastName, string memberId, DateTime dateOfBirth, string gender, float height, float weight, string phoneNumber)
        {
            patient = new Common.Patient()
            {
                FirstName = firstName,
                LastName = lastName,
                MemberId = memberId,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Height = height,
                Weight = weight,
                PhoneNumber = phoneNumber,
                PatientId = patient.PatientId
            };
            var result = controller.Edit(patient);
            var _result = result as RedirectToActionResult;
            var actionName = _result.ActionName;
            Assert.AreEqual("Index", actionName.ToString());
        }

        [Test, Order(4)]
        public void Test_Delete_Patient()
        {
            var result = controller.Delete(patient.PatientId);
            var _result = result as RedirectToActionResult;
            var actionName = _result.ActionName;
            Assert.AreEqual("Index", actionName.ToString());
        }
    }
}
