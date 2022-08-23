using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidateAdminController : ControllerBase
{
    
    private readonly CandidateService _candidateService;

}