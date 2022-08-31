using UnityEngine;

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

    public float x;
    public float y;
    public float z;

    public void Start()
    {
        // Test code to make sure that it works.
        /*for (int i  = 0; i < numberOfItems; i++)
        {
            Vector3 position = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Instantiate(originalObject, position, parent.rotation, parent); 
            Debug.Log("Start");
        }
        */
    }

    private void OnValidate()
    {
        if (!parent)
            parent = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Button.PrimaryThumbStick)
        {
            Spawn();
            Debug.Log("Input is good");
        }
        
    }

    private void Spawn()
    {
        // We don't randomise the y value because we don't want the can to float.
        Vector3 position = new Vector3(Random.Range(x - 0.2f, x + 0.2f), y, Random.Range(z - 0.2f, z + 0.2f));
        Instantiate(originalObject, position, parent.rotation, parent); 
    }
}
