using SQLite;

namespace MoocDownloader.Domain.Contracts;

public abstract class Entity
{
    [PrimaryKey, AutoIncrement]
    [Column(nameof(Id))]
    public int Id { get; set; }

    protected Entity()
    {
    }

    protected Entity(int id)
    {
        Id = id;
    }
}