
using Microsoft.AspNetCore.Builder;

namespace Shared.Domain
{
    public interface IEndpoint
    {
        string root { get; }
        void Register(WebApplication app);
    }
}