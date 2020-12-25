using UsingReactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingReactAPI.Repository.IRepository
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        ICollection<Patient> GetPatientsByDoctor(Guid id);
        Patient GetPatient(Guid patientsId);
        bool PatientExists(string name);
        bool PatientExists(Guid id);
        bool PatientExistsByDoctor(Guid id);
        ICollection<Patient> PatientCPFExists(long cpf);
        bool CreatePatient(Patient patient);
        bool UpdatePatient(Patient patient);
        bool DeletePatient(Patient patient);
        bool Save();
    }
}