using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameComponent;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private GameObject uiPanel;
    public bool isDisplayed = false;

    public List<Dialogue> dialogues;

    [Tooltip("The lower the value, the faster the text speed")] // Recommended speed == 0.05
    public float textSpeed;
    private int indexX; //Current dialougue
    private int indexY; //Current line
    

    private void Start()
    {
        textComponent.text = string.Empty;
        indexX = 0;
        indexY = 0;
        Close();
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            if(textComponent.text == dialogues[indexX].lines[indexY])
            {
                NextLine();
            }
            else
            {
                //fill the chatbox with current lines immediately
                StopAllCoroutines();
                textComponent.text = dialogues[indexX].lines[indexY];
            }
        }
    }

    void StartDialogue()
    {
        indexX = 0;
        indexY = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        nameComponent.text = dialogues[indexX].characterName;  //name of the current speaker
        //Type each character 1 by 1  
        foreach (char c in dialogues[indexX].lines[indexY].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    { 
        if(indexX < dialogues.Count -1)
        {
            if(indexY < dialogues[indexX].lines.Length -1)
            {
                indexY++; //next line
            }
            else
            {
                indexX++;
                indexY = 0;  //if there are more dialogue, next dialogue
            }
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else if(indexX == dialogues.Count - 1)
        {
            if (indexY < dialogues[indexX].lines.Length - 1)
            {
                indexY++; //next line
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                Close();
            }
        }
        else
        {
            Close();
        }
    }
    public void SetUp()
    {
        uiPanel.SetActive(true);
        isDisplayed = true;
        textComponent.text = string.Empty;
        StartDialogue();
    }

    public void Close()
    {
        uiPanel.SetActive(false);
        isDisplayed = false;
    }
}



[System.Serializable]
public class Dialogue //A class that store the name of the speaker and their lines, can be edit in Unity inspector
{
    public string characterName;
    public string[] lines;
}
