using Emails.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Emails.Worker.Worker>();

var host = builder.Build();
host.Run();
