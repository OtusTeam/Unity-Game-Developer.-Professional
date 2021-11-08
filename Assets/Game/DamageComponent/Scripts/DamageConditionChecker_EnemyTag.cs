namespace Otus
{
    public sealed class DamageConditionChecker_EnemyTag : DamageConditionChecker
    {
        public override bool CanTakeDamage(DamageComponent damageComponent)
        {
            return damageComponent.gameObject.CompareTag("Enemy");
        }
    }
}