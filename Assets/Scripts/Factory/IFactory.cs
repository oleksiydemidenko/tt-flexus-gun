using System;

namespace Factory
{
    public interface IFactory<T1, T2> 
        where T1 : UnityEngine.Object 
        where T2 : Enum
    {
        public abstract T1 Create(T2 type);
    }
}