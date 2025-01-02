using System;
using UnityEngine;

public class DuelGround : MonoBehaviour
{
    [NonSerialized] public Unidad allied;
    [NonSerialized] public Unidad enemy;
    public GameObject alliedField;
    public GameObject enemyField;
    public int index;
    public bool battleEnded = false;

    public void AddAllied(Unidad allied)
    {
        allied.transform.SetParent(alliedField.transform);
        this.allied = allied;
    }
    public void AddEnemy(Unidad enemy)
    {
        enemy.transform.SetParent(enemyField.transform);
        this.enemy = enemy;
    }
    public void CalculateResults()
    {
        CalculateStats();
        int alliesNumber = GetForceNumber(allied);
        int enemiesNumber = GetForceNumber(enemy);
        Battleground.Instance.textAllies.text += "Fuerza aliado: " + alliesNumber + "\n";
        Battleground.Instance.textEnemies.text += "Fuerza enemigos: " + enemiesNumber + "\n";
        if (alliesNumber > enemiesNumber)
        {
            GameController.Instance.DestroyUnidad(enemy);
            enemy = null;
            Battleground.Instance.textAllies.text += "Gana el aliado!!!!!";
        }
        else if (enemiesNumber > alliesNumber)
        {
            GameController.Instance.DestroyUnidad(allied);
            allied = null;
            Battleground.Instance.textEnemies.text += "Gana el Enemigo!!!!!";
        }
        else
        {
            Battleground.Instance.textAllies.text += "Empate señores!";
            Battleground.Instance.textEnemies.text += "Empate señores!";
        }
        battleEnded = true;
    }
    private void CalculateStats()
    {
        allied.RestartModValues();
        enemy.RestartModValues();
        CheckPositionBonuses();
        CheckForDuels();
        if (allied.attack > enemy.attack)
        {
            allied.modMaxValue += 2;
        }
        else
        {
            enemy.modMaxValue += 2;
        }
        if (allied.deffense > enemy.deffense)
        {
            allied.modMinValue += 2;
        }
        else
        {
            enemy.modMinValue += 2;
        }
        allied.maxValue -= allied.fightCount;
        enemy.maxValue -= enemy.fightCount;
        allied.fightCount++;
        enemy.fightCount++;
        Battleground.Instance.textAllies.text = "Min value: " + allied.GetMinValue() + " ;Max value: " + allied.GetMaxValue() + "\n";
        Battleground.Instance.textEnemies.text = "Min value: " + enemy.GetMinValue() + " ;Max value: " + enemy.GetMaxValue() + "\n";
    }

    private void CheckForDuels()
    {
        switch (allied.tipo)
        {
            case GlobalNames.infantry:
                if (enemy.tipo == GlobalNames.archer)
                {
                    enemy.lanzamientosExtras++;
                }
                break;
            case GlobalNames.lancers:
                if (enemy.tipo == GlobalNames.archer)
                {
                    enemy.lanzamientosExtras++;
                    enemy.modMaxValue += 2;
                }
                else if (enemy.tipo == GlobalNames.cabalry)
                {
                    allied.modMaxValue = allied.maxValue * 2;
                }
                break;
            case GlobalNames.archer:
                if (enemy.tipo == GlobalNames.lancers || enemy.tipo == GlobalNames.infantry)
                {
                    allied.lanzamientosExtras++;
                }
                else if (enemy.tipo == GlobalNames.cabalry)
                {
                    enemy.modMaxValue = enemy.maxValue * 2;
                }
                break;
            case GlobalNames.cabalry:
                if (enemy.tipo == GlobalNames.lancers)
                {
                    enemy.modMaxValue = enemy.maxValue * 2;
                }
                else if (enemy.tipo == GlobalNames.archer)
                {
                    allied.modMaxValue = allied.maxValue * 2;
                }
                break;
        }
    }

    private void CheckPositionBonuses()
    {
        if (allied.tipo == GlobalNames.cabalry && (index == 0 || index == Battleground.Instance.grounds.Count - 1))
        {
            allied.modMaxValue += 5;
        }
        if (enemy.tipo == GlobalNames.cabalry && (index == 0 || index == Battleground.Instance.grounds.Count - 1))
        {
            enemy.modMaxValue += 5;
        }
    }

    public int GetForceNumber(Unidad unidad)
    {
        int forceNumber = 0;

        for (int i = 0; i < 1 + unidad.lanzamientosExtras; i++)
        {
            int generatedNumber = UnityEngine.Random.Range(unidad.GetMinValue(), unidad.GetMaxValue());
            if (forceNumber < generatedNumber) forceNumber = generatedNumber;
        }

        return forceNumber;
    }
    public DuelGround SetInitialValues(int index)
    {
        this.index = index;
        return this;
    }
}
