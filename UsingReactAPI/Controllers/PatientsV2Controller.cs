using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsingReactAPI.Models;
using UsingReactAPI.Repository.IRepository;
using System;

namespace UsingReactAPI.Controllers
{
    [Route("api/v{version:apiversion}/patients")]
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PatientsV2Controller : ControllerBase
    {
        #region Construtor/Injection
        private readonly IPatientRepository _patients;
        private readonly IMapper _mapper;

        public PatientsV2Controller(IPatientRepository patients, IMapper mapper)
        {
            _patients = patients;
            _mapper = mapper;
        }
        #endregion

        #region Get Patients By Doctor
        /// <summary>
        /// Get Patients By Doctor
        /// </summary>
        /// <param name="doctorid">The id of the Doctor</param>
        /// <returns></returns>
        [HttpGet("{doctorid:guid}", Name = "GetPatientsByDoctor")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public IActionResult GetPatientsByDoctor(Guid doctorid)
        {
            var obj = _patients.GetPatientsByDoctor(doctorid);
            if (obj == null)
            {
                return NotFound();
            }

            return Ok(obj);
        }
        #endregion
    }
}