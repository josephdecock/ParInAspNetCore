var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = "cookie";
        opt.DefaultChallengeScheme = "oidc";
    })
    .AddOpenIdConnect("oidc", opt =>
    {   
        opt.Authority = "https://demo.duendesoftware.com";
        opt.ClientId = "interactive.confidential";
        opt.ClientSecret = "secret";
        opt.ResponseType = "code";
        opt.DisableTelemetry = true;
    })
    .AddCookie("cookie");
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Hello World!").RequireAuthorization();

app.Run();
