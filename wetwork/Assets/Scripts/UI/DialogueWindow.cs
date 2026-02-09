using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Dialogue;

public class DialogueWindow : MonoBehaviour
{
    public float textSpeed;
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = string.Empty;        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        List<string> collapsedLines = new();
        foreach (string line in dialogue.lines)
            collapsedLines.Add(line);
        StartCoroutine(StartDialogue(collapsedLines));
    }

    private IEnumerator StartDialogue(List<string> lines)
    {
        foreach(string line in lines)
            yield return StartCoroutine(TypeLine(line));
        text.text = string.Empty;
    }

    private IEnumerator TypeLine(string line)
    {
        text.text = string.Empty;
        foreach(char c in line.ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(1);
    }

}
