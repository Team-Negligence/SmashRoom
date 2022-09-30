using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    // Used if collided object has no lethality value
    private static readonly float DEF_LETHALITY = 1f;

    // This will be multiplied by the relative velocity to determine how much health a hit should take
    public float lethality;

    // Total amount of health the object has. If 0, object is considered unbreakable
    public float health;

    // Object to replace this object when health reaches 0
    // If this is null, object is just removed instead
    public GameObject destroyedVersion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // On collision.
    void OnCollisionEnter(Collision obj) 
    {
        // if health is 0 the object is considered unbreakable
        if (this.health > 0)
        {
            ApplyCollisionDamage(obj.relativeVelocity, obj.gameObject);
        }
    }

    public void ApplyCollisionDamage(Vector3 collisionVelocity, GameObject collisionObject)
    {
        CollisionChecker collisionObjectCC = collisionObject.GetComponent(typeof(CollisionChecker)) as CollisionChecker;
        float collisionObjectLethality = collisionObjectCC != null ? collisionObjectCC.lethality : DEF_LETHALITY;

        this.health = this.health - collisionVelocity.magnitude * collisionObjectLethality;

        Debug.Log("V:" + collisionVelocity.magnitude.ToString() + "; L:" + collisionObjectLethality.ToString() + "; H:" + this.health.ToString());

        if (this.health <= 0)
        {
            this.health = 0;
            DestroyCurrentObject();
        }
    }

    // Replace the current object with the destroyed version (or just delete current version if no destroyed version)
    private void DestroyCurrentObject()
    {
        if (destroyedVersion != null)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
