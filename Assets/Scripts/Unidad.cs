using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Unidad : MonoBehaviour
{
    public string nombre;
    public int atk;
    public int def;
    public int minValue = 0;
    public int maxValue = 6;
    public int fightCount = 0;
    public bool isAllied;
    public bool isSelected;
    public Image bordes;
    public DuelGround duelGround;
    public void DisplayUnitInfo()
    {
        if (isAllied)
        {
            AlliesInfo.Instance.ShowInfoUnit(this);
        }
        else
        {
            EnemiesInfo.Instance.ShowInfoUnit(this);
        }
        if (isSelected) return;
        if (Battleground.Instance.battleStart) return;
        Battleground.Instance.AddToDuelGround(this);

    }


    public class UnidadBuilder
    {
        private Unidad unidad;
        public UnidadBuilder(Unidad newUnidad)
        {
            unidad = newUnidad;
        }
        public UnidadBuilder WithName(string name)
        {
            unidad.name = name;
            return this;
        }
        public UnidadBuilder WithAtk(int atk)
        {
            unidad.atk = atk;
            return this;
        }
        public UnidadBuilder WithDef(int def)
        {
            unidad.def = def;
            return this;
        }
        public UnidadBuilder WithIsAllied(bool isAllied)
        {
            unidad.isAllied = isAllied;
            if (isAllied)
            {
                unidad.bordes.color = Color.blue;
            }
            else
            {
                unidad.bordes.color = Color.red;
            }
            return this;
        }
        public Unidad Build()
        {
            return unidad;
        }
    }
}
