using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager UI;    
    public playerMovement playerMovement;
    public Vector3 startPos;
    public GameObject gameOver;
    public playerAnim playerAnim;
    public enemyManager EnemyMngr;
    public GameObject player;

//colliders lados
    public string currentSide;
    public bool collider_izq;
    public bool collider_drc;
    public bool collider_arr;
    public bool collider_abj;

//combate
    public int ammoMax = 10;
    public int ammoCurrent;
    
//stats jugador
    public int vidaMax = 4;
    public int vidaCurrent = 4;
    public int dinero;

//timer cd daño
    public bool cdDaño;
    private float i_cdDaño;
    public float limit_cdDaño;
//cheats
    public bool noDeath = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }
    private void Start()
    {
        ammoCurrent = 10;
        UI.UpdateAmmoCounter();
        UI.UpdateVidaCounter();
        Time.timeScale = 1;
    }
    private void Update() 
    {
        Timers();
    }
    private void Timers()
    {
        if(cdDaño)
        {
            i_cdDaño += Time.deltaTime;
            if (i_cdDaño>limit_cdDaño)
            {
                i_cdDaño = 0;
                cdDaño = false;
            }
            playerAnim.UpdateAnim("hit",cdDaño);
        }
    }
    public void HitEnter()
    {
        if(!cdDaño)
        {
            if (vidaCurrent == 1 && !noDeath) {GameOver();}
            cdDaño = true;
            SetVida(-1);
        }
    }
    private void GameOver()
    {
        EnemyMngr.deathlock = true;
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }
    public void LoadScene(string sceneName_)
    {
        SceneManager.LoadScene(sceneName_);
    }
    public void setCollider(bool bool_, string col_name)
    {
        switch (col_name)
        {
            case "collider_izq":
                collider_izq = bool_;
            break;
            case "collider_drc":
                collider_drc = bool_;
            break;
            case "collider_arr":
                collider_arr = bool_;
            break;
            case "collider_abj":
                collider_abj = bool_;
            break;
        }
    }
    public UIManager GetUI()
    {
        return UI;
    }
    public playerMovement GetPlayerMovement()
    {
        return playerMovement;
    }
    public enemyManager GetEnemyManager()
    {
        return EnemyMngr;
    }
    public GameObject GetPlayerGameObj()
    {
        return player;
    }
    public void GoTo(string position)
    {
        switch (position)
        {
            case "start":
            playerMovement.GoTo(new Vector2(startPos.x,startPos.y));
            break;
        }
    }
    public void SpawnEnemyCheat()
    {
        string[] enemyNames = {"bat","worm"};
        int ranInt = Random.Range(0,enemyNames.Length);
        float posX = player.transform.position.x+2;
        float posY = player.transform.position.y+2;
        EnemyMngr.InstantiateEnemy(new Vector2(posX,posY),enemyNames[ranInt]);
    }
    public void NoDeathCheat()
    {
        noDeath = !noDeath;
    }
    public void SetVida(int n)
    {
        print("SetVida("+vidaCurrent+" + "+"("+n+")"+")");
        vidaCurrent += n;
        UI.UpdateVidaCounter();
    }
}