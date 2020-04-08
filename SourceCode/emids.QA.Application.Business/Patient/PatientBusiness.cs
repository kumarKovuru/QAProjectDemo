using emids.QA.Application.DataAccess.Contracts;
using emids.QA.Application.DataAccess.Patient;
using System.Collections.Generic;

namespace emids.QA.Application.Business.Patient
{
    public class PatientBusiness : IPatientBusiness
    {
        private readonly IPatientDataAccess _patientDataAccess;

        public PatientBusiness()
        {
            _patientDataAccess = new PatientDataAccess();
        }
        public List<QA.Application.Common.Patient> GetPatientList()
        {
            return _patientDataAccess.GetPatientList();
        }
        public int Create(QA.Application.Common.Patient patient)
        {
            return _patientDataAccess.Create(patient);
        }

        public void Edit(QA.Application.Common.Patient patient)
        {
            _patientDataAccess.Edit(patient);
        }

        public void Delete(int patientId)
        {
            _patientDataAccess.Delete(patientId);
        }

        public QA.Application.Common.Patient GetById(int patientId)
        {
            return _patientDataAccess.GetById(patientId);
        }
    }
}
