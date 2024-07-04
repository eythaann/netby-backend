using Shared.Domain;
using Microsoft.AspNetCore.Builder;

namespace Shared.Application
{
    public abstract class BaseEndpoint: IEndpoint
    {
        public string root { get; }

        public abstract void Register(WebApplication app);

        protected BaseEndpoint(string? path)
        {
            this.root = path ?? "";
        }
    }
}