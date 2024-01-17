using Blazorise.Icons.Material;
using Blazorise.Material;
using Blazorise.Tests.bUnit;
using Bunit;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TestContext = Bunit.TestContext;

namespace Tradgardsgolf.Blazor.Wasm.Tests;

public class TestContextBuilder : IDisposable
{
    private readonly Bunit.TestContext _context;
    private readonly TestAuthorizationContext _authorizationContext;

    public TestContextBuilder()
    {
        _context = new TestContext();
        
        _context.JSInterop.Mode = JSRuntimeMode.Loose;
        _context.Services
            .AddBlazoriseTests()
            .AddMaterialProviders()
            .AddMaterialIcons();
        
        _authorizationContext = _context.AddTestAuthorization();
    }

    public TestContextBuilder UseAuthorization(Action<TestAuthorizationContext> auth)
    {
        auth.Invoke(_authorizationContext);
        return this;
    }
    
    public TestContextBuilder UseMock<T>(Action<Mock<T>> mockSetup) 
        where T : class
    {
        var mock = new Mock<T>();
        mockSetup.Invoke(mock);
        
        _context.Services.AddSingleton(mock.Object);

        return this;
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public TestContext Build() => _context;
}