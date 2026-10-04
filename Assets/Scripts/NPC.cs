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

    private static NPC activeNPC;
    public NPC_ID npcID;
    public bool isLanguageSunk; // ADD LOGIC FOR GARBLED TEXT
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;


    public static event Action<NPC.NPC_ID> OnNPCInteractionEnd;

    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLanguageSunk = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
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
        if (activeNPC != null && activeNPC != this)
        {
            activeNPC.EndDialogue();
        }

        activeNPC = this;

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
        int dialogueLength = isLanguageSunk
        ? dialogueData.garbledDialogueLines.Length
        : dialogueData.dialogueLines.Length;

        if (isTyping)
        {
            StopAllCoroutines();

            if (isLanguageSunk)
            {
                dialogueText.SetText(dialogueData.garbledDialogueLines[dialogueIndex]);
            }
            else
            {
                dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            }

            isTyping = false;
        }
        else if (++dialogueIndex < dialogueLength)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }

        /*
        if(isLanguageSunk)
        {
            if (isTyping)
            {
                // skip typing animation and show the full line
                StopAllCoroutines();
                dialogueText.SetText(dialogueData.garbledDialogueLines[dialogueIndex]);
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
        else
        {
            //Debug.Log($"[{npcID}] NextLine BEFORE: dialogueIndex = {dialogueIndex}, total lines = {dialogueData.dialogueLines.Length}");

            if (isTyping)
            {
                StopAllCoroutines();

                dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
                isTyping = false;

                //Debug.Log($"[{npcID}] Finished current line manually");
            }
            else if (++dialogueIndex < dialogueData.dialogueLines.Length)
            {
                //Debug.Log($"[{npcID}] Starting line {dialogueIndex}");

                StartCoroutine(TypeLine());
            }
            else
            {
                //Debug.Log($"[{npcID}] Reached END. dialogueIndex = {dialogueIndex}, total = {dialogueData.dialogueLines.Length}");

                EndDialogue();
            }
        }
        */

    }

    IEnumerator TypeLine()
    {
        //Debug.Log("TypeLine from " + npcID);

        isTyping = true;
        dialogueText.SetText("");

        if (isLanguageSunk)
        {
            foreach (char letter in dialogueData.garbledDialogueLines[dialogueIndex])
            {
                //Debug.Log("Writing dialogue from " + npcID);
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData.typingSpeed);
            }
        }
        else
        {
            foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueData.typingSpeed);
            }
        }

        isTyping = false;

        /*
        // TEMP: always auto-progress, ignore autoProgressLines
        yield return new WaitForSeconds(dialogueData.autoProgressDelay);
        NextLine();
        */
    }

    public void NextLineButton()
    {
        if (activeNPC != null)
        {
            activeNPC.NextLine();
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

        if (activeNPC == this)
        {
            activeNPC = null;
        }
    }

    void OnLanguageSunk()
    {
        isLanguageSunk = true;
    }

    void OnDestroy()
    {
        
    }
}
