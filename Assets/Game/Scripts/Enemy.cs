using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
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
    private void Update() {
        if(collectedBrick.Count == 0){
            MoveToBrick();
        }
        if(navMeshAgent.remainingDistance < 0.1f){
            if(collectedBrick.Count < 10){
                MoveToBrick();
            }else{
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
        GameObject nextGroundLevel = GameObject.Find("GroundLevel" + nextLevel.ToString());
        navMeshAgent.SetDestination(nextGroundLevel.transform.position);
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