using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Utils;

public class Battleground : Singleton<Battleground>
{
    public BattlegroundConfiguration configuration;
    public List<DuelGround> grounds;
    public GameObject StartBattleButton;
    public GameObject advanceButton;
    public TextMeshProUGUI textAllies;
    public TextMeshProUGUI textEnemies;
    public GameObject prefabDuelGround;
    public Transform arena;
    private int activeGroundIndex = 0;
    public bool battleStart = false;
    void Start()
    {
        grounds = new List<DuelGround>();
        for (int i = 0; i < configuration.size; i++)
        {
            GameObject duelground = Instantiate(prefabDuelGround, arena);
            grounds.Add(duelground.GetComponent<DuelGround>().SetInitialValues(i));
        }
    }

    public void AddToDuelGround(Unidad unidad)
    {
        foreach (DuelGround ground in grounds)
        {
            if (unidad.isAllied && ground.allied == null)
            {
                ground.AddAllied(unidad);
                unidad.duelGround = ground;
                break;
            }
            if (!unidad.isAllied && ground.enemy == null)
            {
                ground.AddEnemy(unidad);
                unidad.duelGround = ground;
                break;
            }
        }
        if (!grounds.Any(x => x.allied == null || x.enemy == null))
        {
            StartBattleButton.SetActive(true);
        }
    }
    public void StartBattle()
    {
        battleStart = true;
        if (!advanceButton.activeSelf)
        {
            advanceButton.SetActive(true);
        }
    }
    public void Advance()
    {
        if (activeGroundIndex < grounds.Count && !grounds[activeGroundIndex].battleEnded)
        {
            grounds[activeGroundIndex].CalculateResults();
            activeGroundIndex++;
        }
        else
        {
            int alliesAlive = GameController.instance.allies.Count;
            int enemiesAlive = GameController.instance.enemies.Count;
            int lowerQuantity = alliesAlive < enemiesAlive ? alliesAlive : enemiesAlive;
            if (alliesAlive == 0)
            {
                textAllies.text = "Perdiste";
                textEnemies.text = "Ganó";
                GameController.instance.EndGame(false);
            }
            else if (enemiesAlive == 0)
            {
                textAllies.text = "Ganaste";
                textEnemies.text = "Perdió";
                GameController.instance.EndGame(true);
            }
            else
            {
                textAllies.text = "La Batalla continua!!";
                textEnemies.text = "La Batalla continua!!";
                foreach (DuelGround ground in grounds)
                {
                    ground.battleEnded = false;
                    if (ground.allied != null)
                    {
                        ground.allied.duelGround = null;
                        ground.allied.isSelected = false;
                        ground.allied.transform.SetParent(GameController.Instance.AlliedField.transform);
                        ground.allied = null;
                    }
                    if (ground.enemy != null)
                    {
                        ground.enemy.duelGround = null;
                        ground.enemy.isSelected = false;
                        ground.enemy.transform.SetParent(GameController.Instance.EnemyField.transform);
                        ground.enemy = null;
                    }
                }
                battleStart = false;
                advanceButton.SetActive(false);
                activeGroundIndex = 0;
                StartBattleButton.SetActive(false);
                if (grounds.Count > lowerQuantity)
                {
                    for (int i = 0; i < grounds.Count; i++)
                    {
                        Destroy(grounds[i].gameObject);
                    }
                    grounds.Clear();
                    for (int i = 0; i < lowerQuantity; i++)
                    {
                        GameObject duelground = Instantiate(prefabDuelGround, arena);
                        grounds.Add(duelground.GetComponent<DuelGround>().SetInitialValues(i));
                    }
                }
            }
        }
    }

}
