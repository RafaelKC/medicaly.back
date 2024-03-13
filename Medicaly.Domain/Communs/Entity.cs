﻿namespace Medicaly.Domain.Communs;

public interface IEntity
{
    public Guid Id { get; set; }
}

public abstract class Entity: IEntity
{
    public Guid Id { get; set; }
}