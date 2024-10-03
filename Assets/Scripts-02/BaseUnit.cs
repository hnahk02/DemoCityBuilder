using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum StateMachine
{
    Idle,
    Walk,
    Fight
}

public class BaseUnit : MonoBehaviour
{
    public StateMachine state;


    [Header("Stats")]
    public int health;
    public int damage;
    [Range(1f, 10f)]
    public float speed;
    [Range(1f, 3f)]
    public float attackSpeed;
    [Range(5f, 100f)]
    public float detectionRadius;
    [Range(5f, 100f)]
    public float attackRadius;

    [Header("UI")]
    public LayerMask enemyLayerMask;
    public int cost;

    public BaseUnit currentTargetUnit;

    public HQBuilding HQBuilding;

    public bool isWalkToTarget;



    protected virtual void Start()
    {
        state = StateMachine.Idle;
    }

    protected virtual void FixedUpdate()
    {
        switch (state)
        {
            case StateMachine.Idle:
                FindTarget();
                break;
            case StateMachine.Walk:
                WalkToTarget();
                break;
            case StateMachine.Fight:
                Fight();
                if (currentTargetUnit == null) state = StateMachine.Walk;
                break;

            default:
                break;
        }
    }


    public void FindTarget()
    {
        var cols = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);
        foreach (var col in cols)
        {
            // if (col.gameObject.TryGetComponent<BaseUnit>(out currentTargetUnit))
            // {
                if(col.gameObject.TryGetComponent<BaseUnit>(out currentTargetUnit)){
                     Debug.Log(currentTargetUnit.gameObject.name);
                    state = StateMachine.Walk;
                }
               
            //}
        }


    }

    protected virtual void Fight()
    {
        //Debug.Log("Fight");
    }


    public void WalkToTarget()
    {
        if (currentTargetUnit != null)
        {
            isWalkToTarget = true;
            transform.position = Vector3.MoveTowards(transform.position, currentTargetUnit.transform.position, speed * Time.deltaTime);
            var cols = Physics.OverlapSphere(transform.position,attackRadius, enemyLayerMask);
            foreach (var col in cols)
            {
                if (col.gameObject.GetComponent<BaseUnit>() != null)
                {
                    currentTargetUnit = col.gameObject.GetComponent<BaseUnit>();
                    state = StateMachine.Fight;
                }
            }

        }else{
            isWalkToTarget = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Destroy(other.gameObject);
        }
    }


}
