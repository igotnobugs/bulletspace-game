using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour {

    public DialoguePanel dialoguePanel;

    private Dialogue dialogueToUse;
    private Queue<Dialogue.Script> scripts;
    private Action onCompleteFunc;

    private float dialogueTime;
    private Coroutine dialogueTimeHandle;

    private void Awake() {
        scripts = new Queue<Dialogue.Script>();
    }

    public void TriggerDialogue(Dialogue dialogue, Action onCompleteFunc = null) {
        dialogueToUse = dialogue;
        StartDialogue(dialogueToUse, onCompleteFunc);
    }

    private void StartDialogue(Dialogue dialogue, Action onCompleteFunc = null) {
        scripts.Clear();

        foreach (Dialogue.Script script in dialogue.scripts) {
            scripts.Enqueue(script);
        }

        this.onCompleteFunc = onCompleteFunc;
        dialoguePanel.gameObject.SetActive(true);
        dialoguePanel.GetNextButton().onClick.AddListener(() =>
            DisplayNextSentence()
        );

        DisplayNextSentence();
    }

    private void DisplayNextSentence() {
        
        this.TryStopCoroutine(ref dialogueTimeHandle);
        if (scripts.Count == 0) {
            EndDialogue();
            return;
        }
        
        Dialogue.Script script = scripts.Dequeue();
        dialoguePanel.SetDialogueName(script.name);
        dialoguePanel.SetDialogueText(script.sentence);

        dialogueTime = script.time;

        if (script.time > 0) {
            this.RestartCoroutine(DelayToNextDialogue(), ref dialogueTimeHandle);
        }
    }

    private void EndDialogue() {
        dialoguePanel.gameObject.SetActive(false);
        scripts.Clear();
        onCompleteFunc?.Invoke();
    }

    IEnumerator DelayToNextDialogue() {
        //Debug.Log("Waiting");
        yield return new WaitForSeconds(dialogueTime);

        //Debug.Log("Next");
        DisplayNextSentence();
    }
}