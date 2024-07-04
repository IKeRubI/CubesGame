using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    enum TargerEnum
    {
        mouse,
        player,
    }

    [SerializeField]
    private TargerEnum targer;

    [SerializeField]
    private float offSet;

    private Transform playerTransform;


    private void Start()
    {
        playerTransform = Player.Instance.transform;
    }

    private void Update()
    {
        if (Player.Instance == null) return;

        Vector3 dif;
        float rotZ;

        switch (targer)
        {
            case TargerEnum.mouse:

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dif = mousePos - transform.position;

                rotZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offSet * Time.deltaTime);

                break;

            case TargerEnum.player:

                dif = -(transform.position - playerTransform.position);
                rotZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offSet * Time.deltaTime);

                break;
            default:
                break;
        }
    }
}

