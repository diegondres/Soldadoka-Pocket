using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;


[DefaultExecutionOrder(-100)]
public class GameController : Singleton<GameController>
{
    public GameObject AlliedField;
    public GameObject EnemyField;
    public List<UnidadStats> creatablesUnities;
    public int maxSizeArmys;
    public GameObject reglas;

    protected override void Awake()
    {
        base.Awake();
        int sizeArmys = UnityEngine.Random.Range(Battleground.Instance.configuration.size, maxSizeArmys);
        CreateArmy(sizeArmys, AlliedField.transform);
        sizeArmys = UnityEngine.Random.Range(Battleground.Instance.configuration.size, maxSizeArmys);
        CreateArmy(sizeArmys, EnemyField.transform, false);
    }

    private void CreateArmy(int sizeArmy, Transform field, bool isAllied = true)
    {
        for (int i = 0; i < sizeArmy; i++)
        {
            creatablesUnities[UnityEngine.Random.Range(0, creatablesUnities.Count)].CreateUnidad(field, i, isAllied);
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
    public void DestroyElement(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
