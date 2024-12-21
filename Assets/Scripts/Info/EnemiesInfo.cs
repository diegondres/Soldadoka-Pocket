using TMPro;
using Utils;

public class EnemiesInfo : Singleton<EnemiesInfo>
{
    public TextMeshProUGUI enemiesInfoText;
    public void ShowInfoUnit(Unidad unidad)
    {
        enemiesInfoText.text = $"Name: {unidad.name}\n" +
                             $"Attack: {unidad.atk}\n" +
                             $"Defense: {unidad.def}\n";
    }
}