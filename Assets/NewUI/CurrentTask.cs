using UnityEngine;
using UnityEngine.UI;

public class CurrentTask : MonoBehaviour {

    public MenuManager mm;
    public StartMenu start;
    public NewTask edit;

    public Task currentTask;

    public Text taskName;
    public Text taskText;

    public Text objective1;
    public Text objective2;
    public Text objective3;
    public Text objective4;

    public Button timer;
    public Text points;

    // initialize task, name, time, text
    public void Initialize(Task todo)
    {
        currentTask = todo;
        taskName.text = currentTask.taskName;
        taskText.text = currentTask.taskText;
        objective1.text = todo.objective1;
        objective2.text = todo.objective2;
        objective3.text = todo.objective3;
        objective4.text = todo.objective4;
        int hours = currentTask.hours;
        int minutes = currentTask.minutes;
        timer.GetComponentInChildren<Text>().text = hours + ":" + minutes;
        points.text = "Points: " + currentTask.GetPoints(mm.taskList.categoryList).ToString();
    }

    // wait for user to press button before starting timer???

    // cancel, add points to task?
    // for now, just returns to start
    // probably stop timer and stuff like that?
    public void Cancel()
    {
        start.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Edit()
    {
        edit.gameObject.SetActive(true);
        edit.Edit(currentTask);
        gameObject.SetActive(false);
    }

    // done, delete current task from list
    // linger for a second, display: DONE! and return to start
    public void Done()
    {
        mm.taskList.DeleteTask(currentTask);
        mm.SaveTaskList();
        start.gameObject.SetActive(true);
        start.Initialize();
        gameObject.SetActive(false);
    }
}
