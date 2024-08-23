using FluentValidation.Results;

namespace Experimento.Domain.Notification;

public class NotificationContext
{
    public readonly List<Notification> _notifications;
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    public virtual bool HasNotifications()
    {
        return _notifications.Any();
    }
    
    public NotificationContext()
    {
        _notifications = new List<Notification>();
    }

    public virtual void AddNotification(string message)
    {
        _notifications.Add(new Notification(message));
    }

    public void AddNotification(Notification notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IEnumerable<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(IReadOnlyCollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(IList<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(ICollection<Notification> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public void AddNotifications(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            AddNotification(error.ErrorMessage);
        }
    }
    
}