using Medicaly.Domain.Anexos.Dtos;
using Medicaly.Domain.Communs;

namespace Medicaly.Domain.Anexos;

public class Anexo: Entity
{
    public string BucketEndereco { get; set; }
    public DateTime DataUltimaModificacao { get; set; }
    public string Extencao { get; set; }
    public string Nome { get; set; }
    public Guid BucketPrefix { get; set; }

    public Anexo()
    {
    }

    public Anexo(AnexoInput input)
    {
        Id = input.Id != Guid.Empty ? input.Id : Guid.NewGuid();
        DataUltimaModificacao = DateTime.Now;
        BucketPrefix = Guid.NewGuid();

        var nameSplited = input.ArquivoNome.Split(".");
        if (nameSplited.Length < 2)
        {
            throw new ArgumentException("Arquivo nome deve ter uma extenção.");
        }

        Nome = string.Join(".", nameSplited.Take(nameSplited.Count() - 1));
        Extencao = nameSplited.Last();

        BucketEndereco = $"{BucketPrefix}_{Nome}.{Extencao}";
    }

    public void Update(AnexoInput input)
    {
        DataUltimaModificacao = DateTime.Now;

        var nameSplited = input.ArquivoNome.Split(".");
        if (nameSplited.Length < 2)
        {
            throw new ArgumentException("Arquivo nome deve ter uma extenção.");
        }

        Nome = string.Join(".", nameSplited.Take(nameSplited.Count() - 1));
        Extencao = nameSplited.Last();

        BucketEndereco = $"{BucketPrefix}_{Nome}.{Extencao}";
    }
}