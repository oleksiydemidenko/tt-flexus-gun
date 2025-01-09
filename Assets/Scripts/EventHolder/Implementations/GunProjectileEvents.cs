using UnityEngine;

public static class GunProjectileEvents
{
    public static EventHolder<GunProjectile, RaycastHit> Hit = new ();
}
