using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BulletPool
{
    private const int startBulletSpawn = 50;
    private List<BulletController> _bullets = new();
    private BulletFactory _bulletFactory;

    [Inject]
    private void Construct(BulletFactory bulletFactory)
    {
        _bulletFactory = bulletFactory;
    }
    
    public void RegisterBullet()
    {
        for (int i = 0; i < startBulletSpawn; i++)
        {
            var newBullet = _bulletFactory.Create();
            newBullet.IsActive = false;
            _bullets.Add(newBullet);
        }
    }

    public void SpawnBullet(int bulletAmount, int spreadAngle = 0)
    {
        var getInactiveBullets = _bullets.Where(x => !x.IsActive).ToList();

        if (getInactiveBullets.Count < bulletAmount)
        {
            RegisterBullet();
            SpawnBullet(bulletAmount);
        }

        for (int i = 0; i < bulletAmount; i++)
        {
            float angle = Random.Range(-spreadAngle, spreadAngle);
            Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            getInactiveBullets[i].Rotation = rotation;
            getInactiveBullets[i].IsActive = true;
        }
    }

    public void DespawnBullet(BulletController bullet)
    {
        bullet.IsActive = false;
    }

    public void DespawnAll()
    {
        foreach (var bullet in _bullets)
        {
            bullet.IsActive = false;
        }
    }
}
