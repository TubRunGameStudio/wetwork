using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] GameObject heart;

    private int health;
    private HorizontalLayoutGroup group;


    private void Awake()
    {
        group = GetComponent<HorizontalLayoutGroup>();
    }
    public void setHealth(int playerHealth)
    {
        health = playerHealth;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < health; i++)
        {
            GameObject addHeart = GameObject.Instantiate(heart);
            addHeart.gameObject.transform.SetParent(group.transform);
        }
    }
}
