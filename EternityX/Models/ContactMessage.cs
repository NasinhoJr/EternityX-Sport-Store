using EternityX.Models;

public class ContactMessage
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public string Subject { get; set; }
    public string Message { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}