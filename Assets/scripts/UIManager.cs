using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject cheatMenu;
    public TextMeshProUGUI ammoCounter;
    public TextMeshProUGUI vidaCounter;
    public TextMeshProUGUI cheatVidaValue;
    public GameObject cheatNoDeathButton;
    public void UICheatSetVida()
    {
        string nString = cheatVidaValue.text;
        int.TryParse(nString, out int n);
        GameManager.instance.SetVida(n);
    }
    public void GoTo(string position)
    {
        GameManager.instance.GoTo(position);
    }
    public void UICheatSpawnEnemy()
    {
        GameManager.instance.SpawnEnemyCheat();
    }
    public void UINoDeathCheat()
    {
        GameManager.instance.NoDeathCheat();
        if(GameManager.instance.noDeath)
        cheatNoDeathButton.GetComponent<TextMeshProUGUI>().color = Color.green;
        else
        cheatNoDeathButton.GetComponent<TextMeshProUGUI>().color = Color.white;
    }
    public void ToggleCheatMenu()
    {
        cheatMenu.SetActive(!cheatMenu.activeSelf);
    }
    public void UpdateAmmoCounter()
    {
        ammoCounter.text = GameManager.instance.ammoCurrent.ToString();
    }
    public void UpdateVidaCounter()
    {
        vidaCounter.text = GameManager.instance.vidaCurrent.ToString();
    }
    public void UILoadScene(string sceneName_)
    {
        GameManager.instance.LoadScene(sceneName_);
    }
}