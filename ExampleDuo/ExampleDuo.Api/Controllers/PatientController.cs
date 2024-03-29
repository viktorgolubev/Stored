using System.ComponentModel.DataAnnotations;
using ExampleDuo.Infrastructure.Interfaces.Managers;
using ExampleDuo.Infrastructure.Models.Domains;
using Microsoft.AspNetCore.Mvc;

namespace ExampleDuo.Api.Controllers;

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