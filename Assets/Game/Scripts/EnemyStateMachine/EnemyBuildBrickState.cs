using UnityEngine;

public class EnemyBuildBrickState : EnemyBaseState
{
    public override void OnStart(Enemy enemy){
        MoveToNextLevel(enemy);
    }
    public override void OnUpdate(Enemy enemy){
        if(enemy.brickMeshStack.Count == 0){
            enemy.SwitchState(enemy.SeekBrickState);
        }
    }
    private void MoveToNextLevel(Enemy enemy){
        int nextLevel = enemy.currentLevel + 1;
        RaycastHit nextGroundLevel;
        Vector3 targetPosition = enemy.transform.position;
        for (int i = 0; i < 100; i++){
            if(Physics.Raycast(enemy.transform.position + Vector3.forward * i + new Vector3(0, 1000f, 0), Vector3.down, out nextGroundLevel, Mathf.Infinity, enemy.groundLayer)){
                if(nextGroundLevel.transform.name == "GroundLevel" + nextLevel.ToString()){
                    targetPosition = nextGroundLevel.point;
                    break;
                }
            }
        }
        enemy.navMeshAgent.SetDestination(targetPosition);
    }
}
