using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChooseTask : MonoBehaviour {

    public MenuManager mm;

    public Button firstButton;
    public Button secondButton;
    public Button thirdButton;

    public Task[] tasks = new Task[3];

    public GameObject startTask;
    public CurrentTask currentTask;

    // Choose three tasks by method, chosen with int, and display on three buttons (maybe give other info here?)
    // probably also display Text, time, points? (maybe total time available until next task?)

    public void Initialize(int option)
    {
        //print("Test");
        // these are the numbers that the tasks have in the task list currently
        List<Task> tasklist = mm.taskList.taskList;
        int first = 0;
        int second = 1;
        int third = 2;

        // option 1 = points
        // option 2 = deadline
        // option 3 = random
        if(option == 1) {
            // sort by points
            tasklist = mm.taskList.SortList(2);
            mm.SaveTaskList();
            tasks[0] = tasklist[0];
            tasks[1] = tasklist[1];
            tasks[2] = tasklist[2];
        }

        if(option == 2)
        {
            // sort by deadline
            tasklist = mm.taskList.SortList(0);
            tasks[0] = tasklist[0];
            tasks[1] = tasklist[1];
            tasks[2] = tasklist[2];
            // problem if less than 3 tasks in list... fix plx
        }

        if(option == 3)
        {
            int num = tasklist.Count;
            first = Random.Range(0, num);
            second = Random.Range(0, num);
            while(first == second && num > 1)
            {
                second = Random.Range(0, num);
            }
            third = Random.Range(0, num);
            while (first == third || second == third)
            {
                if(num < 2)
                {
                    break;
                    // what to do with third???
                }
                third = Random.Range(0, num);
            }
            tasks[0] = tasklist[first];
            tasks[1] = tasklist[second];
            tasks[2] = tasklist[third];
            // Do something if less than 3 tasks in list?
        }

        firstButton.GetComponentInChildren<Text>().text = tasklist[first].taskName;
        firstButton.image.color = GetColorFromString(mm.taskList.categoryList[tasklist[first].taskCategory].categoryColor);
        secondButton.GetComponentInChildren<Text>().text = tasklist[second].taskName;
        secondButton.image.color = GetColorFromString(mm.taskList.categoryList[tasklist[second].taskCategory].categoryColor);
        thirdButton.GetComponentInChildren<Text>().text = tasklist[third].taskName;
        thirdButton.image.color = GetColorFromString(mm.taskList.categoryList[tasklist[third].taskCategory].categoryColor);
    }

    public void Reroll() {
        Initialize(3);
    }

	// Back button clicked
    public void Back()
    {
        startTask.SetActive(true);
        gameObject.SetActive(false);
    }

    // current points (maybe not applicable?)

    // Button clicked (with int value?)
    public void ButtonClicked(int value)
    {
        currentTask.gameObject.SetActive(true);
        currentTask.Initialize(tasks[value]);
        gameObject.SetActive(false);
    }

    // Display next task

    public Color GetColorFromString(string colStr)
    {
        int count = 0;
        // get r
        string rStr = "";
        rStr += colStr[count];
        count++;
        while (colStr[count] != ',')
        {
            rStr += colStr[count];
            count++;
        }
        float r;
        float.TryParse(rStr, out r);
        count++;
        // get g
        string gStr = "";
        gStr += colStr[count];
        count++;
        while (colStr[count] != ',')
        {
            gStr += colStr[count];
            count++;
        }
        float g;
        float.TryParse(gStr, out g);
        count++;
        // get b
        string bStr = "";
        bStr += colStr[count];
        count++;
        for (int i = count; i < colStr.Length; i++)
        {
            bStr += colStr[i];
        }
        float b;
        float.TryParse(bStr, out b);

        //print(r + " " + g + " " + b);

        return new Color(r, g, b);
    }

}
