using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialogue;

public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float textSpeed;
    private Portait portraits;
    private TextMeshProUGUI text;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = string.Empty;
        portraits = image.gameObject.GetComponent<Portait>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        StartCoroutine(ExecuteDialogue(dialogue));
    }

    private IEnumerator ExecuteDialogue(Dialogue dialogue)
    {
        foreach (Line line in dialogue.lines)
        {
            image.gameObject.SetActive(true);
            image.sprite = portraits.portraits.ContainsKey(line.character) ? portraits.portraits[line.character] : throw new Exception($"No sprite found for portrait {line.character}");
            yield return StartCoroutine(TypeLine(line.line));
            text.text = string.Empty;
            image.gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeLine(string line)
    {
        text.text = string.Empty;
        foreach(char c in line.ToCharArray())
        {
            text.text += c;
            if(char.IsWhiteSpace(c))
                yield return new WaitForSeconds(textSpeed);
            else
                yield return new WaitForSeconds(textSpeed/2);
        }
        yield return new WaitForSeconds(1);
    }

}
