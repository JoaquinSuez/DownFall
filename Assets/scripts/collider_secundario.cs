using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider_secundario : MonoBehaviour
{
    private string col_name;
    /*
    collider_izq;
    collider_drc;
    collider_arr;
    collider_abj;
    */
    private void Start() 
    {
        col_name = this.gameObject.name;
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        switch (col_name)
        {
            case "collider_izq":
                GameManager.instance.setCollider(true,col_name);
            break;
            case "collider_drc":
                GameManager.instance.setCollider(true,col_name);
            break;
            case "collider_arr":
                GameManager.instance.setCollider(true,col_name);
            break;
            case "collider_abj":
                GameManager.instance.setCollider(true,col_name);
            break;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        switch (col_name)
        {
            case "collider_izq":
                GameManager.instance.setCollider(false,col_name);
            break;
            case "collider_drc":
                GameManager.instance.setCollider(false,col_name);
            break;
            case "collider_arr":
                GameManager.instance.setCollider(false,col_name);
            break;
            case "collider_abj":
                GameManager.instance.setCollider(false,col_name);
            break;
        }
    }
}