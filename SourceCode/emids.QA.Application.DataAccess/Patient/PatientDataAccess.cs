using ADO.DataAccessHelper;
using emids.QA.Application.Common;
using emids.QA.Application.DataAccess.Contracts;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace emids.QA.Application.DataAccess.Patient
{
    public class PatientDataAccess : IPatientDataAccess
    {
        public List<QA.Application.Common.Patient> GetPatientList()
        {
            List<QA.Application.Common.Patient> patientList = null;
            patientList = DataAccess<MySqlClientFactory>
                .ExecuteReaderProcedure("Sp_GetPatientList",
                (reader) =>
                {
                    if (reader.HasRows)
                    {
                        patientList = new List<QA.Application.Common.Patient>();
                        while (reader.Read())
                        {
                            patientList.Add(new QA.Application.Common.Patient
                            {
                                PatientId = ConverterHelper.ConvertIntColumnValue(reader["PatientId"]),
                                FirstName = ConverterHelper.GetStringValue(reader["FirstName"]),
                                LastName = ConverterHelper.GetStringValue(reader["LastName"]),
                                MemberId = ConverterHelper.GetStringValue(reader["MemberId"]),
                                DateOfBirth = ConverterHelper.ConvertDateColumnValue(reader["DateOfBirth"]),
                                Gender = ConverterHelper.GetStringValue(reader["Gender"]),
                                Height = ConverterHelper.ConvertNumberToSingle(reader["Height"]),
                                Identifier = ConverterHelper.GetStringValue(reader["Identifier"]),
                                PhoneNumber = ConverterHelper.GetStringValue(reader["PhoneNumber"]),
                                Weight = ConverterHelper.ConvertNumberToSingle(reader["Weight"]),
                            });
                        }
                    }
                    return patientList;
                }, null);
            return patientList;
        }

        public int Create(QA.Application.Common.Patient patient)
        {
            try
            {
                MySqlParameter[] parameters = new MySqlParameter[8];
                parameters[0] = new MySqlParameter()
                {
                    ParameterName = "@FirstName",
                    Value = patient.FirstName,
                    DbType = DbType.String
                };
                parameters[1] = new MySqlParameter()
                {
                    ParameterName = "@LastName",
                    Value = patient.LastName,
                    DbType = DbType.String
                };
                parameters[2] = new MySqlParameter()
                {
                    ParameterName = "@MemberId",
                    Value = patient.MemberId,
                    DbType = DbType.String
                };
                parameters[3] = new MySqlParameter()
                {
                    ParameterName = "@DateOfBirth",
                    Value = patient.DateOfBirth,
                    DbType = DbType.Date
                };
                parameters[4] = new MySqlParameter()
                {
                    ParameterName = "@PhoneNumber",
                    Value = patient.PhoneNumber,
                    DbType = DbType.String
                };
                parameters[5] = new MySqlParameter()
                {
                    ParameterName = "@Height",
                    Value = patient.Height,
                    DbType = DbType.Single
                };
                parameters[6] = new MySqlParameter()
                {
                    ParameterName = "@Weight",
                    Value = patient.Weight,
                    DbType = DbType.Single
                };
                parameters[7] = new MySqlParameter()
                {
                    ParameterName = "@Gender",
                    Value = patient.Gender,
                    DbType = DbType.String
                };

                int patientId = 0;
                patient = DataAccess<MySqlClientFactory>
                    .ExecuteReaderProcedure("Sp_SavePatient",
                    (reader) =>
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                patientId = ConverterHelper.ConvertIntColumnValue(reader["PatientId"]);
                            }
                        }
                        return patient;
                    }, parameters);
                return patientId;
            }
            catch
            {

                throw;
            }
        }

        public void Edit(QA.Application.Common.Patient patient)
        {
            try
            {
                MySqlParameter[] parameters = new MySqlParameter[9];
                parameters[0] = new MySqlParameter()
                {
                    ParameterName = "@PatientId",
                    Value = patient.PatientId,
                    DbType = DbType.Int32
                };
                parameters[1] = new MySqlParameter()
                {
                    ParameterName = "@FirstName",
                    Value = patient.FirstName,
                    DbType = DbType.String
                };
                parameters[2] = new MySqlParameter()
                {
                    ParameterName = "@LastName",
                    Value = patient.LastName,
                    DbType = DbType.String
                };
                parameters[3] = new MySqlParameter()
                {
                    ParameterName = "@MemberId",
                    Value = patient.MemberId,
                    DbType = DbType.String
                };
                parameters[4] = new MySqlParameter()
                {
                    ParameterName = "@DateOfBirth",
                    Value = patient.DateOfBirth,
                    DbType = DbType.Date
                };
                parameters[5] = new MySqlParameter()
                {
                    ParameterName = "@PhoneNumber",
                    Value = patient.PhoneNumber,
                    DbType = DbType.String
                };
                parameters[6] = new MySqlParameter()
                {
                    ParameterName = "@Height",
                    Value = patient.Height,
                    DbType = DbType.Single
                };
                parameters[7] = new MySqlParameter()
                {
                    ParameterName = "@Weight",
                    Value = patient.Weight,
                    DbType = DbType.Single
                };
                parameters[8] = new MySqlParameter()
                {
                    ParameterName = "@Gender",
                    Value = patient.Gender,
                    DbType = DbType.String
                };

                DataAccess<MySqlClientFactory>
                     .ExecuteProcedure("Sp_UpdatePatient", parameters);
            }
            catch
            {

                throw;
            }
        }

        public void Delete(int patientId)
        {
            try
            {
                MySqlParameter[] parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter()
                {
                    ParameterName = "@PatientId",
                    Value = patientId,
                    DbType = DbType.Int32
                };

                DataAccess<MySqlClientFactory>
                     .ExecuteProcedure("Sp_DeletePatient", parameters);
            }
            catch
            {

                throw;
            }
        }

        public QA.Application.Common.Patient GetById(int patientId)
        {
            try
            {
                MySqlParameter[] parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter()
                {
                    ParameterName = "@PatientId",
                    Value = patientId,
                    DbType = DbType.Int32
                };

                QA.Application.Common.Patient patient = null;
                patient = DataAccess<MySqlClientFactory>
                    .ExecuteReaderProcedure("Sp_GetPatientById",
                    (reader) =>
                    {
                        if (reader.HasRows)
                        {
                            patient = new QA.Application.Common.Patient();
                            while (reader.Read())
                            {
                                patient = (new QA.Application.Common.Patient
                                {
                                    PatientId = ConverterHelper.ConvertIntColumnValue(reader["PatientId"]),
                                    FirstName = ConverterHelper.GetStringValue(reader["FirstName"]),
                                    LastName = ConverterHelper.GetStringValue(reader["LastName"]),
                                    MemberId = ConverterHelper.GetStringValue(reader["MemberId"]),
                                    DateOfBirth = ConverterHelper.ConvertDateColumnValue(reader["DateOfBirth"]),
                                    Gender = ConverterHelper.GetStringValue(reader["Gender"]),
                                    Height = ConverterHelper.ConvertNumberToSingle(reader["Height"]),
                                    Identifier = ConverterHelper.GetStringValue(reader["Identifier"]),
                                    PhoneNumber = ConverterHelper.GetStringValue(reader["PhoneNumber"]),
                                    Weight = ConverterHelper.ConvertNumberToSingle(reader["Weight"]),
                                });
                            }
                        }
                        return patient;
                    }, parameters);
                return patient;
            }
            catch
            {

                throw;
            }
        }
    }
}
