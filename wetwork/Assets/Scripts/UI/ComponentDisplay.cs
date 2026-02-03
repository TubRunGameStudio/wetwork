using TMPro;
using UnityEngine;
using static PlayerInventory;

public class ComponentDisplay : MonoBehaviour
{
    [SerializeField] ComponentType component;
    private PlayerInventory inventory;
    private TextMeshProUGUI text;

    private void Start()
    {
        GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inventory = controller.player.GetComponent<PlayerInventory>();

        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = inventory.GetComponentCount(component).ToString();
    }
}
