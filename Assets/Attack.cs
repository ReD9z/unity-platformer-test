using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _isAttack;

    private Charapter _palyer;

    private void Start()
    {
        _palyer = GetComponent<Charapter>();
    }

    void Update()
    {
      
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Enamy enemy = collision.collider.GetComponent<Enamy>();
        if (enemy != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.6f)
                {
                    Debug.Log("1");
                }
                else
                {
                    Debug.Log("2");
                }
            }
        }
    }
}
