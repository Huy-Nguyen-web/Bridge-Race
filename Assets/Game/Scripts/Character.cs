using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject brickMeshPrefab;
    public EndLevel endLevel;
    public Animator animator;
    public GameObject characterModel;
    public GameObject characterMesh;
    public CharacterController controller;
    public float speed = 10.0f;
    public ColorData.ColorType characterColor;
    public List<GameObject> collectedBrick = new List<GameObject>();
    public List<GameObject> brickMeshStack = new List<GameObject>();
    public LayerMask stairLayer;
    public ColorData colorData;
    public int currentLevel = 1;
    public bool gameEnd;
    public bool gotHit;
    public bool isWin;
    private void Awake() {
        endLevel = FindObjectOfType<EndLevel>();
        isWin = false;
    }
    private void Start() {
        OnInit();
        gameEnd = false;
    }
    public virtual void OnInit(){

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
        if(other.CompareTag(Constant.TAG_STAIR)){
            BuildStair(other);
        } else if(other.CompareTag(Constant.TAG_BRICK)){
            if(this.characterColor == other.GetComponent<Brick>().brickColor){ 
                AddBrick(other);
            }
        }
    }
    // private void OnCollisionEnter(Collision other) {
    //     if(other.transform.CompareTag(Constant.TAG_BRICKMESH)){
    //         BrickMeshBehaviour brickMeshBehaviour = other.transform.GetComponent<BrickMeshBehaviour>();
    //         if(brickMeshBehaviour == null) return;
    //         Debug.Log(characterColor == brickMeshBehaviour.brickColor);
    //         if(characterColor == brickMeshBehaviour.brickColor){
    //             RestoreBrick(other);
    //         }
    //     }
    // }
    private void AddBrick(Collider other){
        collectedBrick.Add(other.gameObject);
        other.gameObject.SetActive(false);
        GameObject brickMesh = Instantiate(brickMeshPrefab, characterModel.transform);
        // brickMesh.GetComponent<BrickMeshBehaviour>().brickColor = this.characterColor;
        brickMesh.transform.localPosition = new Vector3(0, 0.3f * brickMeshStack.Count, -0.5f);
        brickMesh.GetComponent<MeshRenderer>().material = colorData.GetColor(characterColor);
        brickMeshStack.Add(brickMesh);
    }
    private void RestoreBrick(Collision other){
        GameObject.Destroy(other.gameObject);
        GameObject brickMesh = Instantiate(brickMeshPrefab, characterModel.transform);
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
        ColorData.ColorType color = (ColorData.ColorType) colorNum;
        GetComponent<MeshRenderer>().material = colorData.GetColor(color);
        characterMesh.GetComponent<SkinnedMeshRenderer>().material = colorData.GetColor(color);
        characterColor = color;
    }
    public void ClearBrick(){
        foreach(GameObject brickMesh in brickMeshStack){
            GameObject.Destroy(brickMesh);
        }
        brickMeshStack.Clear();
        collectedBrick.Clear();
    }
    public void ThrowBrick(){
        // After character got hit, he'll lost the brick behind his back
        // foreach(GameObject brickMesh in brickMeshStack){
        //     BoxCollider brickMeshCollider = brickMesh.GetComponent<BoxCollider>();
        //     Rigidbody brickMeshRigidBody = brickMesh.GetComponent<Rigidbody>();

        //     brickMeshRigidBody.isKinematic = false;
        //     brickMeshCollider.enabled = true;
        //     brickMesh.transform.parent = null;
        // }
        // brickMeshStack.Clear();
    }
    public void EndGame(Vector3 endLevelPosition){
        ClearBrick();
        gameEnd = true;
        if(isWin){
            animator.SetTrigger(Constant.TRIGGER_WINNING);
        }
        if(GetComponent<NavMeshAgent>() != null){
            GetComponent<NavMeshAgent>().enabled = false;
        }
        if (GetComponent<CharacterController>() != null){
            GetComponent<CharacterController>().enabled = false;
        }
    }
    public void CharacterGotHit(){
        if(!gotHit){
            ThrowBrick();
            if(GetComponent<NavMeshAgent>() != null){
                GetComponent<NavMeshAgent>().enabled = false;
            }
            if(GetComponent<CharacterController>() != null){
                GetComponent<CharacterController>().enabled = false;
            }
            gotHit = true;
            animator.SetTrigger(Constant.TRIGGER_FALLING);
            Invoke(nameof(CharacterGotUp), 3.5f);
        }
    }
    public void CharacterGotUp(){
        Enemy enemyScript = GetComponent<Enemy>();
        if(GetComponent<NavMeshAgent>() != null){
            GetComponent<NavMeshAgent>().enabled = true;
        }
        if(GetComponent<CharacterController>() != null){
            GetComponent<CharacterController>().enabled = true;
        }
        if(enemyScript != null){
            enemyScript.SwitchState(enemyScript.currentState);
        }
        gotHit = false;
    }
}
