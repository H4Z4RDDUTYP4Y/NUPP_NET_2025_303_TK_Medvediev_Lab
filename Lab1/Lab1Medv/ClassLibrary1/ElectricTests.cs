using Guitar.Common.Crud;
using Guitar.Common.GuitarTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class ElectricTests
{
    private ICrudServiceAsync<Electric> GetService() => new CrudServiceAsync<Electric>();

    [Fact]
    public async Task CreateAndRead_ShouldReturnSameElectric()
    {
        var service = GetService();
        var electric = Electric.CreateNew();

        await service.CreateAsync(electric);
        var result = await service.ReadAsync(electric.Id);

        Assert.NotNull(result);
        Assert.Equal(electric.Name, result?.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldChangeValues()
    {
        var service = GetService();
        var electric = Electric.CreateNew();
        await service.CreateAsync(electric);

        electric.PickupCount = 10;
        await service.UpdateAsync(electric);

        var updated = await service.ReadAsync(electric.Id);
        Assert.Equal(10, updated?.PickupCount);
    }

    [Fact]
    public async Task RemoveAsync_ShouldDeleteObject()
    {
        var service = GetService();
        var electric = Electric.CreateNew();

        await service.CreateAsync(electric);
        await service.RemoveAsync(electric);

        var result = await service.ReadAsync(electric.Id);
        Assert.Null(result);
    }

    [Fact]
    public async Task SaveAndLoadAsync_ShouldPreserveData()
    {
        var file = "electrics_test.json";
        var service1 = GetService();
        var electric = Electric.CreateNew();

        await service1.CreateAsync(electric);
        await service1.SaveAsync(file);

        var service2 = GetService();
        await service2.LoadAsync(file);
        var loaded = await service2.ReadAsync(electric.Id);

        Assert.NotNull(loaded);
        Assert.Equal(electric.Name, loaded?.Name);

        File.Delete(file); // cleanup
    }

    [Fact]
    public async Task ReadAllAsync_ShouldReturnAll()
    {
        var service = GetService();
        var e1 = Electric.CreateNew();
        var e2 = Electric.CreateNew();

        await service.CreateAsync(e1);
        await service.CreateAsync(e2);

        var all = await service.ReadAllAsync();
        Assert.Equal(2, all.Count());
    }
}