using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState 
{
    public abstract void OnStart(Enemy enemy);
    public abstract void OnUpdate(Enemy enemy);
}
