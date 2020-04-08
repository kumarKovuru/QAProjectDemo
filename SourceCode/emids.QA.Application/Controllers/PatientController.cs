using emids.QA.Application.Common.Config;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace emids.QA.Application.Controllers
{
    public class PatientController : Controller
    {
        private readonly IApplicationConfiguration _appConfig;
        public PatientController(IApplicationConfiguration appConfig)
        {
            _appConfig = appConfig;
        }
        public ActionResult Index()
        {
            List<Common.Patient> patients = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appConfig.WebAPIUrl);
                var responseTask = client.GetAsync("Patient/GetPatientList");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<Common.Patient>>();
                    readTask.Wait();
                    if (readTask.Result == null)
                        patients = new List<Common.Patient>();
                    else
                        patients = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(patients);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            Common.Patient patient = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appConfig.WebAPIUrl);
                var responseTask = client.GetAsync("Patient/GetPatientById?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Common.Patient>();
                    readTask.Wait();
                    patient = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(patient);
        }

        [HttpPost]
        public ActionResult Create(Common.Patient patient)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_appConfig.WebAPIUrl);
                    var responseTask = client.PostAsJsonAsync<Common.Patient>("Patient/CreatePatient", patient);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return RedirectToAction();
                    }
                }

            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            Common.Patient patient = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appConfig.WebAPIUrl);
                var responseTask = client.GetAsync("Patient/GetPatientById?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Common.Patient>();
                    readTask.Wait();
                    patient = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(patient);
        }

        // POST: Todo/Edit/5
        [HttpPost]
        public ActionResult Edit(Common.Patient patient)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_appConfig.WebAPIUrl);
                    var responseTask = client.PutAsJsonAsync<Common.Patient>("Patient/UpdatePatient", patient);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return RedirectToAction();
                    }
                }

            }
            catch
            {
                return View();
            }
        }

        // POST: Todo/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_appConfig.WebAPIUrl);
                    var responseTask = client.DeleteAsync("Patient/DeletePatient?id=" + id.ToString());
                    responseTask.Wait();
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return RedirectToAction();
                    }
                }

            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetById(int id)
        {
            Common.Patient patient = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appConfig.WebAPIUrl);
                var responseTask = client.GetAsync("Patient/GetPatientById?id=" + id.ToString());
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Common.Patient>();
                    readTask.Wait();
                    patient = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(patient);
        }
    }
}
