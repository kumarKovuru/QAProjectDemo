using System;
using System.Collections.Generic;
using System.Text;

namespace emids.QA.Application.Business
{
    public interface IPatientBusiness
    {
        List<Common.Patient> GetPatientList();
        int Create(Common.Patient patient);
        void Edit(QA.Application.Common.Patient patient);
        void Delete(int patientId);
        QA.Application.Common.Patient GetById(int patientId);

    }
}
