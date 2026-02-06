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
}
