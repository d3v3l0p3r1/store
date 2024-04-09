using IdentityServiceBase;
using IdentityServiceBase.Entities;
using Microsoft.EntityFrameworkCore;


public static class Program
{
    public static async Task Main(params string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var cs = builder.Configuration.GetConnectionString("DataContext");

        builder.Services.AddLogging();
        builder.Services.AddNpgsql<DataContext>(cs);

        builder.Services.AddIdentity<User, Role>(cfg =>
        {

        });

        var app = builder.Build();

        await app.RunAsync();
    }
}

