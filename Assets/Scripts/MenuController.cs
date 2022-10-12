using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        // Unload other scene - really should be keeping track of what scene we're in
        SceneManager.UnloadSceneAsync("Cabin");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void DevCabinBtn()
    {
        SceneManager.LoadScene("Cabin", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Main"); // as above
    }
}
