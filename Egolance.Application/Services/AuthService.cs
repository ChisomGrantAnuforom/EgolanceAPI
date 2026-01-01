

    using Egolance.Application.DTOs.Auth;
    using Egolance.Domain.Entities;
    using Egolance.Infrastructure.Database;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    namespace Egolance.Application.Services
    {
        public class AuthService
        {
            private readonly EgolanceDbContext _db;
            private readonly IConfiguration _config;
            private readonly PasswordHasher<User> _passwordHasher;

            public AuthService(EgolanceDbContext db, IConfiguration config)
            {
                _db = db;
                _config = config;
                _passwordHasher = new PasswordHasher<User>();
            }

            public async Task<string> RegisterAsync(RegisterRequest request)
            {
                // Check if email already exists
                if (await _db.Users.AnyAsync(x => x.Email == request.Email))
                    throw new Exception("Email already exists");

                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    FullName = request.FullName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Role = request.Role,
                    CreatedAt = DateTime.UtcNow,
                    IsVerified = false
                };

                // Hash password
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                // Return JWT token
                return GenerateJwtToken(user);
            }

            public async Task<string> LoginAsync(LoginRequest request)
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                if (user == null)
                    throw new Exception("Invalid credentials");

                var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
                if (result == PasswordVerificationResult.Failed)
                    throw new Exception("Invalid credentials");

                return GenerateJwtToken(user);
            }

            private string GenerateJwtToken(User user)
            {
                var jwtSettings = _config.GetSection("Jwt");

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpiresInMinutes"]!)),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

