using UsingReactAPI.Data;
using UsingReactAPI.Models;
using UsingReactAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingReactAPI.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _db;
        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreatePatient(Patient patient)
        {
            patient.DateCreated = DateTime.Now;
            _db.Patients.Add(patient);
            return Save();
        }

        public bool DeletePatient(Patient patients)
        {
            _db.Patients.Remove(patients);
            return Save();
        }

        public Patient GetPatient(Guid patientId)
        {
            return _db.Patients.FirstOrDefault(n => n.Id == patientId);
        }

        public ICollection<Patient> GetPatients()
        {
            return _db.Patients.OrderBy(n => n.Name).ToList();
        }
        
        public ICollection<Patient> GetPatientsByDoctor(Guid id)
        {
            return _db.Patients.Where(x=>x.DoctorId == id).OrderBy(n => n.Name).ToList();
        }

        public bool PatientExists(string name)
        {
            bool value = _db.Patients.Any(n => n.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }
        
        public ICollection<Patient> PatientCPFExists(long cpf)
        {

            ICollection<Patient> value = _db.Patients.Where(n => n.CPF.Replace(".","").Replace("-","").ToLower().Trim() == cpf.ToString().ToLower().Trim()).ToList();
            return value;
        }

        public bool PatientExists(Guid id)
        {
            bool value = _db.Patients.Any(n => n.Id == id);
            return value;
        }
        
        public bool PatientExistsByDoctor(Guid id)
        {
            bool value = _db.Patients.Any(n => n.DoctorId == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdatePatient(Patient patients)
        {
            _db.Patients.Update(patients);
            return Save();
        }
    }
}
