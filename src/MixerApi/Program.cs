using System.IO.Enumeration;
using Akka.DependencyInjection;
using Akka.Hosting;
using Google.Protobuf;
using Schedule1.Mixer.Api.Mixing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var services = builder.Services;
services
    .AddOpenApi()
    .AddAkka("schedule1", options =>
    {
        options.WithActors((system, registry) =>
        {
            var resolver = DependencyResolver.For(system);
            registry.Register<Mixer>(system.ActorOf(resolver.Props<Mixer>(), "mixer"));
        });
    })
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();