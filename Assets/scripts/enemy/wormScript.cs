using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormScript : MonoBehaviour
{
    public GameObject player;
    public float enemySpeed;
    public enemyManager enemyMngr_;
    private Rigidbody2D rb;
    public void StartFunc(GameObject player_)
    {
        player = player_;
    }
    public void Start() 
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        enemyMngr_ = GameManager.instance.GetEnemyManager();
    }
    public void Update()
    {
        Move();
    }
    private void Move()
    {
        if(!enemyMngr_.deathlock) 
            {
                //Movimiento del enemigo
            }
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
