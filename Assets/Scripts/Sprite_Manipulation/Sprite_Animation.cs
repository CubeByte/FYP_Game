using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class MaterialTextureSwitcher : MonoBehaviour
{
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private float switchTime = 1f;

    private Renderer rend;
    private Material mat;
    private bool toggle;

    void Start()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;

        StartCoroutine(SwitchLoop());
    }

    IEnumerator SwitchLoop()
    {
        while (true)
        {
            SetSprite(toggle ? sprite1 : sprite2);
            toggle = !toggle;
            yield return new WaitForSeconds(switchTime);
        }
    }

    void SetSprite(Sprite sprite)
    {
        if (sprite == null || sprite.texture == null || mat == null)
            return;

        if (mat.HasProperty("_BaseMap"))
            mat.SetTexture("_BaseMap", sprite.texture);
        else if (mat.HasProperty("_MainTex"))
            mat.SetTexture("_MainTex", sprite.texture);
        else
            Debug.LogError("Material has no _BaseMap or _MainTex property.");
    }
}