using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Grid_population : MonoBehaviour
{
    public GameObject grid;
    public NewScriptableObjectScript WordPairs;
    public TextMeshProUGUI[] grid_elements;
    public Image Panel;
    
    public void SetActive()
    {
        if (gameObject.activeSelf)
        {
            Panel.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
            gameObject.SetActive(true);
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
}
