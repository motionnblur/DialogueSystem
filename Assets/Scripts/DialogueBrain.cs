using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBrain : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] Button[] dialogButtons;
    [SerializeField] UnityEngine.UI.Text textArea;
    [SerializeField] private TextAsset dialogueFileName;
    public float letterDelay = 0.2f; // Delay between displaying words
    private int letterIndex = 0;
    private Queue<IEnumerator> coroutines = new Queue<IEnumerator>();
    private DialogueBase dialogueBase;
    private DialogueNode currentIntroNode;
    
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
            //coroutines.Enqueue(DisplayLetters(dialogueBase.FindNodeById("continueExploring")));
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
        currentIntroNode = _introNode;
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
        int optionCount = _introNode.options.Length;
        if(optionCount > 0){
            dialogueBox.SetActive(true);
            for(int i = 0; i < optionCount; i++){
                dialogButtons[i].gameObject.SetActive(true);
                dialogButtons[i].GetComponentInChildren<Text>().text = _introNode.options[i].text;
            }
        }
    }

    public void DialogA(){
        coroutines.Enqueue(DisplayLetters(dialogueBase.FindNodeById(currentIntroNode.options[0].nextNode)));
        StartCoroutine(RunCoroutines());
        dialogueBox.SetActive(false);
    }
    public void DialogB(){
        coroutines.Enqueue(DisplayLetters(dialogueBase.FindNodeById(currentIntroNode.options[1].nextNode)));
        StartCoroutine(RunCoroutines());
        dialogueBox.SetActive(false);
    }
    public void DialogC(){
        coroutines.Enqueue(DisplayLetters(dialogueBase.FindNodeById(currentIntroNode.options[2].nextNode)));
        StartCoroutine(RunCoroutines());
        dialogueBox.SetActive(false);
    }
    public void DialogD(){
        coroutines.Enqueue(DisplayLetters(dialogueBase.FindNodeById(currentIntroNode.options[3].nextNode)));
        StartCoroutine(RunCoroutines());
        dialogueBox.SetActive(false);
    }
}
