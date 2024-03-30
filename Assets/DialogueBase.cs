using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBase
{
    private DialogueData dialogueData;
    public DialogueBase(TextAsset jsonFile)
    {
        dialogueData = JsonUtility.FromJson<DialogueData>(jsonFile.text);
    }
    public DialogueNode FindNodeById(string nodeId)
    {
        foreach (var node in dialogueData.player)
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