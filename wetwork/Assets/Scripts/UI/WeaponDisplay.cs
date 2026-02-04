using TMPro;
using UnityEngine;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] string type;
    private Weapon weapon;
    private TextMeshProUGUI text;

    private void Start()
    {
        weapon = PlayerState.Weapons.Find(t => t.GetType().ToString() == type);
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = weapon.Ammo().ToString();
    }
}
