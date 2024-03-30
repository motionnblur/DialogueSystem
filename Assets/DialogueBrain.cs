using System.Collections;
using UnityEngine;

public class DialogueBrain : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text text;
    [SerializeField] private TextAsset dialogueFileName;
    public string fullText; // The complete dialogue text
    public float letterDelay = 0.2f; // Delay between displaying words
    private int letterIndex = 0;

    private void Start()
    {
        //StartCoroutine(DisplayLetters());
        TextAsset jsonFile = dialogueFileName;
        if (jsonFile != null)
        {
            DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(jsonFile.text);
            DialogueNode introNode = FindNodeById(dialogueData, "intro");
            if(introNode != null)
            {
                Debug.Log("Intro text: " + introNode.text);
            }
        }
        else
        {
            Debug.LogError("JSON file not found: " + dialogueFileName);
        }
    }

    private IEnumerator DisplayLetters()
    {
        while (letterIndex < fullText.Length)
        {
            char currentLetter = fullText[letterIndex];
            text.text += currentLetter;

            yield return new WaitForSeconds(letterDelay);

            letterIndex++;
        }
    }

    private DialogueNode FindNodeById(DialogueData data, string nodeId)
    {
        foreach (var node in data.player)
        {
            if (node.id == nodeId)
            {
                return node;
            }
        }
        return null;
    }
}
[System.Serializable]
public class DialogueData
{
    public DialogueNode[] player;
}
[System.Serializable]
public class DialogueNode
{
    public string id;
    public string text;
    public DialogueOption[] options;
}
[System.Serializable]
public class DialogueOption
{
    public string text;
    public string nextNode;
}
