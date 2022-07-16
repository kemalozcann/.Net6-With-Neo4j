using Neo4j.Driver;

namespace SocialMediaWithNeo4j.Models;

public class User:EventGroup
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Mail { get; set; }
    public string password { get; set; }
    public string University { get; set; }
    public string CommunityEventGroup { get; set; }
    public string Friends { get; set; }
     
}