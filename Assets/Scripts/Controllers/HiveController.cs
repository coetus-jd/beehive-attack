using System.Collections;
using System.Collections.Generic;
using Bee.Controllers;
using Bee.Enums;
using UnityEngine;

public class HiveController : MonoBehaviour
{
    [Header("Controllers")]
    private GameController GameController;

    void Awake()
    {
        GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
            .GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        DefenseCameBack(collider);
    }

    /// <summary>
    /// When a defense came back to the hive we add the number of swarm used back
    /// </summary>
    /// <param name="collider"></param>
    private void DefenseCameBack(Collider2D collider)
    {
        print($"Collided: {collider.gameObject.tag}");
        
        if (!collider.gameObject.CompareTag(Tags.Defense))
            return;
    }
}
