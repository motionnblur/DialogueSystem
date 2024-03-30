using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBrain : MonoBehaviour
{
    Queue<IEnumerator> coroutines = new Queue<IEnumerator>();
    [SerializeField] UnityEngine.UI.Text text;
    [SerializeField] private TextAsset dialogueFileName;
    public float letterDelay = 0.2f; // Delay between displaying words
    private int letterIndex = 0;
    private DialogueBase dialogueBase;
    
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
            coroutines.Enqueue(DisplayLetters(dialogueBase.FindNodeById("intro")));
            coroutines.Enqueue(DisplayLetters(dialogueBase.FindNodeById("continueExploring")));
            StartCoroutine(RunCoroutines());
        }
        else
        {
            Debug.LogError("JSON file not found: " + dialogueFileName);
        }
    }

    private IEnumerator RunCoroutines()
    {
        while (coroutines.Count > 0)
        {
            yield return StartCoroutine(coroutines.Dequeue());
            yield return new WaitForSeconds(1f);
            text.text = "";
            letterIndex = 0;
        }
    }
    private IEnumerator DisplayLetters(DialogueNode _introNode)
    {
        while (letterIndex < _introNode.text.Length)
        {
            char currentLetter = _introNode.text[letterIndex];
            text.text += currentLetter;

            yield return new WaitForSeconds(letterDelay);

            letterIndex++;
        }
    }
}
