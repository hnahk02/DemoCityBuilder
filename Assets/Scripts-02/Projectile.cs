using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   public Transform target;

   
   public void FixedUpdate(){
    
     if(target == null )
     {
        Destroy(this.gameObject);
     }
     else
     {
        this.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 5f * Time.fixedDeltaTime );
     }
   }
}
