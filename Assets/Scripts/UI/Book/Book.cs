using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

public class Book : MonoBehaviour
{
    public void SetActive()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
