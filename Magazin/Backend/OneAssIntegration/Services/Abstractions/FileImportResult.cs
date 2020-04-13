namespace OneAssIntegration.Services.Abstractions
{
    public class FileImportResult
    {
        public int Total { get; set; }

        public int Success { get; set; }

        public int Failed { get; set; }
    }
}