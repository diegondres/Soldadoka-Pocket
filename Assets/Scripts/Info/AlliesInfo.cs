using TMPro;
using Utils;

public class AlliesInfo : Singleton<AlliesInfo>
{
    public TextMeshProUGUI alliesInfoText;
    public void ShowInfoUnit(Unidad unidad)
    {
        alliesInfoText.text = $"Name: {unidad.name}\n" +
                             $"Attack: {unidad.atk}\n" +
                             $"Defense: {unidad.def}\n";
    }
}