using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed;

    public void Update()
    {
        Move();
    }

    public void Hit()
    {
        GameManager.instance.HitEnter();
    }
    private void Move()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, enemySpeed);
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}