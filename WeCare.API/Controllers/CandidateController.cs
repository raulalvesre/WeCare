using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;
using WeCare.Domain;

namespace WeCare.API.Controllers;

[ApiController]
[Route("candidate")]
public class CandidateController : ControllerBase
{
    private readonly CandidateService _candidateService;

    public CandidateController(CandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet("{id:int}")]
    public Candidate GetById(int id)
    {
        return _candidateService.GetById(id);
    }
}