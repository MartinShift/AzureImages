using Microsoft.Azure.WebJobs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace AzureImages;

public class ImageResizer
{
    public static void ResizeImage(
        [BlobTrigger("images/{name}", Connection = "BlobStorageConnection")] Stream inputBlob,
        string name,
        [Blob("images-min/{name}", FileAccess.Write, Connection = "BlobStorageConnection")] Stream outputBlob,
        ILogger log)
    {
        try
        {
            using (Image image = Image.Load(inputBlob))
            {
                image.Mutate(x => x.Resize(200, 100));
                image.SaveAsJpeg(outputBlob);
            }

            log.LogInformation($"Processed blob\n Name:{name} \n Size: {outputBlob.Length} bytes");
        }
        catch (Exception ex)
        {
            log.LogError($"Error processing the image {name}. Exception: {ex.Message}");
        }
    }
}