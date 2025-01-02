using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;


[DefaultExecutionOrder(-100)]
public class GameController : Singleton<GameController>
{
    public GameObject AlliedField;
    public GameObject EnemyField;
    public List<UnidadStats> creatablesUnities;
    public List<Unidad> allies;
    public List<Unidad> enemies;
    public int maxSizeArmys;
    public GameObject reglas;
    public GameObject CanvasEndGame;
    public TextMeshProUGUI textEndGame;

    protected override void Awake()
    {
        base.Awake();
        int sizeArmys = UnityEngine.Random.Range(Battleground.Instance.configuration.size, maxSizeArmys);
        CreateArmy(sizeArmys, AlliedField.transform);
        sizeArmys = UnityEngine.Random.Range(Battleground.Instance.configuration.size, maxSizeArmys);
        CreateArmy(sizeArmys, EnemyField.transform, false);
        CanvasEndGame.SetActive(false);
    }

    private void CreateArmy(int sizeArmy, Transform field, bool isAllied = true)
    {
        for (int i = 0; i < sizeArmy; i++)
        {
            Unidad newUnidad = creatablesUnities[UnityEngine.Random.Range(0, creatablesUnities.Count)].CreateUnidad(field, i, isAllied);
            if (isAllied) allies.Add(newUnidad);
            else enemies.Add(newUnidad);
        }
    }

    public void ShowReglas()
    {
        if (reglas.activeSelf)
        {
            reglas.SetActive(false);
            return;
        }
        reglas.SetActive(true);
    }
    public void EndGame(bool winAllies)
    {
        CanvasEndGame.SetActive(true);
        if (winAllies)
        {
            textEndGame.text = "Felicidades por la victoria campeon, pudiste defender al mundo de esos malosos.\n Esta fue solo una demo, pero preparate que la version completa tendra incontables nuevas aventuras.\n\n Wishlist Now!!";
        }
        else
        {
            textEndGame.text = "Lamentablemente los malosos ganaron, pero arriba esos animos campeon, que puedes volver a enfrentarlos nuevamente todas las veces que quieras.\n  Esto fue solo una demo, pero preparate que la version completa tendra incontables nuevas aventuras.\n\n Wishlist Now!!";
        }
    }
    public void DestroyUnidad(Unidad unidad)
    {
        if (unidad.isAllied)
        {
            allies.Remove(unidad);
        }
        else
        {
            enemies.Remove(unidad);
        }
        Destroy(unidad.gameObject);
    }
}
