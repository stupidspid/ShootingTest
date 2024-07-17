using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PoolObjects<TObject, TFactory> where TObject : MonoBehaviour where TFactory : PlaceholderFactory<TObject>
{
    private const int startSpawn = 50;
    private List<TObject> _list = new();
    private TFactory _factory;

    public void Register(TFactory factory, Transform parent = null)
    {
        _factory = factory;
        for (int i = 0; i < startSpawn; i++)
        {
            var newInstance = factory.Create();
            newInstance.gameObject.SetActive(false);
            newInstance.transform.SetParent(parent);
            _list.Add(newInstance);
        }
    }

    public TObject Spawn()
    {
        var getInactiveObjects = _list.Where(x => !x.gameObject.activeSelf).ToList();

        if (getInactiveObjects.Count < 1)
        {
            Register(_factory);
            Spawn();
        }

        getInactiveObjects[0].gameObject.SetActive(true);
        return getInactiveObjects[0];
    }

    public void Despawn(TObject currentObjec)
    {
        currentObjec.gameObject.SetActive(false);
    }

    public void DespawnAll()
    {
        foreach (var l in _list)
        {
            l.gameObject.SetActive(false);
        }
    }
}
