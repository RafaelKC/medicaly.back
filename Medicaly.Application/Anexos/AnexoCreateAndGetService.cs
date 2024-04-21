using Medicaly.Application.Anexos.Dtos;
using Medicaly.Application.Communs;
using Medicaly.Application.Transients;
using Medicaly.Domain.Anexos.Dtos;
using Medicaly.Infrastructure.Consts;
using Medicaly.Infrastructure.Supabse;

namespace Medicaly.Application.Anexos;

public interface IAnexoCreateAndGetService
{
    public Task<AnexoCreatedOutput?> Create(AnexoInput input);
    public Task<PagedResult<AnexoComLinkOutput>> GetList(PagedFilteredInput input);
    public Task<AnexoComLinkOutput?> Get(Guid id);
}

public class AnexoCreateAndGetService: IAnexoCreateAndGetService, IAutoTransient
{
    private readonly IAnexoService _anexoService;
    private readonly ISupabseClient _supabseClient;

    public AnexoCreateAndGetService(IAnexoService anexoService, ISupabseClient supabseClient)
    {
        _anexoService = anexoService;
        _supabseClient = supabseClient;
    }

    public async Task<AnexoCreatedOutput?> Create(AnexoInput input)
    {
        var output = await _anexoService.Create(input);
        if (output == null) return null;

        var url = await _supabseClient.AnexosStorage.CreateUploadSignedUrl(output.BucketEndereco);
        return new AnexoCreatedOutput
        {
            AnexoId = output.Id,
            UploadLink = url.SignedUrl
        };
    }

    public async Task<PagedResult<AnexoComLinkOutput>> GetList(PagedFilteredInput input)
    {
        var pagedResult = await _anexoService.GetList(input);
        var enderecos = pagedResult.Items.Select(a => a.BucketEndereco).ToList();

        if (enderecos.Count < 1) return new PagedResult<AnexoComLinkOutput>
        {
            Items = new List<AnexoComLinkOutput>()
        };

        var umaHoraEmSegundos = 60 * 60;


        var urls = new Dictionary<string, string>();

        var urlsResult = await _supabseClient.AnexosStorage.CreateSignedUrls(enderecos, umaHoraEmSegundos);
        if (urlsResult != null)
        {
            urls = urlsResult
                .Where(a => !string.IsNullOrWhiteSpace(a.Path) && string.IsNullOrWhiteSpace(a.SignedUrl))
                .ToDictionary(a => a.Path, a => a.SignedUrl);
        }

        return new PagedResult<AnexoComLinkOutput>
        {
            TotalCount = pagedResult.TotalCount,
            Items = pagedResult.Items.Select(anexo => new AnexoComLinkOutput
            {
                Id = anexo.Id,
                Nome = anexo.Nome,
                Extencao = anexo.Extencao,
                BucketPrefix = anexo.BucketPrefix,
                BucketEndereco = anexo.BucketEndereco,
                DataUltimaModificacao = anexo.DataUltimaModificacao,
                DownloadLink = urls.ContainsKey(anexo.BucketEndereco) ? urls[anexo.BucketEndereco] : ""
            }).ToList()
        };
    }

    public async Task<AnexoComLinkOutput?> Get(Guid id)
    {
        var output = await _anexoService.Get(id);
        if (output is null) return null;

        var umaHoraEmSegundos = 60 * 60;
        var url = await _supabseClient.AnexosStorage.CreateSignedUrl(output.BucketEndereco, umaHoraEmSegundos);

        return new AnexoComLinkOutput
        {
            Id = output.Id,
            Nome = output.Nome,
            Extencao = output.Extencao,
            BucketPrefix = output.BucketPrefix,
            BucketEndereco = output.BucketEndereco,
            DataUltimaModificacao = output.DataUltimaModificacao,
            DownloadLink = url
        };
    }
}