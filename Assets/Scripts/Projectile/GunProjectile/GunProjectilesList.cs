using Factory;
using UnityEngine;

[CreateAssetMenu(fileName = "GunProjectilesList", menuName = "Projectiles/Gun/List", order = 0)]
public class GunProjectilesList : ScriptableObject 
{
    [SerializeField] private FactoryListElement<GunProjectile, GunProjectileType>[] _Elements;
    
    public FactoryListElement<GunProjectile, GunProjectileType> this[GunProjectileType type]
    {
        get 
        {
            foreach (var baseListElement in _Elements)
                if (baseListElement.Type.Equals(type))
                    return baseListElement;
            return null;
        }
    }
}