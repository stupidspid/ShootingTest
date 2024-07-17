using UnityEngine;

public class Signals {}

public class ShootSignal
{

}

public class ShootToEnemySignal
{
    public Transform Target { get; private set; }

    public ShootToEnemySignal(Transform target)
    {
        Target = target;
    }
}
