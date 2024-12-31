using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Unidad : MonoBehaviour
{
    public string tipo;
    public int attack;
    public int deffense;
    public int minValue = 0;
    public int maxValue = 6;
    public int modMinValue = 0;
    public int modMaxValue = 0;
    public int lanzamientosExtras = 0;
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
    }

    public void RestartModValues()
    {
        modMaxValue = 0;
        modMinValue = 0;
        lanzamientosExtras = 0;
    }
    public int GetMaxValue() => maxValue + modMaxValue;
    public int GetMinValue() => minValue + modMinValue;


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
            unidad.attack = atk;
            return this;
        }
        public UnidadBuilder WithDef(int def)
        {
            unidad.deffense = def;
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
        public UnidadBuilder WithTipo(string tipo)
        {
            unidad.tipo = tipo;
            return this;
        }
        public Unidad Build()
        {
            return unidad;
        }
    }
}
