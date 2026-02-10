using System.Collections.Generic;
using UnityEngine;

public class Portait : MonoBehaviour
{
    [SerializeField] private Sprite bolsa;
    [SerializeField] private Sprite nestor;

    public Dictionary<string, Sprite> portraits;
    
    void Start()
    {
        gameObject.SetActive(false);

        portraits = new Dictionary<string, Sprite>();
        portraits.Add("Bolsa", bolsa);
        portraits.Add("Nestor", nestor);
    }

}
