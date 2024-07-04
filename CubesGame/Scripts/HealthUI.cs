using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HealthUI : MonoBehaviour
{
    [SerializeField] 
    private GameObject hasProgresGameObject;

    [SerializeField] 
    private Image barImage;

    [SerializeField]
    private bool hideOnStart;

    [SerializeField]
    private float healthLerpMultiplaer = 1f;

    private Health hasProgress;
    private float healthShowCase;


    private void Start()
    {
        hasProgress = hasProgresGameObject.GetComponent<Health>();

        if (hasProgress == null)
        {
            Debug.LogError("GameObject" + hasProgresGameObject + "does not have IHasProgres component");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 1f;
        healthShowCase = 1f;
    }


    private void Update()
    {
        barImage.fillAmount = Mathf.Lerp(barImage.fillAmount, healthShowCase, Time.deltaTime * healthLerpMultiplaer);
    }


    private void HasProgress_OnProgressChanged(object sender, Health.OnProgressChangedEventArgs e)
    {
        healthShowCase = e.healthRatio;

    }
}
