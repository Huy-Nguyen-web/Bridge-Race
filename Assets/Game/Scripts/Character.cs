using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    [SerializeField] private GameObject brickMeshPrefab;
    public CharacterController controller;
    public float speed = 10.0f;
    public ColorData.ColorType characterColor;
    public List<GameObject> collectedBrick = new List<GameObject>();
    public List<GameObject> brickMeshStack = new List<GameObject>();
    public LayerMask stairLayer;
    public ColorData colorData;
    public int currentLevel = 1;
    public bool gameEnd;
    private void Start() {
        OnInit();
        gameEnd = false;
    }
    public virtual void OnInit(){
        // ChangeColor();
    }

    public void ReturnBrick(){
        Debug.Log(currentLevel);
        if(collectedBrick[collectedBrick.Count - 1].transform.parent.name == "BrickSpawnerLevel" + currentLevel.ToString()){
            collectedBrick[collectedBrick.Count - 1].SetActive(true);
        }
        Debug.Log(collectedBrick[collectedBrick.Count - 1].transform.parent.name);
        collectedBrick.RemoveAt(collectedBrick.Count - 1);
        Destroy(brickMeshStack[brickMeshStack.Count - 1]);
        brickMeshStack.RemoveAt(brickMeshStack.Count - 1);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Stair"){
            BuildStair(other);
        } else if(other.tag == "Brick"){
            if(this.characterColor == other.GetComponent<Brick>().brickColor){ 
                AddBrick(other);
            }
        }
    }
    private void AddBrick(Collider other){
        collectedBrick.Add(other.gameObject);
        other.gameObject.SetActive(false);
        GameObject brickMesh = Instantiate(brickMeshPrefab, transform);
        brickMesh.transform.localPosition = new Vector3(0, 0.3f * brickMeshStack.Count, -0.5f);
        brickMesh.GetComponent<MeshRenderer>().material = colorData.GetColor(characterColor);
        brickMeshStack.Add(brickMesh);
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
    public void ClearBrick(){
        foreach(GameObject brickMesh in brickMeshStack){
            GameObject.Destroy(brickMesh);
        }
        brickMeshStack.Clear();
        collectedBrick.Clear();
    }
    public void EndGame(Vector3 endLevelPosition){
        Debug.Log("End Game");
        ClearBrick();
        gameEnd = true;
        if(GetComponent<NavMeshAgent>() != null){
            GetComponent<NavMeshAgent>().enabled = false;
        }
        if (GetComponent<CharacterController>() != null){
            GetComponent<CharacterController>().enabled = false;
        }
    }
}
