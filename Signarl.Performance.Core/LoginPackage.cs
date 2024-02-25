namespace Signarl.Performance.Core;

public record struct AddOrderRequest(string ClientId);

public record struct AddOrderResponse(bool SuccessFul = default, byte ErrorCode = default,
    string? ErrorMessage = default);