using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Need to add IInteractable functionality!!!
public class NPC : MonoBehaviour
{
    public enum NPC_ID
    {
        NPC1,
        NPC2
    }

    public NPC_ID npcID;
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;


    public static event Action<NPC.NPC_ID> OnNPCInteractionEnd;

    void Awake()
    {
        PlayerMovement.OnNPCInteracted += Interact;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(NPC.NPC_ID id)
    {
        if (id != npcID)
            return;

        if (dialogueData == null || (PauseController.IsPaused && !isDialogueActive))
            return;

        if(isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());
    }

    public void NextLine()
    { 
        if(isTyping)
        {
            // skip typing animation and show the full line
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;
        bool autoProgress = true; // temp because of buggy SO ui

        // bool autoProgress = dialogueData.autoProgressLines[dialogueIndex];
        if (dialogueData.autoProgressLines.Length > dialogueIndex && autoProgress)
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.SetPause(false);
        OnNPCInteractionEnd?.Invoke(npcID);
    }

    void OnDestroy()
    {
        PlayerMovement.OnNPCInteracted -= Interact;
    }
}
