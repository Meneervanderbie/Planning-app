using UnityEngine;
using System.Collections;

public class StartTask : MonoBehaviour {

    public GameObject startMenu;
    public ChooseTask chooseTask;

    // Back button clicked
    public void Back()
    {
        startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    

    // Highest points button clicked
    public void HighPoints()
    {
        chooseTask.gameObject.SetActive(true);
        chooseTask.Initialize(1);
        gameObject.SetActive(false);
    }

    // Closest deadline button clicked
    public void CloseDeadline()
    {
        chooseTask.gameObject.SetActive(true);
        chooseTask.Initialize(2);
        gameObject.SetActive(false);
    }

    // Random button clicked
    public void RandomTasks()
    {
        chooseTask.gameObject.SetActive(true);
        chooseTask.Initialize(3);
        gameObject.SetActive(false);
    }

    // Display next task

}
