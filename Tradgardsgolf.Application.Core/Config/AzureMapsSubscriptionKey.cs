using System.Diagnostics.CodeAnalysis;

namespace Tradgardsgolf.Core.Config;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public class AzureMapsSubscriptionKey
{
    public string Value { get; init; }
}