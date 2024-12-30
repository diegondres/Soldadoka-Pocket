using System;
using UnityEngine;
using Utils;


[DefaultExecutionOrder(-100)]
public class GameController : Singleton<GameController>
{
    public GameObject AlliedField;
    public GameObject EnemyField;
    public GameObject prefabUnidad;
    public int maxSizeArmys;
    public GameObject reglas;
    protected override void Awake(){
        base.Awake();
        int sizeArmys = UnityEngine.Random.Range(Battleground.Instance.configuration.size, maxSizeArmys);
        CreateArmy(sizeArmys, AlliedField.transform);
        sizeArmys = UnityEngine.Random.Range(Battleground.Instance.configuration.size, maxSizeArmys);
        CreateArmy(sizeArmys, EnemyField.transform, false);
    }

    public void CreateArmy(int sizeArmy, Transform field, bool isAllied = true){
        for(int i = 0; i < sizeArmy; i++){
            Vector3 position = field.position + new Vector3(UnityEngine.Random.Range(-300, 300), UnityEngine.Random.Range(-300, 300));
            GameObject newUnidad = Instantiate(prefabUnidad, position, Quaternion.identity);
            newUnidad.transform.SetParent(field);
            Unidad unit = newUnidad.GetComponent<Unidad>();
            unit = new Unidad.UnidadBuilder(unit)
                .WithName("Unidad " + i.ToString())
                .WithAtk(UnityEngine.Random.Range(1, 100))
                .WithDef(UnityEngine.Random.Range(1, 100))
                .WithIsAllied(isAllied)
                .Build();
        }
    }

    public void ShowReglas(){
        if(reglas.activeSelf){
            reglas.SetActive(false);
            return;
        }
        reglas.SetActive(true); 
    }
    public void DestroyElement(GameObject gameObject){
        Destroy(gameObject);
    }
}
