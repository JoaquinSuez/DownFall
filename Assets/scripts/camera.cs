using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour

{
    public GameObject cam;
    public GameObject player;
    float yPos;
    float xPos;
    void Update()
    {
        yPos = player.transform.position.y;
        xPos = cam.transform.position.x;
        Vector2 newPos = new Vector2(xPos,yPos);
        cam.transform.position = newPos;
    }
}
