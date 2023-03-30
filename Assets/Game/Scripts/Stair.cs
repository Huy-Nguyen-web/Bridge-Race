using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    public ColorData.ColorType stairColor;
    public Material defaultMaterial;
    void Start(){
        defaultMaterial = GetComponent<MeshRenderer>().material;
    }
    public void ChangeColor(ColorData.ColorType color){
        GetComponent<MeshRenderer>().material = colorData.GetColor(color);
        stairColor = color;
    }
}
