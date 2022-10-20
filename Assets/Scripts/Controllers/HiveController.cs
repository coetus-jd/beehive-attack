using System.Collections;
using System.Collections.Generic;
using Bee.Controllers;
using Bee.Enums;
using UnityEngine;

public class HiveController : MonoBehaviour
{
    public bool EnemyAttacked { get; private set; }
    
    [Header("Controllers")]
    private GameController GameController;

    void Awake()
    {
        GameController = GameObject.FindGameObjectWithTag(Tags.GameController)
            .GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (HasLostGame(collider))
            return;

        DefenseCameBack(collider);
    }

    /// <summary>
    /// When a defense came back to the hive we add the number of swarm used back
    /// </summary>
    /// <param name="collider"></param>
    private void DefenseCameBack(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag(Tags.Defense))
            return;
    }

    private bool HasLostGame(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag(Tags.Enemy))
            return false;

        EnemyAttacked = true;
        return true;
    }
}
