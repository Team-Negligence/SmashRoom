using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoDespawn : MonoBehaviour
{
    public float maxDespawnTime;
    public float minDespawnTime;

    private float despawnTime;
    private float timeAlive = 0;

    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        despawnTime = Random.Range(maxDespawnTime, minDespawnTime);

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
        if (isGrabbed)
        {
            timeAlive = 0;
        } 
        else
        {
            timeAlive += Time.deltaTime;

            if (timeAlive > despawnTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
