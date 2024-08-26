using System;

namespace TesteCopilot.Domain.Riders;

public interface IRiderDomain
{
    public Task<bool> ValidateRiderAsync(int userId, int veihcleId);

}
