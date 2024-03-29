using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExampleStar.Infrastructure.Interfaces.Managers;
using ExampleStar.Infrastructure.Models.Domains;

namespace ExampleStar.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IPatientManager patientManager) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Patient>> GetPatientByIdAsync(int id, CancellationToken token)
    {
        return Ok(await patientManager.GetPatientByIdAsync(id, token));
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Patient>>> SearchPatient(
        string? firstName,
        [Required] string lastName,
        CancellationToken token)
    {
        return Ok(await patientManager.SearchPatientsAsync(firstName, lastName, token));
    }
}