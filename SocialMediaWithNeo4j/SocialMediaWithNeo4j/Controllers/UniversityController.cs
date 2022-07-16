using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SocialMediaWithNeo4j.Models;

namespace SocialMediaWithNeo4j.Controllers;

[ApiController]
[Route("[controller]")]
public class UniversityController:ControllerBase
{ 
    private readonly IGraphClient _client;

    public UniversityController(IGraphClient client)
    {
        _client = client;
    }
        
    [HttpPost("CreateUniversity")]
    public async Task<IActionResult> CreateUniversity([FromBody]University uni)
    {
        await _client.Cypher.Create("(u:University $uni)")
            .WithParam("uni", uni)
            .ExecuteWithoutResultsAsync();
        return Ok();
        
    }

    [HttpGet("GetUniversityName")]
    public async Task<IActionResult> GetUniversity(string idk)
    {
        var University = await _client.Cypher.Match("(u:University)")
            .Where((University u)=>u.Uni==idk)
            .Return(u => u.As<University>()).ResultsAsync;

        return Ok(University);
    }
    
    [HttpPut("UpdateUniversity")]
    public async Task<IActionResult> UpdateUniversity(int Uid, [FromBody]University Uni)
    {
        await _client.Cypher.Match("(u:University)")
            .Where((University u) => u.Uid == Uid)
            .Set("u =$University")
            .WithParam("University", Uni)
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }

      
    [HttpDelete("DeleteUniversity")]
    public async Task<IActionResult> DeleteUniversity(int Uid, [FromBody] University Uni)
    {
        await _client.Cypher.Match("(u:University)")
            .Where((University u) => u.Uid == Uid)
            .DetachDelete("u")
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }
    
  
        
}


  