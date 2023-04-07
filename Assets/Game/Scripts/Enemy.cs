using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public LayerMask groundLayer;
    public NavMeshAgent navMeshAgent;
    public List<Brick> bricks = new List<Brick>();
    public GameObject brickSpawner;
    public EnemyBaseState currentState;
    public EnemyBuildBrickState BuildBrickState = new EnemyBuildBrickState();
    public EnemySeekBrickState SeekBrickState = new EnemySeekBrickState();
    private void Start() {
        currentState = SeekBrickState;
        currentState.OnStart(this) ;
        endLevel.OnEndLevelAction += EndGame;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update() {
        if(gameEnd) return;
        if(gotHit) return;
        currentState.OnUpdate(this);
    }
    public void SwitchState(EnemyBaseState state){
        currentState = state;
        state.OnStart(this);
    }
}