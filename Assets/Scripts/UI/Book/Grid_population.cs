using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Grid_population : MonoBehaviour
{
    public GameObject grid;
    public GameObject loadout;
    public NewScriptableObjectScript WordPairs;
    public TextMeshProUGUI[] grid_elements;
    public Image Panel;
    
    public void SetActiveGrid()
    {
        if (grid.activeSelf)
        {
            Panel.gameObject.SetActive(false);
            grid.SetActive(false);
        }
        else
        {
            loadout.SetActive(false);
            Panel.gameObject.SetActive(true);
            grid.SetActive(true);
            for (int i = 0; i < grid_elements.Length; i++)
            {
                if (WordPairs.ReturnWordPairInPosition(i) == null)
                {
                    grid_elements[i].text = "???";
                }
                grid_elements[i].text = WordPairs.ReturnWordPairInPosition(i);
            }
        }
    }

    public void SetActiveLoadout()
    {
        if (loadout.activeSelf)
        {
            Panel.gameObject.SetActive(false);
            loadout.SetActive(false);
        }
        else
        {
            grid.SetActive(false);
            Panel.gameObject.SetActive(true);
            loadout.SetActive(true);
        }
    }
}
