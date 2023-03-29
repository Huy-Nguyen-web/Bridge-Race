using UnityEngine;

[CreateAssetMenu(fileName = "ColorType", menuName = "Scriptable Object/Color Type")]
public class ColorData : ScriptableObject
{
    [SerializeField] private Material[] materials;
    public enum ColorType { Red = 0, Green = 1, Blue = 2, Yellow = 3 }
    public Material GetColor(ColorType color){
        return materials[(int)color];
    }
}