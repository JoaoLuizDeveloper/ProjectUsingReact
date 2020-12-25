using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsingReactAPI.Models;
using UsingReactAPI.Repository.IRepository;
using UsingReactAPI.Models.DTOs;

namespace UsingReactAPI.Controllers
{
    [Route("api/v{version:apiversion}/doctors")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class DoctorsController : ControllerBase
    {
        #region Construtor/Injection
        private readonly IDoctorRepository _doctor;
        private readonly IPatientRepository _patient;
        private readonly IMapper _mapper;

        public DoctorsController(IDoctorRepository doctor, IMapper mapper, IPatientRepository patient)
        {
            _doctor = doctor;
            _patient = patient;
            _mapper = mapper;
        }
        #endregion

        #region Get List of Doctors
        /// <summary>
        /// Get List of Doctors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(List<Doctor>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDoctors()
        {
            var objList = _doctor.GetDoctors();
            var objDTo = new List<Doctor>();

            foreach (var obj in objList)
            {
                objDTo.Add(_mapper.Map<Doctor>(obj));
            }

            return Ok(objDTo);
        }
        #endregion

        #region Get Individual Doctor
        /// <summary>
        /// Get Individual Doctor
        /// </summary>
        /// <param name="id">The id of the Doctor</param>
        /// <returns></returns>
        [HttpGet("{id:guid}", Name = "GetDoctor")]
        [ProducesResponseType(200, Type = typeof(Doctor))]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetDoctor(Guid id)
        {
            var obj = _doctor.GetDoctor(id);
            if (obj == null)
            {
                return NotFound();
            }

            var objDTO = _mapper.Map<Doctor>(obj);
            return Ok(objDTO);
        }
        #endregion

        #region Create, Update and Delete Doctor
        /// <summary>
        /// Create Doctor
        /// </summary>
        /// <param name="doctor">The Doctor</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Doctor))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateDoctor([FromBody] DoctorCreateDto doctor)
        {
            if (doctor == null)
            {
                return BadRequest(ModelState);
            }
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctorsObj = _mapper.Map<Doctor>(doctor);

            if (_doctor.CRMEndExists(doctorsObj.CRM + doctorsObj.CRMUF))
            {
                ModelState.AddModelError("", "This CRM already Exist");
                return StatusCode(404, ModelState);
            }

            doctorsObj.CRMEnd = doctorsObj.CRM + doctorsObj.CRMUF;

            if (!_doctor.CreateDoctor(doctorsObj))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to save {doctor.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetDoctor", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = doctorsObj.Id }, doctorsObj);
        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="doctorsDto">The Doctor</param>
        /// <returns></returns>
        [HttpPatch(Name = "UpdateDoctor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateDoctor([FromBody] DoctorUpdateDto doctorsDto)
        {
            if (doctorsDto == null)
            {
                return BadRequest(ModelState);
            }

            var objeto = _doctor.GetDoctor(doctorsDto.Id);
            var doctorsObj = _mapper.Map<Doctor>(doctorsDto);

            doctorsObj.CRMEnd = doctorsObj.CRM + "-" + doctorsObj.CRMUF;

            if (_doctor.CRMEndExists(doctorsObj.CRM + doctorsObj.CRMUF))
            {
                if(objeto.CRMEnd != doctorsObj.CRMEnd)
                {
                    ModelState.AddModelError("", "This CRM already Exist");
                    return StatusCode(404, ModelState);
                }                
            }

            if (!_doctor.UpdateDoctor(objeto))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to updating {doctorsDto.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete Doctor
        /// </summary>
        /// <param name="id">The Doctor</param>
        /// <returns></returns>
        [HttpDelete("{id:guid}", Name = "DeleteDoctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDoctor(Guid id)
        {
            if (!_doctor.DoctorExists(id))
            {
                return NotFound();
            }

            var doctorsDto = _doctor.GetDoctor(id);

            if (_patient.PatientExistsByDoctor(id))
            {
                ModelState.AddModelError("", "This Doctor have One or more Patients");
                return StatusCode(404, ModelState);
            }

            if (!_doctor.DeleteDoctor(doctorsDto))
            {
                ModelState.AddModelError("", $"Something went wrong when you trying to deleting {doctorsDto.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        #endregion
    }
}