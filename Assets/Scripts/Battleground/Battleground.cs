using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Battleground : PersistentSingleton<Battleground>
{
    public BattlegroundConfiguration configuration;
    public List<DuelGround> grounds;
    public GameObject DuelGroundAllies;
    public GameObject DuelGroundEnemies;
    public GameObject StartBattleButton;
    void Start()
    {
        grounds = new List<DuelGround>();
        for (int i = 0; i < configuration.size; i++)
        {
            grounds.Add(new DuelGround.DuelGroundBuilder()
                .withBiome("Forest")
                .build());
        }
    }

    public void AddToDuelGround(Unidad unidad)
    {
        foreach (DuelGround ground in grounds)
        {
            if (unidad.isAllied && ground.allied == null)
            {
                ground.AddAllied(unidad);
                unidad.transform.SetParent(DuelGroundAllies.transform);
                unidad.duelGround = ground;
                break;
            }
            if (!unidad.isAllied && ground.enemy == null)
            {
                ground.AddEnemy(unidad);
                unidad.transform.SetParent(DuelGroundEnemies.transform);
                unidad.duelGround = ground;
                break;
            }
        }
        if (!grounds.Any(x => x.allied == null || x.enemy == null))
        {
            StartBattleButton.SetActive(true);
        }
    }
    public void StartBattle(){
        Debug.Log("Battle Started");
    }
}
