using DotNetMusicApi.Services;
using DotNetMusicApi.Services.Options;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<SpotifyOptions>();
builder.Services.AddHostedService<TimedTokenService>();
builder.Services.AddScoped<ISpotifyService, SpotifyService>();
builder.Services.AddScoped<IYouTubeService, YouTubeService>();
builder.Services.AddScoped<IConversionService, ConversionService>();
builder.Services.AddScoped<IRecognitionService, AuddService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();