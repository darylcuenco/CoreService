namespace core.Models;
public class DBManagerObject
{
    public string? id { get; set; }
    public Data? data { get; set; }

}

public class Posts
{
    public int? id { get; set; }
    public string? post { get; set; }
}

public class Data
{
    public string? username { get; set; }
    public string? email { get; set; }
    public List<Posts>? posts { get; set; }
}
