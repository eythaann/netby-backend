using System.IdentityModel.Tokens.Jwt;
using Auth.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Application;
using Env;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Auth
{
    public class AuthEndpoint: BaseEndpoint
    {
        private UserRepository repository = new HardCodedUserRepository();

        public IResult NewUser([FromBody] RegisterPayload payload)
        {
            var isValid = MiniValidation.MiniValidator.TryValidate(payload, out var errors);
            if (!isValid)
            {
                return Results.ValidationProblem(errors);
            }

            repository.save(new User {
                Id = Guid.NewGuid().ToString("N"),
                Name = payload.Name ?? "",
                Email = payload.Email ?? "",
                Password = payload.Password ?? "",
            });
            return Results.Ok();
        }


        public IResult Login([FromBody] LoginPayload payload)
        {
            var isValid = MiniValidation.MiniValidator.TryValidate(payload, out var errors);
            if (!isValid)
            { 
                return Results.ValidationProblem(errors);
            }

            var searched = repository.search(payload.Email);
            if (searched?.Password != payload.Password)
            {
                return Results.Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.Config.SECRET_KEY));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, searched?.Name ?? ""),
                    new Claim(ClaimTypes.Email, searched?.Email ?? ""),
                    new Claim(ClaimTypes.NameIdentifier, searched?.Id ?? "")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials,
            };

            var token = tokenHandler.CreateToken(tokenDes);
            return Results.Text(tokenHandler.WriteToken(token));
        }

        public override void Register(WebApplication app)
        {
            app.MapPost($"{root}/register", NewUser);
            app.MapPost($"{root}/login", Login);
        }

        public AuthEndpoint() : base(null) { }
        public AuthEndpoint(string path) : base(path) { }
    }
}