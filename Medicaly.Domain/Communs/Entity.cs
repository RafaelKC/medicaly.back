using System.ComponentModel.DataAnnotations;

namespace Medicaly.Domain.Communs;

public interface IEntity
{
    public Guid Id { get; set; }
}

public abstract class Entity: IEntity
{
    [Key]
    public Guid Id { get; set; }
}