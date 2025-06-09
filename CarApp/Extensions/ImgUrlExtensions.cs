namespace CarApp.Extensions;

public static class ImgUrlExtensions
{
    public static string GetStringUrl(this string objectName)
    {
        var objectUrl = Uri.EscapeDataString(objectName);
        return $"http://localhost:9001/api/v1/buckets/car-images/objects/" +
               $"download?preview=true&prefix={objectUrl}&version_id=null";
    }
}