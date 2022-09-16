using System.Collections.Generic;
using UnityEngine;

namespace SonaruUtilities
{
    [System.Serializable]
    public class ObjectPool
    {
        private Queue<IPoolAble> pool;
        
        [Header("Object Pool Params")]
        public GameObject PoolObject;
        public Transform Parent;
        public int OriginPoolAmount;
        public bool Expandable;
        public int Length => pool.Count;
        public bool IsEmpty => pool.Count <= 0;

        public ObjectPool(GameObject poolObj, Transform parent, int amount = 1, bool expandable = true)
        {
            PoolObject = poolObj;
            Parent = parent;
            OriginPoolAmount = amount;
            Expandable = expandable;

            pool = new Queue<IPoolAble>();
            for (var i = 0; i < OriginPoolAmount; i++) InitPoolObj();
        }


        private void InitPoolObj()
        {
            var obj = Object.Instantiate(PoolObject, Parent);
            var poolAble = obj.GetComponent<IPoolAble>();
            if (poolAble == null)
            {
                Debug.LogError("Can't find \"IPoolAble\" Component in the target pool object !");
                return;
            }
            poolAble.Pool = this;
            poolAble.Obj = obj;
            poolAble.InitSelf();
            pool.Enqueue(poolAble);
            obj.SetActive(false);
        }
        
        
        public IPoolAble GetObject()
        {
            if (IsEmpty)
            {
                if (Expandable) InitPoolObj();
                else return null;
            }

            var poolAble = pool.Dequeue();
            poolAble.Obj.SetActive(true);
            poolAble.SpawnSelf();
            return poolAble;
        }


        public void Recycle(IPoolAble poolAble)
        {
            poolAble.ResetSelf();
            pool.Enqueue(poolAble);
            poolAble.Obj.SetActive(false);
        }

        
    }
}