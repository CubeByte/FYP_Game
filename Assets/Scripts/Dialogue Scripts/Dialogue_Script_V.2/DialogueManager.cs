using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public Canvas dialogueCanvas;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        
        sentences.Clear();

        foreach (var sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        //string sentence = sentences.Dequeue();
        dialogueText.text = sentences.Dequeue();
    }

    void EndDialogue()
    {
        dialogueCanvas.enabled = false;   
        Debug.Log("Ending Dialogue");
    }
    
    
}
