using System.Collections;
using System.Linq;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    public enum Type
    {
        Arrow,
        Stone,
        Canon,
        Gun
    }

    public Type type;

    public Projectile arrow, stone, canon, gun;
    public Projectile chosenProjectile;




    [Header("Stats")]
    public int health;
    public int damage;
    [Range(1f, 3f)]
    public float attackSpeed;
    [Range(2f, 10f)]
    public float detectionRadius;
    [Range(2f, 10f)]
    public float attackRadius;

    [Header("UI")]
    public LayerMask enemyLayerMask;
    public int cost;

    public Enemy currentEnemy;
    public float attackSpeedTimer;

    public Transform firePos;
    public bool canFire = true;



    private void Awake()
    {
        attackSpeedTimer = attackSpeed;
    }

    private void OnEnable()
    {
        switch (type)
        {
            case Type.Arrow:
                chosenProjectile = arrow;
                break;
            case Type.Canon:
                chosenProjectile = canon;
                break;
            case Type.Gun:
                chosenProjectile = gun;
                break;
            case Type.Stone:
                chosenProjectile = stone;
                break;
            default:
                chosenProjectile = arrow;
                break;
        }
    }

    void FixedUpdate()
    {
        if (currentEnemy != null)
        {
            Attack();
            FindTarget();

        }
        else
        {
            FindTarget();
        }
    }


    public void FindTarget()
    {
        var cols = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);
        foreach (var col in cols)
        {
            // if (col.gameObject.TryGetComponent<BaseUnit>(out currentTargetUnit))
            // {
            if (col.gameObject.TryGetComponent<Enemy>(out currentEnemy))
            {
                Debug.Log(currentEnemy.gameObject.name);
            }
            //}
        }


        if(cols.Count() == 0){
            currentEnemy = null;
        }


    }

    public void Attack()
    {

       
       
            //transform.LookAt(currentEnemy.transform.position);
            //  Quaternion rot = Quaternion.LookAt(currentEnemy.transform.position);
            // transform.rotation = rot;
            //Projectile projectile = Instantiate(chosenProjectile, firePos.position, Quaternion.identity);

           
            var targetRotation = Quaternion.LookRotation(currentEnemy.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.fixedDeltaTime);
            if(canFire == true){
                 
                    Projectile proj = Instantiate(chosenProjectile, firePos.position, transform.rotation);
                    canFire = false;
                    StartCoroutine(WaitToFire());
            }
            // Smoothly rotate towards the target point.
         
        
    }

    private IEnumerator WaitToFire(){
        yield return new WaitForSeconds(2f);
        canFire = true;
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
