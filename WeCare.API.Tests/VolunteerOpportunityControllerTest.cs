using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using Xunit;

namespace WeCare.API.Tests;

public class VolunteerOpportunityControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public VolunteerOpportunityControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Save__should_return_ok_with_volunteer_opportunity()
    {
        // var volunteerOpportunityForm = new VolunteerOpportunityForm
        // {
        //     Description = "desc",
        //     Name = "name",
        //     OpportunityDate = DateTime.Now.AddDays(30),
        //     Address = new AddressViewModel
        //     {
        //         City = "scs",
        //         Complement = "comp",
        //         Neighborhood = "neigh",
        //         Number = "1023",
        //         PostalCode = "09560-500",
        //         State = State.SP,
        //         Street = "teu pai"
        //     }
        // };
        //
        // var opportunityAsJson = JsonConvert.SerializeObject(volunteerOpportunityForm);
        //
        // var response = await _client.PostAsync("api/volunteer-opportunities", new StringContent(opportunityAsJson, Encoding.UTF8, "application/json"));
        //
        // response.EnsureSuccessStatusCode();
    }
}