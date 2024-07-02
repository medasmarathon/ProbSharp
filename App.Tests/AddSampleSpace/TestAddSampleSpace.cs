using App.Entities;
using App.Operations.AddSampleSpace;
using App.Operations.Interfaces;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProbSharp.Persistence;

namespace App.Tests.AddSampleSpace;

public class TestAddSampleSpace : BaseAppTest
{
    public async override Task InitializeAsync()
    {
        await base.InitializeAsync();
        serviceCollection.RegisterProbSharpApp();
        services = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task AddSampleSpace_Should_BeSuccessful()
    {
        using var scope = services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProbSharpContext>();

        var ssRequest = scope.ServiceProvider.GetRequiredService<IRequestHandler<AddSampleSpaceRequest, SampleSpace>>();

        var ss = await ssRequest.Handle(new AddSampleSpaceRequest { Name = "Sample Standalone SS" });

        var insertedSs = await dbContext.Nodes.Where(n => n.Id == ss.Id).FirstOrDefaultAsync();
        insertedSs.Should().NotBeNull();
    }
}
