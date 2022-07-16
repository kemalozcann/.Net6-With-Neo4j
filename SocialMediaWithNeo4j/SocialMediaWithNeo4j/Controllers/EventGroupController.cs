using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SocialMediaWithNeo4j.Models;

namespace SocialMediaWithNeo4j.Controllers;

[ApiController]
[Route("[controller]")]
public class EventGroupController:ControllerBase
{
    private readonly IGraphClient _client;

    
    public EventGroupController(IGraphClient client)
    {
        _client = client;
    }
    
  
 
    [HttpPost("CreateFinanceEvent")]
    public async Task<IActionResult> CreateFinanceEvent([FromBody]Finance FinanceEvent, int idk)
    {
        
        await _client.Cypher.Match("(i:User)")
            .Where((User i) => i.Id==idk)
            .Create("(f:Finance $FinanceEvent) ")
            .WithParam("FinanceEvent", FinanceEvent)
            .ExecuteWithoutResultsAsync();

        return Ok();
    }
    
    [HttpPost("CreateSporEvent")]
    public async Task<IActionResult> CreateSporEvent([FromBody]Spor SporEvent, int idk)
    {
        await _client.Cypher.Match("(i:User)")
            .Where((User i) => i.Id==idk)
            .Create("(S:Spor $SporEvent)")
            .WithParam("SporEvent", SporEvent)
            .ExecuteWithoutResultsAsync();

        return Ok();
    }
    
    [HttpPost("CreateTechEvent")]
    public async Task<IActionResult> CreateTechEvent([FromBody]Tech TechEvent ,int idk )
    {
        await _client.Cypher.Match("(i:User)")
            .Where((User i) => i.Id==idk)
            .Create("(T:Tech $TechEvent)")
            .WithParam("TechEvent", TechEvent)
            .ExecuteWithoutResultsAsync();

        return Ok();
    }
  
    [HttpGet("GetFinanceAll")]
    public async Task<IActionResult> GetFinanceAll()
    {
        var Finance = await _client.Cypher.Match("(f:Finance)")
            .Return(f => f.As<Finance>()).ResultsAsync;

        return Ok(Finance);
    }
    
    [HttpGet("GetTechAll")]
    public async Task<IActionResult> GetTechAll()
    {
        var Tech = await _client.Cypher.Match("(t:Tech)")
            .Return(t => t.As<Tech>()).ResultsAsync;

        return Ok(Tech);
    }
    
    [HttpGet("GetSporAll")]
    public async Task<IActionResult> GetSporAll()
    {
        var Spor = await _client.Cypher.Match("(s:Spor)")
            .Return(s => s.As<Spor>()).ResultsAsync;

        return Ok(Spor);
    }

    [HttpPut("UpdateFinanceEvent")]
    public async Task<IActionResult> UpdateFinanceEvent(int fid, [FromBody]Finance FinanceEvent)
    {
        await _client.Cypher.Match("(f:Finance)")
            .Where((Finance f) => f.Fid == fid)
            .Set("f =$FinanceEvent")
            .WithParam("FinanceEvent", FinanceEvent)
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }
    [HttpPut("UpdateSporEvent")]
    public async Task<IActionResult> UpdateSporEvent(int sid, [FromBody]Spor SporEvent)
    {
        await _client.Cypher.Match("(s:Spor)")
            .Where((Spor s) => s.Sid == sid)
            .Set("s =$Spor")
            .WithParam("SporEvent", SporEvent)
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }
    [HttpPut("UpdateTechEvent")]
    public async Task<IActionResult> UpdateTechEvent(int tid, [FromBody]Tech TechEvent)
    {
        await _client.Cypher.Match("(t:Tech)")
            .Where((Tech t) => t.Tid == tid)
            .Set("t=$Tech")
            .WithParam("TechEvent", TechEvent)
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }
    
    [HttpDelete("DeleteFinanceEvent")]
    public async Task<IActionResult> DeleteFinanceEvent(int fid, [FromBody]Finance FinanceEvent)
    {
        await _client.Cypher.Match("(f:Finance)")
            .Where((Finance f) => f.Fid == fid)
            .DetachDelete("f")
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }
    
    [HttpDelete("DeleteSporEvent")]
    public async Task<IActionResult> DeleteSporEvent(int sid, [FromBody]Spor SporEvent)
    {
        await _client.Cypher.Match("(s:Spor)")
            .Where((Spor s) => s.Sid == sid)
            .DetachDelete("s")
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }
    
    [HttpDelete("DeleteTechEvent")]
    public async Task<IActionResult> DeleteTechEvent(int tid, [FromBody]Tech TechEvent)
    {
        await _client.Cypher.Match("(t:Tech)")
            .Where((Tech t) => t.Tid == tid)
            .DetachDelete("t")
            .ExecuteWithoutResultsAsync();

        return Ok();
        
    }
    
 
    
}


