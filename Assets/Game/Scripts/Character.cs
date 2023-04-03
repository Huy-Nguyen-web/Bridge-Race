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
        if(collectedBrick[collectedBrick.Count - 1].transform.parent.name == "BrickSpawnerLevel" + currentLevel.ToString()){
            collectedBrick[collectedBrick.Count - 1].SetActive(true);
        }
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
        ClearBrick();
        gameEnd = true;
        if(GetComponent<NavMeshAgent>() != null){
            GetComponent<NavMeshAgent>().enabled = false;
        }
        if (GetComponent<CharacterController>() != null){
            GetComponent<CharacterController>().enabled = false;
        }
    }
    // private void OnControllerColliderHit(ControllerColliderHit hit) {
    //     if(hit.transform.tag == "Character"){
    //         Debug.Log(hit.transform.name);
    //         if(hit.transform.GetComponent<Character>().collectedBrick.Count > collectedBrick.Count){
    //             // If other player has more brick than this player,
    //             // this player need to be stun and thrown the brick
    //             CharacterGotHit();
    //             Invoke(nameof(CharacterGotUp), 1f);
    //         }
    //     }
    // }
    // private void OnCollisionEnter(Collision other) {
    //     if(other.transform.tag == "Character"){
    //         if(other.transform.GetComponent<Character>().collectedBrick.Count > collectedBrick.Count){
    //             // If other player has more brick than this player,
    //             // this player need to be stun and thrown the brick
    //             CharacterGotHit();
    //             Invoke(nameof(CharacterGotUp), 1f);
    //         }
    //     }
    // }
    public void CharacterGotHit(){
        Debug.Log(transform.name + " got hit");
        if(GetComponent<NavMeshAgent>() != null){
            GetComponent<NavMeshAgent>().enabled = false;
        }
        if(GetComponent<CharacterController>() != null){
            GetComponent<CharacterController>().enabled = false;
        }
        GetComponent<Rigidbody>().isKinematic = false;

        Invoke(nameof(CharacterGotUp), 1f);
    }
    public void CharacterGotUp(){
        if(GetComponent<NavMeshAgent>() != null){
            GetComponent<NavMeshAgent>().enabled = true;
        }
        if(GetComponent<CharacterController>() != null){
            GetComponent<CharacterController>().enabled = true;
        }
        GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(GetComponent<Rigidbody>().isKinematic);
        Debug.Log(transform.name + " got up");
    }
}
