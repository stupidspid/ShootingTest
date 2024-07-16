using System.Linq;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    [SerializeField] private float targetingRadius;
    [SerializeField] private LayerMask targetLayer;

    public Transform GetNearestTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, targetingRadius, targetLayer);
        if (targets.Length == 0) return null;

        return targets.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).First().transform;
    }
}