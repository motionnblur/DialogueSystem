using System.Collections;
using UnityEngine;

public class DialogueBrain : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text text;
    [SerializeField] private TextAsset dialogueFileName;
    public float letterDelay = 0.2f; // Delay between displaying words
    private int letterIndex = 0;
    private DialogueBase dialogueBase;
    private DialogueNode introNode;
    
    private void Awake()
    {
        dialogueBase = new DialogueBase(dialogueFileName);
    }

    private void Start()
    {
        //StartCoroutine(DisplayLetters());
        TextAsset jsonFile = dialogueFileName;
        if (jsonFile != null)
        {
            introNode = dialogueBase.FindNodeById("intro");
            string introText = introNode.text;
            if(introNode != null)
            {
                Debug.Log("Intro text: " + introText);
                StartCoroutine(DisplayLetters());
            }
        }
        else
        {
            Debug.LogError("JSON file not found: " + dialogueFileName);
        }
    }

    private IEnumerator DisplayLetters()
    {
        while (letterIndex < introNode.text.Length)
        {
            char currentLetter = introNode.text[letterIndex];
            text.text += currentLetter;

            yield return new WaitForSeconds(letterDelay);

            letterIndex++;
        }
    }
}
