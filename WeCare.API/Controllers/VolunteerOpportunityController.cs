using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/volunteer-opportunities")]
public class VolunteerOpportunityController
{
    private readonly VolunteerOpportunityService _volunteerOpportunityService;
    
    [HttpPost]
    public async ValueTask<ActionResult<VolunteerOpportunityResponse>> Save(VolunteerOpportunityForm form)
    {
        //TODO pegar o id da instituição de um token JWT por exemplo
        return await _volunteerOpportunityService.Save(1, form);
    }
}