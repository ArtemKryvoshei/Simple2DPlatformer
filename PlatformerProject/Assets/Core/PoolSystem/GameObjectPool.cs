using System;
using System.Collections.Generic;
using System.Threading;
using Core.PrefabFactory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.PoolSystem
{
    public class GameObjectPool : IGameObjectPool
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly string _address;
        private readonly Transform _parent;
        private readonly Stack<GameObject> _pool = new();
        
        private readonly HashSet<GameObject> _rentObjects = new();


        public GameObjectPool(IPrefabFactory prefabFactory, string address, Transform parent = null)
        {
            _prefabFactory = prefabFactory;
            _address = address;
            _parent = parent;
        }

        public async UniTask WarmUpAsync(int count, CancellationToken token = default)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = await _prefabFactory.CreateAsync(_address, _parent, token);
                obj.SetActive(false);
                _pool.Push(obj);
            }
        }

        public GameObject Rent()
        {
            GameObject obj = _pool.Count > 0 ? _pool.Pop() : null;

            if (obj != null)
            {
                obj.SetActive(true);
                _rentObjects.Add(obj);
                return obj;
            }

            throw new Exception($"[GameObjectPool] No objects in pool for address {_address}. Consider prewarming more.");
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_parent);
            _rentObjects.Remove(obj);
            _pool.Push(obj);
        }

        public bool Contains(GameObject obj, bool includeRented)
        {
            if (includeRented)
            {
                return _rentObjects.Contains(obj) || _pool.Contains(obj);
            }
            else
            {
                _pool.Contains(obj);
            }

            return false;
        }
    }
}