using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public InputActionReference reference;
    // Start is called before the first frame update
    void Start()
    {
        reference.action.started += Reset;
    }

    private void Reset(InputAction.CallbackContext context)
    {
        Debug.Log("bsiadhioasjoasd");
        SceneManager.LoadScene("MainMenu");
    }
}
