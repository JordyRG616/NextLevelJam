using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : MonoBehaviour
{
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;

    public float distance;
    public Vector3 direction;

    private void OnDrawGizmos()
    {
        if (from == null || to == null) return;

        direction = (to.position - from.position).normalized;
        Gizmos.DrawRay(from.position, direction);
        Gizmos.DrawWireSphere(to.position, .1f);

        distance = (from.position - to.position).magnitude;
    }
}
