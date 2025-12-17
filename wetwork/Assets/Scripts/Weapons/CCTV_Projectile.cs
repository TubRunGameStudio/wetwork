using UnityEngine;

public class CCTV_Projectile : MonoBehaviour
{
    [SerializeField] private GameObject cutout;
    public void Activate()
    {
        cutout.SetActive(true);
    }
}
