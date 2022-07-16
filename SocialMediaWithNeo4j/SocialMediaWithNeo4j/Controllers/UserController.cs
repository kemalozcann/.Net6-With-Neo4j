using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using SocialMediaWithNeo4j.Models;

namespace SocialMediaWithNeo4j.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase
{
   private readonly IGraphClient _client;

   public UserController(IGraphClient client)
   {
      _client = client;
   }


   [HttpPost("CreateUser")]
   public async Task<IActionResult> CreateUser([FromBody]User usr)
   {
      await _client.Cypher.Create("(u:User $usr)")
         .WithParam("usr", usr)
         .ExecuteWithoutResultsAsync();
      
      return Ok();
   }
   
   [HttpGet("GetUser")]
   public async Task<IActionResult> GetUser()
   {
      var User = await _client.Cypher.Match("(u:User)")
         .Return(u => u.As<User>()).ResultsAsync;

      return Ok(User);
   }
  

   [HttpGet("GetUserId")]
   public async Task<IActionResult> GetUserId(int id)
   {
      var User = await _client.Cypher.Match("(u:User)")
         .Where((User u)=>u.Id==id)
         .Return(u => u.As<User>()).ResultsAsync;

      return Ok(User.LastOrDefault());
   }


   [HttpGet("UserUniversityRelationShip")]
   public async Task<IActionResult> UserUniversityRelationShip(int Id, string Uni)
   {
      await _client.Cypher.Match("(u:User),(uni:University)")
         .Where((User u, University uni) => u.Id == Id && uni.Uni == Uni)
         .Create("(u)-[r:HasUniversity]->(uni)")
         .ExecuteWithoutResultsAsync();
      
      return Ok();
   }

   [HttpGet("UserFinanceEventRelationShip")]
   public async Task<IActionResult> UserFinanceEventRelationShip(int Id, string CommunityEventGroup)
   {
      await _client.Cypher.Match("(u:User), (f:Finance)")
         .Where((User u,Finance f) => u.Id == Id && u.CommunityEventGroup== CommunityEventGroup  )
         .Create("(u)-[r:HasFinanceEvent]->(f)")
         .ExecuteWithoutResultsAsync();
      
      return Ok();
   }
   
   [HttpGet("UserSporEventRelationShip")]
   public async Task<IActionResult> UserSporEventRelationShip(int Id, string CommunityEventGroup)
   {
      await _client.Cypher.Match("(u:User), (s:Spor)")
         .Where((User u, Spor s) => u.Id == Id && u.CommunityEventGroup== CommunityEventGroup )
         .Create("(u)-[r:HasSporEvent]->(s)")
         .ExecuteWithoutResultsAsync();
      
      return Ok();
   }
   
   [HttpGet("UserTechEventRelationShip")]
   public async Task<IActionResult> UserTechEventRelationShip(int Id, string CommunityEventGroup)
   {
      await _client.Cypher.Match("(u:User), (t:Tech)")
         .Where((User u,  Tech t) => u.Id == Id && u.CommunityEventGroup== CommunityEventGroup)
         .Create("(u)-[r1:HasTechEvent]->(t)")
         .ExecuteWithoutResultsAsync();

      return Ok();
   }
   
   [HttpGet("GetAllUniversityUser")]
   public async Task<IActionResult> GetAllUniversityUser()
   {
      var User = await _client.Cypher.Match("(uni:University), (u:User)")
         .Where((User u, University uni)=> u.University==uni.Uni)
         .Return(u => u.As<User>()).ResultsAsync;

      return Ok(User);
   }
   
   [HttpGet("GetAllFinanceUser")]
   public async Task<IActionResult> GetAllFinanceUser()
   {
      var User = await _client.Cypher.Match("(f:Finance), (u:User)")
         .Where((User u, Finance f)=> u.CommunityEventGroup==f.FinanceEvent)
         .Return(u => u.As<User>()).ResultsAsync;

      return Ok(User);
   }
   
   [HttpGet("GetAllTechUser")]
   public async Task<IActionResult> GetAllTechUser()
   {
      var User = await _client.Cypher.Match("(t:Tech), (u:User)")
         .Where((User u, Tech t)=> u.CommunityEventGroup==t.TechEvent)
         .Return(u => u.As<User>()).ResultsAsync;

      return Ok(User);
   }
   
   [HttpGet("GetAllSporUser")]
   public async Task<IActionResult> GetAllSporUser()
   {
      var User = await _client.Cypher.Match("(s:Spor), (u:User)")
         .Where((User u, Spor s)=> u.CommunityEventGroup==s.SporEvent)
         .Return(u => u.As<User>()).ResultsAsync;

      return Ok(User);
   }
   
   [HttpGet("GetUniversityNameAndUser")]
   public async Task<IActionResult> GetUniversityNameAndUser(string idk)
   {
      var User = await _client.Cypher.Match("(u:User)")
         .Where((User u)=>u.University==idk)
         .Return(u => u.As<User>()).ResultsAsync;

      return Ok(User);
   }

   [HttpPut("UpdateUser")]
   public async Task<IActionResult> UpdateUser(int id, [FromBody]User usr)
   {
      await _client.Cypher.Match("(u:User)")
         .Where((User u) => u.Id == id)
         .Set("u = $usr")
         .WithParam("usr", usr)
         .ExecuteWithoutResultsAsync();

      return Ok();
   }

   [HttpDelete("DeleteUser")]
   public async Task<IActionResult> DeleteUser(int id, [FromBody] User usr)
   {
      await _client.Cypher.Match("(u:User)")
         .Where((User u) => u.Id == id)
         .DetachDelete("u")
         .ExecuteWithoutResultsAsync();

      return Ok();
   }
   
   
  
}
 