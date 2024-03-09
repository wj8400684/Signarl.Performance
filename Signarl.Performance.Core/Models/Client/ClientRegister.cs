namespace Signarl.Performance.Core.Models;

public sealed record ClientRegisterRequest(string DeviceId, string Name, string CpuId);

public sealed record ClientRegisterResultModel(string Token);