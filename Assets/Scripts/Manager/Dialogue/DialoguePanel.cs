using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private Image dialoguePortraitImage;
    [SerializeField] private Text dialogueName;
    [SerializeField] private Text dialogueTextArea;
    [SerializeField] private Button nextButton;


    public void SetDialogueName(string setName) {
        dialogueName.text = setName;
    }

    public void SetDialogueText(string setText) {
        dialogueTextArea.text = setText;
    }

    public Button GetNextButton() {
        return nextButton;
    }
}
