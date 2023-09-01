using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitbox : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) 
    {
        GameManager.instance.GetEnemyManager().Hit();
    }
}