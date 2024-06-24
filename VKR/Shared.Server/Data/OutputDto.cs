namespace Shared.Server.Data;
    public class OutputDto
    {
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
        public bool Success => string.IsNullOrEmpty(StandardError);
    }
