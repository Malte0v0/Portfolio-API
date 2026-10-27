public class PortfolioData
{
  public Profile Profile { get; set; } = new();
  public List<AccessKey> AccessKeys { get; set; } = [];
}