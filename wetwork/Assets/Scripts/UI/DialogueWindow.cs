using NUnit.Framework.Internal.Commands;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class DialogueWindow : MonoBehaviour
{
    public string[] lines;
    public float textSpeed;
    private TextMeshProUGUI text;
    int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = string.Empty;
        StartDialogue();
    }


    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
