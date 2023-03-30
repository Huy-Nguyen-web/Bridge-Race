using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] private LayerMask groundLayer;
    private NavMeshAgent navMeshAgent;
    private List<Brick> bricks = new List<Brick>();
    private GameObject brickSpawner;
    public int currentLevel = 1;
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        CreateBrickList();
        MoveToBrick();
    }
    private Vector3 GetBrickPosition(){
        for (int i = 0; i < 100; i++){
            int randomBrick = Random.Range(0, bricks.Count - 1);
            if(bricks[randomBrick].brickColor == characterColor && bricks[randomBrick].gameObject.active == true){
                return bricks[randomBrick].transform.position;
            }
        }
        return transform.position;
    }
    private Vector3 GetClosestBrickPosition(){
        Vector3 closestBrickPosition = transform.position;
        float distanceToClosestBrick = 100000000f;
        for (int i = 0; i < bricks.Count; i++){
            if(bricks[i].gameObject.active && bricks[i].brickColor == characterColor){
                float distanceToBrick = Vector3.Distance(transform.position, bricks[i].transform.position);
                if(distanceToClosestBrick == null) distanceToClosestBrick = distanceToBrick;
                if(distanceToBrick < distanceToClosestBrick){
                    distanceToClosestBrick = distanceToBrick;
                    closestBrickPosition = bricks[i].transform.position;
                }
            }
        }
        return closestBrickPosition;
    }
    private void Update() {
        if(collectedBrick.Count == 0){
            MoveToBrick();
        }
        if(navMeshAgent.remainingDistance < 0.1f){
            if(collectedBrick.Count < 10){
                Debug.Log(characterColor + ": Find new brick");
                MoveToBrick();
            }else{
                Debug.Log(characterColor + ": Build the bridge");
                MoveToNextLevel();
            }
        }
    }
    public void MoveToBrick(){
        CreateBrickList();
        navMeshAgent.SetDestination(GetBrickPosition());
    }
    private void MoveToNextLevel(){
        int nextLevel = currentLevel + 1;
        // GameObject nextGroundLevel = GameObject.Find("GroundLevel" + nextLevel.ToString());
        // bool foundNextGroundLevel = false;
        RaycastHit nextGroundLevel;
        Vector3 targetPosition = transform.position;
        for (int i = 0; i < 100; i++){
            if(Physics.Raycast(transform.position + Vector3.forward * i + new Vector3(0, 1000f, 0), Vector3.down, out nextGroundLevel, Mathf.Infinity, groundLayer)){
                if(nextGroundLevel.transform.name == "GroundLevel" + nextLevel.ToString()){
                    targetPosition = nextGroundLevel.point;
                    break;
                }
            }
        }
        navMeshAgent.SetDestination(targetPosition);
        // navMeshAgent.SetDestination(new Vector3(-10.9f, nextGroundLevel.transform.position.y, 41));
    }
    public void CreateBrickList(){
        bricks.Clear();
        brickSpawner = GameObject.Find("BrickSpawnerLevel" + currentLevel.ToString()); 
        if (brickSpawner != null){
            for(int i = 0; i < brickSpawner.transform.childCount - 1; i++){
                Brick brick = brickSpawner.transform.GetChild(i).GetComponent<Brick>();
                bricks.Add(brick);
            }
        }
    }
}