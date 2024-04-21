namespace Medicaly.Application.Anexos.Dtos;

public class AnexoCreatedOutput
{
    public Uri UploadLink { get; set; }
    public string Token { get; set; }
    public string Key { get; set; }
    public Guid AnexoId { get; set; }
}