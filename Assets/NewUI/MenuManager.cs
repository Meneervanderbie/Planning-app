using UnityEngine;
using System;

public class MenuManager : MonoBehaviour {

    // TODO: add highscores to tasklist?

    private IO io;
    public TaskList taskList;
    private string dataPath = "/Data/Tasklist.xml";

    public StartMenu startMenu;

    // Save current day
    public DateTime today;

    // Save current points
    public int points;

    // Maybe highscore list? (only top 10? or last few days?) must have both score and day

    // Get Tasklist?

	void Start () {
        io = new IO();
        taskList = io.Load(Application.dataPath + dataPath);
        today = DateTime.Today;
        taskList.Initialize();
        startMenu.Initialize();
        // if new day then reset points, otherwise get this day from highscores and set points to that number
	}

    public void SaveTaskList()
    {
        io.Save(Application.dataPath + dataPath, taskList);
    }

}
