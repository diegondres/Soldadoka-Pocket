using System;
using UnityEngine;

public class DuelGround : MonoBehaviour
{
    [NonSerialized] public Unidad allied;
    [NonSerialized] public Unidad enemy;
    public GameObject alliedField;
    public GameObject enemyField;

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
            GameController.Instance.DestroyElement(enemy.gameObject);
            enemy = null;
            Battleground.Instance.textAllies.text += "Gana el aliado!!!!!";
        }
        else if (enemiesNumber > alliesNumber)
        {
            GameController.Instance.DestroyElement(allied.gameObject);
            enemy = null;
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
        
        if (allied.tipo == GlobalNames.infantry)
        {
            if (enemy.tipo == GlobalNames.archer)
            {
                enemy.lanzamientosExtras++;
            }
            else if(enemy.tipo == GlobalNames.cabalry){
                
            }
        }
        else if (allied.tipo == GlobalNames.lancers)
        {

        }
        else if (allied.tipo == GlobalNames.archer)
        {

        }
        else if (allied.tipo == GlobalNames.cabalry)
        {
        }
    }

    public int GetForceNumber(Unidad unidad)
    {
        int forceNumber = 0;

        for (int i = 0; i < 1 + unidad.lanzamientosExtras; i++)
        {
            int generatedNumber = UnityEngine.Random.Range(allied.GetMinValue(), allied.GetMaxValue());
            if (forceNumber < generatedNumber) forceNumber = generatedNumber;
        }

        return forceNumber;
    }
}
