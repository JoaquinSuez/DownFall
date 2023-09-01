using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    public GameObject prefabBat;
    public GameObject prefabWorm;
    public bool deathlock = false;
    public void Hit()
    {
        GameManager.instance.HitEnter();
    }
    public void InstantiateEnemy(Vector2 vector2, string prefab)
    {
        GameObject player = GameManager.instance.GetPlayerGameObj();
        switch(prefab)
        {
            case"bat":
                GameObject batInst = Instantiate(prefabBat, vector2, Quaternion.identity);
                batInst.GetComponent<batScript>().StartFunc(player);
            break;
            case"worm":
                GameObject wormInst = Instantiate(prefabWorm, vector2, Quaternion.identity);
                wormInst.GetComponent<wormScript>().StartFunc(player);
            break;
        }
    }
}