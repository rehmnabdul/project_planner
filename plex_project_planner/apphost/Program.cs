using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Add services to the container.
builder.AddProject<Projects.PlexProjectPlanner_Web>("webapp");

// Add your resources below
// builder.AddSqlServer("dbname");

var app = builder.Build();

await app.RunAsync();