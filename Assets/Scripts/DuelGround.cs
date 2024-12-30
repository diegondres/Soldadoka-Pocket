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
        int alliesNumber = UnityEngine.Random.Range(allied.minValue, allied.maxValue);
        int enemiesNumber = UnityEngine.Random.Range(enemy.minValue, enemy.maxValue);
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
        if (allied.atk > enemy.atk)
        {
            allied.maxValue += 2;
        }
        else
        {
            enemy.maxValue += 2;
        }
        if (allied.def > enemy.def)
        {
            allied.minValue += 2;
        }
        else
        {
            enemy.minValue += 2;
        }
        allied.maxValue -= allied.fightCount;
        enemy.maxValue -= enemy.fightCount;
        allied.fightCount++;
        enemy.fightCount++;
        Battleground.Instance.textAllies.text = "Min value: " + allied.minValue + " ;Max value: " + allied.maxValue + "\n";
        Battleground.Instance.textEnemies.text = "Min value: " + enemy.minValue + " ;Max value: " + enemy.maxValue + "\n";

    }
}
