﻿using System.Text.RegularExpressions;
using Medicaly.Domain.Anexos.Dtos;
using Medicaly.Domain.Communs;
using Medicaly.Domain.ResultadoAnexos;
using Medicaly.Domain.Resultados;

namespace Medicaly.Domain.Anexos;

public class Anexo: Entity
{
    public string BucketEndereco { get; set; }
    public DateTime DataUltimaModificacao { get; set; }
    public string Extencao { get; set; }
    public string Nome { get; set; }
    public Guid BucketPrefix { get; set; }
    
    public ICollection<ResultadoAnexo> ResultadosAnexos { get; set; }
    public ICollection<Resultado> Resultados { get; set; }

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

        var path = $"{BucketPrefix}_{Nome}.{Extencao}";

        BucketEndereco = path.Replace(" ", ""); ;
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

        var path = $"{BucketPrefix}_{Nome}.{Extencao}";

        BucketEndereco = Regex.Replace(path,  @"s", ""); ;
    }
}