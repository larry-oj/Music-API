namespace DotNetMusicApi.Services.Models.Converter;

public class ConverterFile
{
    public Stream FileStream { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }

    public ConverterFile(Stream fileStream, string contentType, string fileName)
    {
        FileStream = fileStream;
        ContentType = contentType;
        FileName = fileName;
    }
}