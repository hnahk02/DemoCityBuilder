using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public enum Type
{
    Tower1,
    Tower2,
}

public class Tower : BaseUnit, IUnit
{

    public Type type;
    public Projectile projectilePrefab;
    public Transform firePos;
    float attackSpeedTimer;

    protected override void Start()
    {

        base.Start();
        attackSpeedTimer = attackSpeed;
    }

    protected override void FixedUpdate()
    {
        Debug.Log("Tower");
        if(state == StateMachine.Idle){
            FindTarget();
        }else{
            Fight();
        }
    }

    protected override void Fight()
    {
        base.Fight();
        if (currentTargetUnit != null)
        {
            attackSpeedTimer -= Time.fixedDeltaTime;
            if (attackSpeedTimer < 0)
            {
                Debug.Log($"{this.gameObject.name} hit {damage} damage to {currentTargetUnit.name}");
                Projectile prj = Instantiate(projectilePrefab, firePos.transform.position, Quaternion.identity);
                //prj.target = currentTargetUnit.transform;
                prj.gameObject.SetActive(true);
                currentTargetUnit.health -= damage;
                attackSpeedTimer = attackSpeed;
            }


            if (currentTargetUnit.health < 0)
            {
                Destroy(currentTargetUnit.gameObject);
                Debug.Log($"{currentTargetUnit.gameObject.name} is dead!!");
                state = StateMachine.Idle;
            }
        }
    }
}
