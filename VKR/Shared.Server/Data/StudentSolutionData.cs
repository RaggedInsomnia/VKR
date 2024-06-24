namespace Shared.Server.Data;

public class StudentSolutionData
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    
    public int StudentId { get; set; }
    public string SourceCode { get; set; }
    
    public int Variant { get; set; }
    
    public int Attempt { get; set; }
    
    public bool Success { get; set; }

    public string Result { get; set; }
}