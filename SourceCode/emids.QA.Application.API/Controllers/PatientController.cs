using emids.QA.Application.Business;
using emids.QA.Application.Business.Patient;
using emids.QA.Application.Common.Config;
using Microsoft.AspNetCore.Mvc;

namespace emids.QA.Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBusiness _patientBusiness;

        public PatientController(IPatientBusiness patientBusiness)
        {
            _patientBusiness = patientBusiness;
        }

        [HttpGet]
        [Route("GetPatientList")]
        public IActionResult GetPatientList()
        {
            var patients = _patientBusiness.GetPatientList();
            return Ok(patients);
        }

        [HttpGet]
        [Route("GetPatientById")]
        public IActionResult GetById(int id)
        {
            var patient = _patientBusiness.GetById(id);
            return Ok(patient);
        }

        [HttpPost]
        [Route("CreatePatient")]
        [ServiceFilter(typeof(ModelValidationErrorHandlerFilter), Order = 1)]
        public IActionResult Create(Common.Patient patient)
        {
            int id = _patientBusiness.Create(patient);
            return Ok(id);
        }

        [HttpPut]
        [Route("UpdatePatient")]
        [ServiceFilter(typeof(ModelValidationErrorHandlerFilter), Order = 1)]
        public ActionResult Edit(Common.Patient patient)
        {
            _patientBusiness.Edit(patient);
            return Ok();
        }

        [HttpDelete]
        [Route("DeletePatient")]
        public ActionResult Delete(int id)
        {
            _patientBusiness.Delete(id);
            return Ok();
        }
    }
}

