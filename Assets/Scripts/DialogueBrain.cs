using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBrain : MonoBehaviour
{
    Queue<IEnumerator> coroutines = new Queue<IEnumerator>();
    [SerializeField] UnityEngine.UI.Text textArea;
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
            textArea.text = "";
            letterIndex = 0;
        }
    }
    private IEnumerator DisplayLetters(DialogueNode _introNode)
    {
        foreach (string text in _introNode.texts){
            while (letterIndex < text.Length)
            {
                char currentLetter = text[letterIndex];
                textArea.text += currentLetter;

                yield return new WaitForSeconds(letterDelay);

                letterIndex++;
            }
            yield return new WaitForSeconds(0.8f);

            letterIndex = 0;
            textArea.text = "";
        }
    }
}
