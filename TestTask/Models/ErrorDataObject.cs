namespace TestTask.Models;

public class ErrorDataObject
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
    
    public string ControllerName { get; set; }
    
    public  string ActionName { get; set; }
}