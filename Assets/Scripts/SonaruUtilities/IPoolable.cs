using UnityEngine;

namespace SonaruUtilities
{
    public interface IPoolAble
    {
        ObjectPool Pool { get; set; }
        GameObject Obj { get; set; }
        
        void InitSelf();
        void SpawnSelf();
        void ResetSelf();
    }
}