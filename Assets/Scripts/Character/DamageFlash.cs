using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private Renderer[] _renderer;

    void Start()
    {
        _renderer = GetComponentsInChildren<Renderer>();
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
        
        //to pause time for a flash
        IEnumerator FlashCoroutine()
        {
            SetMREmission(Color.red);
            
            yield return new WaitForSeconds(0.2f);
            
            SetMREmission(Color.black);
        }
    }

    //to change color in the array for character to display the damage flash
    void SetMREmission(Color color)
    {
        for (int i = 0; i < _renderer.Length; i++)
        {
            Material mat = _renderer[i].material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", color);
        }
    }
}
