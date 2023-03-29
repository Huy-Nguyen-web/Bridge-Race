using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    public ColorData.ColorType brickColor;
    private void Awake() {
        ChangeColor();
    }
    public void ChangeColor(){
        int randomColorInt = Random.Range(0, 4);
        ColorData.ColorType color = (ColorData.ColorType) randomColorInt;
        GetComponent<MeshRenderer>().material = colorData.GetColor(color);
        brickColor = color;
    }
}
