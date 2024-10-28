using Messenger.Config;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddDbContext();
builder.Services.AddServices();
builder.Services.AddIdentity();
builder.Services.AddAuthentication(builder);
builder.Services.AddMailing(builder);
builder.Services.AddRedis(builder);
builder.Services.AddAuthorization();
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Storage"));
var requestPath = "/Files";




app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthentication();



app.Run();

