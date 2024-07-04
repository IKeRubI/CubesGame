using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Rifle rifle;
    [SerializeField]
    private float timerMax = 2f;
    private float timer = 0;

    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = timerMax;
            rifle.Shoot();
        }
    }
}
