using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float TimeToHide = 3;

    [SerializeField]
    private float TimeToDestroy = 10;

    private float TimeBeingDisplayed = 0;

    void Start()
    {
        Destroy(gameObject, TimeToDestroy);
    }

    void Update()
    {
        TimeBeingDisplayed += Time.deltaTime;

        if (TimeBeingDisplayed >= TimeToHide)
            gameObject.SetActive(false);
    }
}
