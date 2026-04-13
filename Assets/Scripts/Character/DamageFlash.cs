using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;

    private SpriteRenderer[] spriteRenderers;
    private Color[] spriteBaseColors;

    private Renderer[] meshRenderers;
    private Material[] meshMaterials;
    private Color[] meshBaseColors;
    private string[] meshColorProperties;

    private Coroutine activeFlash;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
        spriteBaseColors = new Color[spriteRenderers.Length];

        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteBaseColors[i] = spriteRenderers[i].color;
        }

        CollectMeshRenderersAndMaterials();
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
        SetSpriteColors();
        SetMeshColors();

        yield return new WaitForSeconds(flashDuration);

        ResetFlash();
        activeFlash = null;
    }

    private void ResetFlash()
    {
        ResetSpriteColors();
        ResetMeshColors();
    }

    private void SetSpriteColors()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            Color baseColor = spriteBaseColors[i];

            spriteRenderers[i].color = new Color(
                flashColor.r,
                flashColor.g,
                flashColor.b,
                baseColor.a
            );
        }
    }

    private void ResetSpriteColors()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = spriteBaseColors[i];
        }
    }

    private void SetMeshColors()
    {
        for (int i = 0; i < meshMaterials.Length; i++)
        {
            Material material = meshMaterials[i];
            string colorProperty = meshColorProperties[i];

            if (material == null || string.IsNullOrEmpty(colorProperty))
            {
                continue;
            }

            Color baseColor = meshBaseColors[i];

            material.SetColor(colorProperty, new Color(
                flashColor.r,
                flashColor.g,
                flashColor.b,
                baseColor.a
            ));
        }
    }

    private void ResetMeshColors()
    {
        for (int i = 0; i < meshMaterials.Length; i++)
        {
            Material material = meshMaterials[i];
            string colorProperty = meshColorProperties[i];

            if (material == null || string.IsNullOrEmpty(colorProperty))
            {
                continue;
            }

            material.SetColor(colorProperty, meshBaseColors[i]);
        }
    }

    private void CollectMeshRenderersAndMaterials()
    {
        Renderer[] allRenderers = GetComponentsInChildren<Renderer>(true);

        int count = 0;
        for (int i = 0; i < allRenderers.Length; i++)
        {
            if (!(allRenderers[i] is SpriteRenderer))
            {
                count++;
            }
        }

        meshRenderers = new Renderer[count];
        meshMaterials = new Material[count];
        meshBaseColors = new Color[count];
        meshColorProperties = new string[count];

        int index = 0;
        for (int i = 0; i < allRenderers.Length; i++)
        {
            if (allRenderers[i] is SpriteRenderer)
            {
                continue;
            }

            Renderer renderer = allRenderers[i];
            Material material = renderer.material;

            meshRenderers[index] = renderer;
            meshMaterials[index] = material;

            if (material.HasProperty("_BaseColor"))
            {
                meshColorProperties[index] = "_BaseColor";
                meshBaseColors[index] = material.GetColor("_BaseColor");
            }
            else if (material.HasProperty("_Color"))
            {
                meshColorProperties[index] = "_Color";
                meshBaseColors[index] = material.GetColor("_Color");
            }
            else
            {
                meshColorProperties[index] = null;
                meshBaseColors[index] = Color.white;
                Debug.LogWarning($"Material on {renderer.name} has no _BaseColor or _Color property.", renderer);
            }

            index++;
        }
    }
}