using MediatR;

namespace Tradgardsgolf.Contracts.Settings;

public record AllowPlayDistanceCommand : IRequest<SettingResponse<int>> { }