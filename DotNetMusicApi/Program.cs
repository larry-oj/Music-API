using DotNetMusicApi.Services;
using DotNetMusicApi.Services.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<SpotifyOptions>();
builder.Services.AddHostedService<TimedTokenService>();
builder.Services.AddTransient<ISpotifyService, SpotifyService>();
builder.Services.AddTransient<IYouTubeService, YouTubeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();