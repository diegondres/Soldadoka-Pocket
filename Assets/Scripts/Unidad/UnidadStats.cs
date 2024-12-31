using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnidadConfi", menuName = "Stats/UnidadStats", order = 1)]
public class UnidadStats : ScriptableObject
{
    [SerializeField] private int minAttack;
    [SerializeField] private int maxAttack;
    [SerializeField] private int minDeffense;
    [SerializeField] private int maxDeffense;
    public string type;
    public GameObject prebab;

    public int GetAtack()
    {
        return Random.Range(minAttack, maxAttack);
    }
    public int GetDeffense()
    {
        return Random.Range(minDeffense, maxDeffense);
    }
    public Unidad CreateUnidad(Transform field, int id, bool isAllied = true)
    {
        GameObject newUnidad = Instantiate(prebab, field);
        Unidad unit = newUnidad.GetComponent<Unidad>();
        _ = new Unidad.UnidadBuilder(unit)
            .WithName(type + " " + id)
            .WithAtk(GetAtack())
            .WithDef(GetDeffense())
            .WithTipo(type)
            .WithIsAllied(isAllied)
            .Build();

        return unit;
    }
}
