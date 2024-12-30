using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Battleground : PersistentSingleton<Battleground>
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
            grounds.Add(duelground.GetComponent<DuelGround>());
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
        Debug.Log("Battle Started");
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
            Debug.Log("Se ha acabado la batalla");
        }
    }
    public DuelGround GetActiveGround()
    {
        return grounds[activeGroundIndex];
    }
}
