using Ocelot.DependencyInjection;
using Ocelot.Middleware;
/*using Micro.Services.IdentityAPI.Extensions;*/

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Ocelot.Json",optional:false,reloadOnChange:true);
builder.Services.AddOcelot(builder.Configuration);
/*builder.Services.AddAppAuthentication();*/
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();


app.Run();