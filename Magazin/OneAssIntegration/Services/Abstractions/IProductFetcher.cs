using System.Threading.Tasks;

namespace OneAssIntegration.Services.Abstractions
{
    public interface IProductFetcher
    {
        Task LoadProducts();
    }
}