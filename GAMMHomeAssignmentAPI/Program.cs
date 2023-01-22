using GAMMHomeAssignmentAPI.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
	build.WithOrigins("103.148.14.102").AllowAnyMethod().AllowAnyHeader();
}

));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters =
			new TokenValidationParameters
			{
				ValidateIssuer= false,
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
				.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value))
			};
	});
builder.Services.AddAutoMapper(typeof(RegisterUserMapper).Assembly);

var app = builder.Build();
app.UseAuthorization();
app.UseAuthorization();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("corspolicy");
app.UseHttpsRedirection();



app.MapControllers();

app.Run();
