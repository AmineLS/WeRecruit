namespace WeRecruit.Entities;

public class Admin
{
    public string Identifier { get; init; }
    public string Password { get; init; }
    public string Name { get; init; }

    // other props as needed

    private bool Equals(Admin other) => Identifier == other.Identifier && Password == other.Password;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Admin)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Identifier, Password);
}