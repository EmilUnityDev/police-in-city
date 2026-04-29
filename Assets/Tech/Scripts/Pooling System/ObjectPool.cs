using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    public List<T> Pool
    {
        get => _pool;
    }

    private List<T> _pool;

    private T _original;

    private Transform _parent;

    public ObjectPool(T original, Transform parent, int size)
    {
        _pool = new List<T>();
        _original = original;
        _parent = parent;

        GrowPool(size);
    }

    public T GetFromPool()
    {
        foreach (var item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                return item;
            }
        }

        GrowPool(1);
        return GetFromPool();
    }

    public int GetActiveCount()
    {
        int N = 0;
        foreach (var item in _pool)
        {
            if (item.gameObject.activeInHierarchy)
            {
                N++;
            }
        }

        return N;
    }

    public List<T> GetActive()
    {
        List<T> active = new List<T>();

        foreach (var item in _pool)
        {
            if (item.gameObject.activeInHierarchy)
            {
                active.Add(item);
            }
        }

        return active;
    }

    public void DestroyAll()
    {
        foreach (var item in _pool)
        {
            if (item == null) continue;

            Object.Destroy(item.gameObject);
        }
    }

    private void GrowPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            T newObject = Object.Instantiate(_original, _parent);

            newObject.gameObject.SetActive(false);

            _pool.Add(newObject);
        }
    }
}