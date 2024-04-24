using Medicaly.Domain.Anexos;
using Medicaly.Domain.Anexos.Dtos;

namespace Medicaly.Application.Anexos.Dtos;

public class AnexoComLinkOutput: AnexoOutput
{
   public string DownloadLink { get; set; }
}