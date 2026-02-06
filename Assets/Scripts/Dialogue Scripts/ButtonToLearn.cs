using UnityEngine;
using UnityEngine.UI;

public class ButtonToLearn : MonoBehaviour
{
    public void OnClick()
    {
        NewScriptableObjectScript.setIsKnown("you");
    }
}
