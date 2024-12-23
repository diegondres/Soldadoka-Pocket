using TMPro;
using UnityEngine;
using Utils;

public class AlliesInfo : Singleton<AlliesInfo>
{
    public TextMeshProUGUI alliesInfoText;
    private Unidad lastUnidadSelected;
    public TextMeshProUGUI textSelectionButton;
    public GameObject selectionButton;
    public void ShowInfoUnit(Unidad unidad)
    {
        if (!selectionButton.activeSelf)
        {
            selectionButton.SetActive(true);
        }
        alliesInfoText.text = $"Name: {unidad.name}\n" +
                             $"Attack: {unidad.atk}\n" +
                             $"Defense: {unidad.def}\n";
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
        Debug.Log("SelectDeselect isSelected: " + lastUnidadSelected.isSelected);
        if (lastUnidadSelected.isSelected)
        {
            lastUnidadSelected.isSelected = false;
            textSelectionButton.text = "Select";
            lastUnidadSelected.transform.position = GameController.instance.AlliedField.transform.position + new Vector3(Random.Range(-300, 300), Random.Range(-300, 300));
        }
        else
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            lastUnidadSelected.isSelected = true;
            textSelectionButton.text = "Deselect";
            Battleground.Instance.AddToDuelGround(lastUnidadSelected);

        }
    }

}