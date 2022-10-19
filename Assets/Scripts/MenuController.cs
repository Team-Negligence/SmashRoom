using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartBtn()
    {
        SceneManager.LoadScene("Hallway");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }

    public void DevCabinBtn()
    {
        SceneManager.LoadScene("Cabin");
    }

    public void DevHallwayBtn()
    {
        SceneManager.LoadScene("Hallway");
    }

    public void DevMindfulnessBtn()
    {
        SceneManager.LoadScene("MindfulnessRoom");
    }

    public void DevClassroomBtn()
    {
        SceneManager.LoadScene("Classroom");
    }
}
