using DotNetMusicApi.Services;
using DotNetMusicApi.Services.Options;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<SpotifyOptions>();
builder.Services.AddHostedService<TimedTokenService>();
builder.Services.AddTransient<ISpotifyService, SpotifyService>();
builder.Services.AddTransient<IYouTubeService, YouTubeService>();
builder.Services.AddTransient<IConversionService, ConversionService>();

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