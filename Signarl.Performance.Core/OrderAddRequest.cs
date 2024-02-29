namespace Signarl.Performance.Core;

public record struct OrderAddRequest(string UserId);

public record struct OrderAddByIdRequest(string ClientId, string UserId);

public record struct OrderAddResponse(bool Successful, string? ErrorMessage = default);