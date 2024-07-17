using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private BulletController bullet;
    [SerializeField] private EnemyController enemy;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<BulletController>().AsSingle();
        Container.BindFactory<BulletController, BulletFactory>().FromComponentInNewPrefab(bullet);
        Container.Bind<PlayerController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<SpawnAroundService>().AsSingle();
        Container.BindInterfacesTo<EnemyController>().AsSingle();
        Container.BindFactory<EnemyController, EnemyFactory>().FromComponentInNewPrefab(enemy);
        Container.Bind<PoolObjects<BulletController,BulletFactory>>().AsSingle();
        Container.Bind<PoolObjects<EnemyController,EnemyFactory>>().AsSingle();
        Container.Bind<GunController>().FromComponentInHierarchy().AsSingle();
    }
}