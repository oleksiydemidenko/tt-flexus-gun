using Factory;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticlesList", menuName = "Particles/List", order = 0)]
public class ParticlesList : ScriptableObject 
{
    [SerializeField] private FactoryListElement<Particles, ParticlesType>[] _Elements;
    
    public FactoryListElement<Particles, ParticlesType> this[ParticlesType type]
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