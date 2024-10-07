using UnityEngine;

public class Projectile : MonoBehaviour
{
   public Rigidbody rb;

   private void Start(){
      rb = GetComponent<Rigidbody>();
      rb.AddRelativeForce(Vector3.forward * 500);
      Destroy(transform.gameObject, 1f);

   }
   
}
