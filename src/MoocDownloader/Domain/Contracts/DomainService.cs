using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace MoocDownloader.Domain.Contracts;

public abstract class DomainService<TEntity>
    where TEntity : Entity, new()
{
    protected static string DatabasePath => Path.Combine(Constants.UserDataPath, Constants.UserDataFile);

    protected DomainService()
    {
        CreateTable();
    }

    public TEntity Get(int id)
    {
        using var connection = new SQLiteConnection(DatabasePath);
        return connection.Table<TEntity>().First(entity => entity.Id == id);
    }

    public List<TEntity> GetList()
    {
        using var connection = new SQLiteConnection(DatabasePath);
        return connection.Table<TEntity>().ToList();
    }

    public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicateExpr)
    {
        using var connection = new SQLiteConnection(DatabasePath);
        return connection.Table<TEntity>().Where(predicateExpr).ToList();
    }

    public void Insert(TEntity entity)
    {
        using var connection = new SQLiteConnection(DatabasePath);
        connection.Insert(entity);
    }

    public void Update(TEntity entity)
    {
        using var connection = new SQLiteConnection(DatabasePath);
        connection.Update(entity);
    }

    public void CreateTable()
    {
        using var connection = new SQLiteConnection(DatabasePath);
        connection.CreateTable<TEntity>();
    }
}