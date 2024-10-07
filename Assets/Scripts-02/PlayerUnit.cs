using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : BaseUnit
{
    private NavMeshAgent agent;

    public Transform destinationTransform;

    public Projectile projectilePrefab;
    public Transform firePos;

    float attackSpeedTimer;

    protected override void Start()
    {
        base.Start();
        attackSpeedTimer = attackSpeed;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.destination = destinationTransform.position;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isWalkToTarget == true)
        {
            agent.ResetPath();
            agent.isStopped = true;
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
                agent.isStopped = false;
                agent.destination = destinationTransform.position;
                isWalkToTarget = false;
            }
        }
        else
        {
            isWalkToTarget = false;
            state = StateMachine.Idle;
            agent.isStopped = false;
            agent.destination = destinationTransform.position;
            
        }

    }
}
