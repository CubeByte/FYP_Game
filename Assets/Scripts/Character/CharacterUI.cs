using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public TextMeshProUGUI characterNameText;
    public Image healthFill;
    public TextMeshProUGUI healthText;
    public Image turnVisual;
    public TextMeshProUGUI weaknessText;
    public Canvas weaknessCanvas;

    private void Update()
    {
        transform.forward = transform.position - Camera.main.transform.position;
    }

    public void ToggleTurnVisual(bool toggle)
    {
        turnVisual.gameObject.SetActive(toggle);
    }

    public void SetcharacterText(string characterName)
    {
        characterNameText.text = characterName;
    }

    public void UpdateHealthBar(int currentHP, int maxHP)
    {
        healthText.text = $"{currentHP}/{maxHP}";
        healthFill.fillAmount = (float)currentHP / (float)maxHP;
    }

    public void UpdateCharacterWeakness(bool toggle, string weakness)
    {
        weaknessCanvas.gameObject.SetActive(toggle);
        weaknessText.text = $"{weakness}";
    }
}
