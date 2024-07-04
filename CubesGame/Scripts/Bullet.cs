using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{

    public static event EventHandler OnAnyHit;


    enum BulletType
    {
        linear,
        balistic,
    }

    [SerializeField]
    private float radius;

    [SerializeField]
    private float bulletLifeTime = 2f;

    [SerializeField]
    private GameObject impactParicles;

    private float bulletDamage;
    private float bulletSpeed = 10f;
    private BulletType bulletType = BulletType.linear;
    private Vector3 bulletDir = Vector3.zero;
    private Transform sender;



    private void Start()
    {
        Destroy(this.gameObject, bulletLifeTime);
    }


    private void Update()
    {

        switch (bulletType)
        {
            case BulletType.linear:

                transform.position += bulletSpeed * Time.deltaTime * bulletDir;

                break;

        }

        CollisionHandler();
    }


    public void DesrtroyBullet()
    {

        Destroy(this.gameObject);
    }


    private void CollisionHandler()
    {
        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, radius, Vector3.zero);

        if (raycastHit2D.collider != null)
        {
            GameObject colliderOther = raycastHit2D.collider.gameObject;

            if (colliderOther == sender) return;

            if (colliderOther.TryGetComponent<IDamagable>(out IDamagable iDamagable))
            {
                iDamagable.Damage(bulletDamage);
            }

            
            GameObject particle = Instantiate(impactParicles, transform.position, Quaternion.identity);
            Destroy(particle, 10);

            OnAnyHit.Invoke(this, EventArgs.Empty);

            DesrtroyBullet();
        }
    }

    public void SetBulletData(Transform agent, float speed, float damage, Vector3 direction)
    {
        sender = agent;
        bulletSpeed = speed;
        bulletDamage = damage;
        bulletDir = direction;
    }
}
