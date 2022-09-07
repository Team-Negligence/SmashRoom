using UnityEngine;
using System.Collections.Generic;
using UnityEngine.XR;

/// <summary>
/// Spawn an object at a transform's position
/// </summary>
public class SpawnObject : MonoBehaviour
{
    [Tooltip("The object that will be spawned")]
    public GameObject originalObject = null;

    [Tooltip("The transform where the object is spanwed")]
    public Transform parent = null;

    public int numberOfItems;

    // Position for where you want it to be spawned.
    public float x;
    public float y;
    public float z;

    // Gets the input devices.
    private List<InputDevice> devices = new List<InputDevice>();
    private InputDevice targetDevice;

    public void Start()
    {
        // Test code to make sure that the spawning works.
        /*for (int i  = 0; i < numberOfItems; i++)
        {
            Vector3 position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Instantiate(originalObject, position, parent.rotation, parent); 
            Debug.Log("Start");
        }
        */

        // The device that will be our main input. This assumes that 
        // we are only using one device.
        if (devices.Count > 0) {
            targetDevice = devices[0];
        }
    }

    private void OnValidate()
    {
        if (!parent)
            parent = transform;
    }

    private void Update()
    {
        // This input needs to be replaced with an input from the button.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
            Debug.Log("Input is good");
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            Spawn();
            Debug.Log("Keyboard B button is good");
        }

        // This corresponds to X or A button. Needs testing to make sure which one it is.
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue)) 
        {
                Spawn();
                Debug.Log("Controller primary button input is good");
        }
        
    }

    private void Spawn()
    {
        // We don't randomise the y value because we don't want the can to float.
        // Position was randomised but needs to be changed to something more specific.      
        Vector3 position = new Vector3(Random.Range(x - 0.2f, x + 0.2f), y, Random.Range(z - 0.2f, z + 0.2f));
        Instantiate(originalObject, position, parent.rotation, parent); 
    }
}
