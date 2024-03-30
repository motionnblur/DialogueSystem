using System.Collections;
using UnityEngine;

public class DialogueBrain : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text text;
    public string fullText; // The complete dialogue text
    public float letterDelay = 0.2f; // Delay between displaying words
    private int letterIndex = 0;

    private void Start()
    {
        StartCoroutine(DisplayLetters());
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
}
