using AutoMapper;
using UsingReactAPI.Models;
using UsingReactAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsingReactAPI.Mapper
{
    public class BuiltCodeMappings: Profile
    {
        public BuiltCodeMappings()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
            CreateMap<Patient, PatientUpdateDto>().ReverseMap();
            CreateMap<Patient, PatientCreateDto>().ReverseMap();
            CreateMap<Doctor, DoctorDto>().ReverseMap();
            CreateMap<Doctor, DoctorCreateDto>().ReverseMap();
            CreateMap<Doctor, DoctorUpdateDto>().ReverseMap();
        }
    }
}