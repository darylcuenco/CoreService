namespace core.Models;

public class ResponseModelArray
{
    public string? message { get; set; }
    public List<DBManagerObject>? result { get; set; }
    public bool? isSuccess { get; set; }

}

public class ResponseModel
{
    public string? message { get; set; }
    public DBManagerObject? result { get; set; }
    public bool? isSuccess { get; set; }

}
