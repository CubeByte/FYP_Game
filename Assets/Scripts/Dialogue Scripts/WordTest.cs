using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordTest : MonoBehaviour
{
        [SerializeField] private NewScriptableObjectScript dialogueText;
        [SerializeField] private TextMeshProUGUI dialogueKnown;
        [SerializeField] private TextMeshProUGUI dialogueUnknown;
        void Start()
        {
                dialogueKnown.text = dialogueText.getDialogue().GetDisplayLineKnown(dialogueText.getText());
                dialogueUnknown.text = dialogueText.getDialogue().GetDisplayLineUnKnown(dialogueText.getText());
        }

        void Update()
        {
                dialogueKnown.text = dialogueText.getDialogue().GetDisplayLineKnown(dialogueText.getText());
                dialogueUnknown.text = dialogueText.getDialogue().GetDisplayLineUnKnown(dialogueText.getText());
        }
}