using TMPro;
using UnityEngine;
using Utils;

public class EnemiesInfo : Singleton<EnemiesInfo>
{
    public TextMeshProUGUI enemiesInfoText;
    private Unidad lastUnidadSelected;
    public TextMeshProUGUI textSelectionButton;
    public GameObject selectionButton;
    public void ShowInfoUnit(Unidad unidad)
    {
        if (!selectionButton.activeSelf)
        {
            selectionButton.SetActive(true);
        }
        enemiesInfoText.text = $"Name: {unidad.name}\n" +
                             $"Attack: {unidad.attack}\n" +
                             $"Defense: {unidad.deffense}\n";
        lastUnidadSelected = unidad;
        if (unidad.isSelected)
        {
            textSelectionButton.text = "Deselect";
        }
        else
        {
            textSelectionButton.text = "Select";
        }
    }

    public void SelectDeselect()
    {

        if (lastUnidadSelected.isSelected)
        {
            lastUnidadSelected.duelGround.enemy = null;
            lastUnidadSelected.duelGround = null;
            lastUnidadSelected.isSelected = false;
            textSelectionButton.text = "Select";
            lastUnidadSelected.transform.SetParent(GameController.instance.EnemyField.transform);
        }
        else
        {
            lastUnidadSelected.isSelected = true;
            textSelectionButton.text = "Deselect";
            Battleground.Instance.AddToDuelGround(lastUnidadSelected);

        }
    }
}