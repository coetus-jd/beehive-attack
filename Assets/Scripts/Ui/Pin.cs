using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float TimeToHide = 3;


    private float TimeBeingDisplayed = 0;

    void Update()
    {
        TimeBeingDisplayed += Time.deltaTime;

        if (TimeBeingDisplayed >= TimeToHide)
            gameObject.SetActive(false);
    }
}
