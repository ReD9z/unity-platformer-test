using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private GameObject currentHitObj;

    [SerializeField] private float circleRadius;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    private Enamy _enemy;
    private Vector2 origin;
    private Vector2 direction;

    private float currentHitDis;

    private void Start()
    {
        _enemy = GetComponent<Enamy>();
    }

    private void Update()
    {
        origin = transform.position;

        if (_enemy.isFacingRight)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.CircleCast(origin, circleRadius, direction, maxDistance, layerMask);

        if(hit)
        {
            currentHitObj = hit.transform.gameObject;
            currentHitDis = hit.distance;
            if(currentHitObj.CompareTag("Player"))
            {
                _enemy.StartChasingPlayer();
            }
        } else
        {
            currentHitObj = null;
            currentHitDis = maxDistance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(origin, origin + direction * currentHitDis);
        Gizmos.DrawWireSphere(origin + direction * currentHitDis, circleRadius);
    }
}
