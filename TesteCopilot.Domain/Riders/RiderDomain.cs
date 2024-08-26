using TesteCopilot.Repository.AppContext;

namespace TesteCopilot.Domain.Riders;

public class RiderDomain : IRiderDomain
{
    private readonly AppDatabaseContext _context;

    public RiderDomain(AppDatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> ValidateRiderAsync(int userId, int veihcleId)
    {
        var user = await _context.Users.FindAsync(userId);
        var vehicle = await _context.Vehicles.FindAsync(veihcleId);

        if (user == null || vehicle == null)
        {
            return false;
        }

        if (string.IsNullOrEmpty(user.Name) ||
            string.IsNullOrEmpty(user.Email) ||
            string.IsNullOrEmpty(vehicle.Plate) ||
            vehicle.Capacity == 0 ||
            vehicle.Id == 0
            )

        {
            return false;
        }

        return true;
    }
}
