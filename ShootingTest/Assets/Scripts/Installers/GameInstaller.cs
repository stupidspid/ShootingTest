using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private BulletController bullet;
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<BulletController>().AsSingle();
        Container.BindFactory<BulletController, BulletFactory>().FromComponentInNewPrefab(bullet);
    }
}