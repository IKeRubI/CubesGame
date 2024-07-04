using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RifleSO : ScriptableObject
{
    public string rifleName;
    public GameObject rifleVisual;

    public GameObject bullet;

    
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletScatter;
    public float bulletAmount;

    public float timeBetwenShots;
    public bool autoFire = false;
}
