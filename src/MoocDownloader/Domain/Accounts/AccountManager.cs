using MoocDownloader.Domain.Contracts;
using MoocDownloader.Models.Accounts;
using SQLite;

namespace MoocDownloader.Domain.Accounts;

public class AccountManager : DomainService<Account>
{
    public Account? GetAccountByWebSiteName(string websiteName)
    {
        using var connection = new SQLiteConnection(DatabasePath);
        return connection.Table<Account>().FirstOrDefault(account => account.WebsiteName == websiteName);
    }

    public void Insert(string websiteName, AccountModel account)
    {
        Insert(new Account
        {
            WebsiteName = websiteName,
            Type = account.Type,
            Status = account.Status,
            Username = account.Username,
            Password = account.Password,
            CookieData = account.CookieData,
        });
    }

    public void Update(string websiteName, AccountModel account)
    {
        var entity = GetAccountByWebSiteName(websiteName);

        if (entity is null)
        {
            return;
        }

        entity.Type = account.Type;
        entity.Status = account.Status;
        entity.Username = account.Username;
        entity.Password = account.Password;
        entity.CookieData = account.CookieData;

        Update(entity);
    }
}