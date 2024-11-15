using MediatR;

namespace Tradgardsgolf.Contracts.Settings;

public class QueryAzureMapsSubscriptionKey : IRequest<AzureMapsSubscriptionKeyResponse>;