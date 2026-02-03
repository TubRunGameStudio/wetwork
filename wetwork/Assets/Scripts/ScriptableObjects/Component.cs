using UnityEngine;

public enum ComponentType { N5, OpticLens, RadioPart, ProximityTrigger };


[CreateAssetMenu(fileName = "Component", menuName = "Scriptable Objects/Component")]
public class Component : ScriptableObject
{
    public ComponentType componentType;
    public Sprite sprite;
    public GameObject pickupPrefab;
}
