namespace Domain._Base.Models;

public abstract class Entidade
{
    protected Entidade() => Id = Guid.NewGuid().ToString();

    public string Id { get; set; }
}