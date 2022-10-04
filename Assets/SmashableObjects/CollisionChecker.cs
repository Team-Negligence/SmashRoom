using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class CollisionChecker : MonoBehaviour
{
    UnityEvent onObjectDamage;
    UnityEvent onObjectDestroy;

    private static readonly float DEF_LETHALITY = 1f;
    private static readonly float INVINCE_FRAME_TIME = 0.25f;

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

    // used for fall damage
    private float fallTime = 0;
    private bool hasFallen = false;
    private bool isGrabbed = false;

    // Damage ticks
    private bool isInvincible = false;
    private float invincibilityCounter = 0;

    // Sounds
    public List<AudioClip> damageSounds;
    public List<AudioClip> brokenSounds;

    public float damageVolume = 1.0F;
    public float breakVolume = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        // Events for when the object is damaged/destroyed
        if (onObjectDamage == null)
            onObjectDamage = new UnityEvent();
        if (onObjectDestroy == null)
            onObjectDestroy = new UnityEvent();

        // Register local event listeners
        onObjectDestroy.AddListener(() => { DestroyCurrentObject(); });
        onObjectDestroy.AddListener(() => { PlayRandomSound(brokenSounds, breakVolume); });

        onObjectDamage.AddListener(() => { PlayRandomSound(damageSounds, damageVolume); });

        // Register listeners for when item is grabbed/dropped
        var interactor = GetComponent<XRDirectInteractor>();
        if (interactor != null)
        {
            interactor.selectEntered.AddListener(interactable =>
            {
                isGrabbed = true;
            });
            interactor.selectExited.AddListener(interactable =>
            {
                isGrabbed = false;
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health > 0)
        {
            RefreshInvincibilityFrame();
            UpdateFallDamage();
        }
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
                TryDamage(damage);
            }

            // Reset fall measurements
            hasFallen = false;
            fallTime = 0;
        }
    }

    public void RefreshInvincibilityFrame()
    {
        if (isInvincible)
        {
            invincibilityCounter += Time.deltaTime;

            if (invincibilityCounter >= INVINCE_FRAME_TIME)
            {
                isInvincible = false;
                invincibilityCounter = 0;
            }
        }
    }

    // Attempt to add damage, prevented if within invincibility tick
    // returns true if damage was dealt, false otherwise
    public bool TryDamage(float damage)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            health -= damage;

            // Also detroy object if 0 health
            if (this.health <= 0)
            {
                this.health = 0;
                onObjectDestroy.Invoke();
            }

            onObjectDamage.Invoke();
            return true;
        }
        return false;
    }

    public void ApplyCollisionDamage(Vector3 collisionVelocity, GameObject collisionObject)
    {
        CollisionChecker collisionObjectCC = collisionObject.GetComponent<CollisionChecker>();
        float collisionObjectLethality = collisionObjectCC != null ? collisionObjectCC.lethality : DEF_LETHALITY;

        var damage = collisionVelocity.magnitude * collisionObjectLethality;

        if (damage > damage_threshold)
        {
            TryDamage(damage);
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

    private void PlayRandomSound(List<AudioClip> soundOptions, float volume)
    {
        AudioSource.PlayClipAtPoint(soundOptions[Random.Range(0, (soundOptions.Count - 1))], gameObject.transform.position, volume);
    }    
}
