using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExplousivBarrale : MonoBehaviour, IDamagable
{

    public static event EventHandler OnAnyExplosion;


    [SerializeField]
    private float radius;
    [SerializeField]
    private float damage;
    [SerializeField]
    private GameObject particlePrefab;
    [SerializeField]
    private Collider2D collider2d;

    [SerializeField]
    private float explosivForce = 10f;

    public void Damage(float amount)
    {
        collider2d.enabled = false;


        RaycastHit2D[] raycastHit2DArray = Physics2D.CircleCastAll(transform.position, radius, Vector3.zero);


        foreach (var item in raycastHit2DArray)
        {
            GameObject go = item.collider.gameObject;

            if (go == this.gameObject) continue;

            if (go.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D))
            {
                Vector3 dist = go.transform.position - transform.position;
                rigidbody2D.AddForce(dist * explosivForce , ForceMode2D.Impulse);
            }

            if (go.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damagable.Damage(damage);
            }
        }

        KillAgent();
    }

    public void KillAgent()
    {
        GameObject particleGameObject = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(particleGameObject, 5f);

        OnAnyExplosion.Invoke(this, EventArgs.Empty);

        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
