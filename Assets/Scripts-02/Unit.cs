using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Unit : MonoBehaviour
{


     public Dictionary<Unit, int> UnitLevelDict = new Dictionary<Unit, int>();


     public enum CostType
     {
          One,
          Two,
          Three,
          Four,
          Five
     }

     public enum StateMachine
     {
          Idle,
          Walk,
          Fight
     }

     public HQBuilding HQBuilding;

     public bool isEnemy;

     public CostType costType;
     public StateMachine state;

     public int enemyLayerMaskIndex;

     [Range(0f, 5f)]
     public float maxDistance;

     public Unit currentTargetUnit;

     [Header("Stats")]
     public int health;

     [Range(0f, 2f)]
     public float attackSpeed;

     [Range(0f, 10f)]
     public float speed;

     public int damage;

     public bool canAttackHQ;

     public LayerMask EnemyLayerMask;

     public Transform firePos;

     public Projectile projectilePrefab;


     private void Awake()
     {
          attackSpeedTimer = attackSpeed;



     }

     private void Start()
     {
          if (isEnemy)
          {
               HQBuilding = GameRuntime.Instance.playerBuilding;
          }
          else
          {
               HQBuilding = GameRuntime.Instance.enemyBuilding;
          }
          state = StateMachine.Walk;

     }


     private void FixedUpdate()
     {
          switch (state)
          {
               case StateMachine.Idle:
                    if (UnityEngine.Random.Range(0f, 1f) > 0.5 ? true : false)
                    {
                         state = StateMachine.Walk;
                    }
                    break;
               case StateMachine.Walk:
                    if (currentTargetUnit == null) Wander();
                    else WalkToTarget();
                    break;
               case StateMachine.Fight:
                    Fight();
                    if (currentTargetUnit == null) state = StateMachine.Walk;
                    break;

               default:
                    break;
          }
     }




     [SerializeField] private float enemyDetectionRadius = 2f;
     [SerializeField] private float fightRangeRadius = 1f;

     public void SetCostType(int id)
     {
          if (id == 1) costType = CostType.One;
          if (id == 2) costType = CostType.Two;
          if (id == 3) costType = CostType.Three;
     }

     public void FindTarget()
     {
          var cols = Physics.OverlapSphere(transform.position, enemyDetectionRadius, EnemyLayerMask);
          foreach (var col in cols)
          {
               if (col.gameObject.GetComponent<Unit>() != null)
               {
                    currentTargetUnit = col.gameObject.GetComponent<Unit>();
                    state = StateMachine.Walk;
               }

               if(col.gameObject.GetComponent<HQBuilding>() != null)
               {
                    canAttackHQ = true;
               }
          }


     }

     float attackSpeedTimer;


     public void Fight()
     {
          if (currentTargetUnit != null)
          {
               attackSpeedTimer -= Time.fixedDeltaTime;
               if (attackSpeedTimer < 0)
               {
                    Debug.Log($"{this.gameObject.name} hit {damage} damage to {currentTargetUnit.name}");
                    Projectile prj = Instantiate(projectilePrefab, firePos.transform.position, Quaternion.identity);
                    prj.target = currentTargetUnit.transform;
                    prj.gameObject.SetActive(true);
                    currentTargetUnit.health -= damage;
                    attackSpeedTimer = attackSpeed;
               }

               if (currentTargetUnit.health < 0)
               {
                    Debug.Log($"{currentTargetUnit.name} is dead !!");
                    Destroy(currentTargetUnit.gameObject);
               }

          }else{
               
          }
     }

     public void WalkToTarget()
     {
          if (currentTargetUnit != null)
          {
               transform.position = Vector3.MoveTowards(transform.position, currentTargetUnit.transform.position, speed * Time.deltaTime);
               var cols = Physics.OverlapSphere(transform.position, fightRangeRadius, EnemyLayerMask);
               foreach (var col in cols)
               {
                    if (col.gameObject.GetComponent<Unit>() != null)
                    {
                         currentTargetUnit = col.gameObject.GetComponent<Unit>();
                         state = StateMachine.Fight;
                    }
               }

          }
     }

     public void Wander()
     {
          FindTarget();
          transform.position = Vector3.MoveTowards(transform.position, HQBuilding.transform.position, speed * Time.fixedDeltaTime);
     }
     private void OnDrawGizmos()
     {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(transform.position, enemyDetectionRadius);
          Gizmos.color = Color.green;
          Gizmos.DrawWireSphere(transform.position, fightRangeRadius);
     }

     private void OnTriggerEnter(Collider other)
     {
          if (other.gameObject.layer == 10)
          {
               Destroy(other.gameObject);
          }
     }

     





}
