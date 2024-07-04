using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rifle : MonoBehaviour 
{

    public static event EventHandler OnAnyShoot;



    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform bullet;

    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletDamage;
    [SerializeField]
    private float bulletScatter;
    [SerializeField]
    private float bulletAmount;


    [SerializeField]
    private float timeBetwenShots;
    private float lastShot = 0;

    private Vector3 bulletDirection;

    private Transform rifleHandler;

    public void Start()
    {
        rifleHandler = GetComponentInParent<Transform>();
    }



    public void Shoot()
    {
        if (Time.time - lastShot < timeBetwenShots) return;

        lastShot = Time.time;

        bulletDirection = firePoint.position - rifleHandler.position;

        bulletDirection.Normalize();

        for (int i = 0; i < bulletAmount; i++)
        {

            bulletDirection.y += UnityEngine.Random.Range(-bulletScatter, bulletScatter);
            bulletDirection.x += UnityEngine.Random.Range(-bulletScatter, bulletScatter);


            CreateBullet(bulletDirection);
        }

        OnAnyShoot?.Invoke(this, EventArgs.Empty);
    }



    private void CreateBullet(Vector3 dir)
    {
        Transform gameObject = Instantiate(bullet, firePoint.position, Quaternion.identity);

        Bullet bul = gameObject.GetComponent<Bullet>();

        bul.SetBulletData(rifleHandler, bulletSpeed, bulletDamage, dir);
    }
}
