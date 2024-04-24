using Medicaly.Domain.Communs;

namespace Medicaly.Domain.Anexos.Dtos;

public class AnexoOutput: EntityDto
{
    public string BucketEndereco { get; set; }
    public DateTime DataUltimaModificacao { get; set; }
    public string Extencao { get; set; }
    public string Nome { get; set; }
    public Guid BucketPrefix { get; set; }

    public AnexoOutput()
    {

    }

    public AnexoOutput(Anexo input)
    {
        Id = input.Id;
        Nome = input.Nome;
        Extencao = input.Extencao;
        BucketPrefix = input.BucketPrefix;
        BucketEndereco = input.BucketEndereco;
        DataUltimaModificacao = input.DataUltimaModificacao;
    }
}