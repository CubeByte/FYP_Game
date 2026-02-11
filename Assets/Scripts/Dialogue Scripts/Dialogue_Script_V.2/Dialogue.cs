using UnityEngine;
[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public string name;
    
    [TextArea(3, 5)]
    public string[] sentences;
}
