using TMPro;
using UnityEngine;

public class Grid_population : MonoBehaviour
{
    public GameObject grid;
    public NewScriptableObjectScript WordPairs;
    public TextMeshProUGUI[] grid_elements;
    
    public void SetActive()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
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
