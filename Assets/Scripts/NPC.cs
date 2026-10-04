using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public enum NPC_ID
    {
        FatherLeft, // (1,1)
        FirstChildAdult, // (2,2)
        FirstChildChoke, // (2,3)
        FirstChildFirstStep, // (4,3)
        MomLeft, // (3, 4)
        Police, // (2,4)
        SecondChildLast, // (3,1)
        SecondChildOnion, // (2,3)
        SpouseBaby, // (1,2)
        SpouseCheat, // (3,2)
        SpouseHammer, // (2,1)
        SpouseHand, // (1,3)
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
        IslandSinkManager.OnLanguageSink += OnLanguageSunk;
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
        IslandSinkManager.OnLanguageSink -= OnLanguageSunk;
    }
}
