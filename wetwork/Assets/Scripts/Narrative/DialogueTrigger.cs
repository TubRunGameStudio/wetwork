using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset text;
    DialogueWindow window;

    private void Start()
    {
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        window = controller.DIALOGUE_WINDDOW;

    }

    public void StartDialogue()
    {
        Dialogue dialogue = JsonUtility.FromJson<Dialogue>(text.text);
        window.StartDialogue(dialogue);
    }
}
