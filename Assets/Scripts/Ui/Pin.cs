using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float TimeToHide = 3;

    [SerializeField]
    private float TimeToDestroy = 15;

    private Animator Animator;

    private float TimeBeingDisplayed = 0;

    void Awake()
    {
        Animator = GetComponent<Animator>();

        Animator.SetBool("Blinking", true);
    }

    void Start()
    {
        Destroy(gameObject, TimeToDestroy);
    }

    void Update()
    {
        TimeBeingDisplayed += Time.deltaTime;

        if (TimeBeingDisplayed >= TimeToHide)
        {
            gameObject.SetActive(false);
            Animator.SetBool("Blinking", false);
        }
    }
}
