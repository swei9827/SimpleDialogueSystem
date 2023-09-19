using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;

    public List<Dialogue> dialogue; //dialougue

    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        var dialogueSystem = interactor.dialougueSystemUI;
        dialogueSystem.dialogues.Clear(); //empty the list of existing lines
        foreach(Dialogue d in dialogue)
        {
            dialogueSystem.dialogues.Add(d); //add current npc unique dialougue into dialogue system's dialougue
        }
        dialogueSystem.SetUp();
               
        return true;
    }
}
