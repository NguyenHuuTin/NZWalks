# NZWalks
install packages:
  1. AutoMapper
  2. AutoMapper.Extensions.Microsoft.DependencyInjection
  3. FluentValidation
  4. FluentValidation.AspNetCore
  5. FluentValidation.DependencyInjectionExtensions
  6. Microsoft.EntityFrameworkCore.SqlServer
  7. Microsoft.EntityFrameworkCore.Tools
  8. Microsoft.AspNetCore.Authentication.JwtBearer
  9. Microsoft.IdentityModel.Tokens
  10. System.IdentityModel.Tokens.Jwt
* setup to connet with conectionString
    builder.Services.AddDbContext<NZWalksDbContext>(options => 
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks"));
    });
* AutoMapper: var regionDTO = mapper.Map<Models.DTO.Region>(region);
* FluentValidation: 
  public class AddRegionRequestValidator : AbstractValidator<Models.DTO.AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            RuleFor(x => x.code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
 * JWT:
    - Install package for JWT
    - add key, Issuer, Audience in appSeting
        "Jwt": {
          "Key": "any string",
          "Issuer": "https://localhost: xxxx/",
          "Audience": "https://localhost:xxxx/"
        }
        
        To get the address you can follow these steps:
        + right click in project choose property
        + choose Debug
        + click on link Open debug lauch profile UI
        + choose a addres in input app URL
    - Create IUserRepon and UserRepon to Check login with username and password
    - Create ITokenHandle and TokenHandle to generate token
        public Task<string> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            user.Roles.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"], 
                configuration["Jwt:Audience"], 
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    setup in program file 
  builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }); ;
  
  app.UseAuthentication();
  app.UseAuthorization();
  
  To can use login with JWT in Swagger, you can setup:
  builder.Services.AddSwaggerGen(options =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter a valid JWT bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[]{} }
    });
});


  
