using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10.0f;
    public ColorData.ColorType characterColor;
    public List<GameObject> collectedBrick = new List<GameObject>();
    public LayerMask stairLayer;
    public ColorData colorData;
    private void Start() {
        OnInit();
    }
    public virtual void OnInit(){
        // ChangeColor();
    }
    public void ReturnBrick(){
        collectedBrick[collectedBrick.Count - 1].SetActive(true);
        collectedBrick.RemoveAt(collectedBrick.Count - 1);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Stair"){
            BuildStair(other);
        } else if(other.tag == "Brick"){
            if(this.characterColor == other.GetComponent<Brick>().brickColor){ 
                collectedBrick.Add(other.gameObject);
                other.gameObject.SetActive(false);
            }
        }
    }
    void BuildStair(Collider stair){
        Stair stairScript = stair.gameObject.GetComponent<Stair>();
        Material stairMaterial = stair.gameObject.GetComponent<MeshRenderer>().material;
        if(collectedBrick.Count > 0 && (stairScript.stairColor != characterColor || stairMaterial == stairScript.defaultMaterial)){
            stair.gameObject.GetComponent<Stair>().ChangeColor(characterColor);
            stair.gameObject.GetComponent<MeshRenderer>().enabled = true;
            ReturnBrick();
        }
    }
    public void ChangeColor(int colorNum){
        // int randomColorInt = Random.Range(0, 4);
        ColorData.ColorType color = (ColorData.ColorType) colorNum;
        GetComponent<MeshRenderer>().material = colorData.GetColor(color);
        characterColor = color;
    }
}
