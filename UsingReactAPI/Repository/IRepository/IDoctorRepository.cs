using UsingReactAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingReactAPI.Repository.IRepository
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(Guid doctorId);
        bool CRMEndExists(string crmEnd);
        bool DoctorExists(Guid id);
        bool CreateDoctor(Doctor doctor);
        bool UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(Doctor doctor);
        bool Save();
    }
}