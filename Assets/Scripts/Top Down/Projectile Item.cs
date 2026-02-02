using UnityEngine;

[CreateAssetMenu(fileName = "Projectile Item", menuName = "Scriptable Objects/Projectile Item")]
public class ProjectileItem : Item
{
    public override GameObject Summon(Vector2 position, Vector2 direction)
    {
        GameObject obj = base.Summon(position, direction);

        if (obj.TryGetComponent(out Projectile projectile))
        {
            projectile.Launch(direction);
        }

        return obj;
    }
}
