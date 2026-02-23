public interface IDamageable
{
    int Health {get; set;}
    bool IsDead {get; set;}
    void TakeDamage (int amount);
}