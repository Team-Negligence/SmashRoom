using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionChecker : MonoBehaviour
{
    // Used if collided object has no lethality value
    private static readonly float DEF_LETHALITY = 1f;

    // This will be multiplied by the relative velocity to determine how much health a hit from this object should deal
    public float lethality;

    // Multiplied by the fall time to determine the damage to be dealt
    public float fall_sensitivity;

    // If less than this amount of damage is taken, the event is ignored
    public float damage_threshold;

    // Total amount of health the object has. If 0, object is considered unbreakable
    public float health;

    // Object to replace this object when health reaches 0
    // If this is null, object is just removed instead
    public GameObject destroyedVersion;

    // used for falld damage
    private float fallTime = 0;
    private bool hasFallen = false;
    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Register listeners for when item is grabbed/dropped
        var interactor = GetComponent<XRDirectInteractor>();
        interactor.selectEntered.AddListener(interactable =>
        {
            isGrabbed = true;
        });
        interactor.selectExited.AddListener(interactable =>
        {
            isGrabbed = false;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    // On collision.
    void OnCollisionEnter(Collision obj) 
    {
        // if health is 0 the object is considered unbreakable (mostly used for weapons)
        if (this.health > 0)
        {
            ApplyCollisionDamage(obj.relativeVelocity, obj.gameObject);
        }
    }

    void UpdateFallDamage()
    {
        // If the velocity is negative, the object is either falling, or moving down in the player's hands
        if (GetComponent<Rigidbody>().velocity.y < 0 && !isGrabbed)
        {
            // Increase fall time and mark as having fallen
            fallTime += Time.deltaTime;
            hasFallen = true;
        }
        else if (hasFallen)
        {
            // No longer falling, take damage
            var damage = fallTime * fall_sensitivity;

            if (damage >= damage_threshold)
            {
                health -= damage_threshold;
            }

            // Reset fall measurements
            hasFallen = false;
            fallTime = 0;
        }
    }

    public void ApplyCollisionDamage(Vector3 collisionVelocity, GameObject collisionObject)
    {
        CollisionChecker collisionObjectCC = collisionObject.GetComponent<CollisionChecker>();
        float collisionObjectLethality = collisionObjectCC != null ? collisionObjectCC.lethality : DEF_LETHALITY;

        var damage = collisionVelocity.magnitude * collisionObjectLethality;

        if (damage > damage_threshold)
        {
            this.health = this.health - damage;
        }

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
