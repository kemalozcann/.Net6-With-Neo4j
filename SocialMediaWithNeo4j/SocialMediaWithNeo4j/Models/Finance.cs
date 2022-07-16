namespace SocialMediaWithNeo4j.Models;

public class Finance:EventGroup
{
    public int Fid { get; set; }
    public string FinanceEvent { get; set; }
    public string FinanceMessage { get; set; }

    
}