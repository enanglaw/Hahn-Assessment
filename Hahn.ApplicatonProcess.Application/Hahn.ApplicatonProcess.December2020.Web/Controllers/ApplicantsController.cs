using Hahn.ApplicatonProcess.December2020.Domain.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    public class ApplicantsController : Controller
    {
        readonly ILogger<ApplicantsController> _logger;
        readonly IApplicantService _applicantsService;

        public ApplicantsController(ILogger<ApplicantsController> logger, IApplicantService applicantService)
        {
            _logger = logger;
            _applicantsService = applicantService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Applicant>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetApplicants()
        {
            try
            {
                var applicants = await _applicantsService.GetApplicants();
                return Ok(applicants);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Applicant))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var applicant = await _applicantsService.GetApplicant(id);
                if (applicant == null)
                    return BadRequest($"Could not find any applicant with provided Id");
                return Ok(applicant);
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Applicant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ApplicantModel applicant)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var createResult = await _applicantsService.AddApplicant(applicant);
                return CreatedAtAction(nameof(Get), new { id = createResult.Id }, createResult);

            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] Applicant applicant)
        {
            try
            {
                var updated = await _applicantsService.UpdateApplicant(id, applicant);
                if (!updated)
                    return StatusCode((int)HttpStatusCode.Conflict, "Failed to save updates to applicant");
                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var updated = await _applicantsService.DeleteApplicant(id);
                if (!updated)
                    return StatusCode((int)HttpStatusCode.Conflict, $"Failed to delete applicant ");
                return NoContent();
            }
            catch (Exception exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, exception.Message);
            }
        }
    }
}
