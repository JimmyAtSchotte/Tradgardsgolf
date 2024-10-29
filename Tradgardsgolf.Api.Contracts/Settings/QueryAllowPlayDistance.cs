using MediatR;

namespace Tradgardsgolf.Contracts.Settings;

public record QueryAllowPlayDistance : IRequest<SettingResponse<int>> { }