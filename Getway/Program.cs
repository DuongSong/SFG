﻿using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddOcelot(configuration);
var app = builder.Build();

app.UseOcelot().Wait();

app.Run();

