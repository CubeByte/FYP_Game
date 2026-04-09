using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;

    private Material[] emissionMaterials;
    private SpriteRenderer[] spriteRenderers;
    private Color[] spriteBaseColors;
    private Coroutine activeFlash;

    void Awake()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
        emissionMaterials = CollectEmissionMaterials(renderers);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        spriteBaseColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteBaseColors[i] = spriteRenderers[i].color;
        }
    }

    public void Flash()
    {
        if (activeFlash != null)
        {
            StopCoroutine(activeFlash);
            ResetFlash();
        }

        activeFlash = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        SetMeshEmission(flashColor);
        SetSpriteColors(flashColor);

        yield return new WaitForSeconds(flashDuration);

        ResetFlash();
        activeFlash = null;
    }

    private void ResetFlash()
    {
        SetMeshEmission(Color.black);
        ResetSpriteColors();
    }

    private void SetMeshEmission(Color color)
    {
        for (int i = 0; i < emissionMaterials.Length; i++)
        {
            Material material = emissionMaterials[i];
            material.EnableKeyword("_EMISSION");
            material.SetColor("_EmissionColor", color);
        }
    }

    private void SetSpriteColors(Color color)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = color;
        }
    }

    private void ResetSpriteColors()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = spriteBaseColors[i];
        }
    }

    private Material[] CollectEmissionMaterials(Renderer[] renderers)
    {
        int materialCount = 0;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] is SpriteRenderer)
            {
                continue;
            }

            materialCount++;
        }

        Material[] materials = new Material[materialCount];
        int materialIndex = 0;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] is SpriteRenderer)
            {
                continue;
            }

            materials[materialIndex] = renderers[i].material;
            materialIndex++;
        }

        return materials;
    }
}
