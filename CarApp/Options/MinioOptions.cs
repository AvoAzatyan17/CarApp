namespace CarApp.Options;

public class MinioOptions
{
    public string DefaultBucket { get; set; }
    public string Endpoint { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
}