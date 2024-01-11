namespace Logic.TakeDamage
{
    public interface ITakeDamage
    {
        bool Happened { get; }
        bool TakeDamage(float value);
    }
}