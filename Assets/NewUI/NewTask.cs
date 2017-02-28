using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class NewTask : MonoBehaviour {

    public GameObject startMenu;
    public MenuManager mm;
    public GameObject planMenu;

    public InputField taskName;
    public InputField description;
    public Dropdown hourSelect;
    public Dropdown minuteSelect;
    public InputField objective1;
    public InputField objective2;
    public InputField objective3;
    public InputField objective4;
    public Dropdown categories;
    public Dropdown deadline;

    public Task editTask;

    // back button
    public void Back()
    {
        planMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    // Initialize all the categories OnEnable and clear all fields
    void OnEnable()
    {
        taskName.text = "";
        description.text = "";
        objective1.text = "";
        objective2.text = "";
        objective3.text = "";
        objective4.text = "";
        deadline.value = 0;

        categories.ClearOptions();
        List<string> categoryList = mm.taskList.GetCategoryStrings();
        categories.AddOptions(categoryList);
        categories.value = 0;

        minuteSelect.ClearOptions();
        hourSelect.ClearOptions();
        List<string> minuteList = new List<string>();
        minuteList.Add("Minutes");
        for (int i = 1; i < 60; i++)
        {
            minuteList.Add(i.ToString());
        }
        minuteSelect.AddOptions(minuteList);
        minuteSelect.value = 0;

        List<string> hourList = new List<string>();
        hourList.Add("Hours");
        for (int i = 1; i < 6; i++)
        {
            hourList.Add(i.ToString());
        }
        hourSelect.AddOptions(hourList);
        hourSelect.value = 0;
    }

    public void Edit(Task task)
    {
        editTask = task;
        taskName.text = task.taskName;
        description.text = task.taskText;
        hourSelect.value = task.hours;
        minuteSelect.value = task.minutes;
        objective1.text = task.objective1;
        objective2.text = task.objective2;
        objective3.text = task.objective3;
        objective4.text = task.objective4;
        categories.value = task.taskCategory;
        if(task.taskDeadline == DateTime.Today)
        {
            deadline.value = 0;
        }
        else if(task.taskDeadline == DateTime.Today.AddDays(1))
        {
            deadline.value = 1;
        }
        else if (task.asap)
        {
            deadline.value = 2;
        }
        else if((task.taskDeadline - DateTime.Today).TotalDays <= 7)
        {
            deadline.value = 3;
        }
        else if((task.taskDeadline - DateTime.Today).TotalDays <= 31)
        {
            deadline.value = 4;
        }
        else
        {
            deadline.value = 5;
        }
    }

    // When OK clicked, read all fields, make new task, save the tasklist and return to start. 
    public void AddTask()
    {
        if (!mm.taskList.CheckIfNameExists(taskName.text) || editTask != null){
            int hours = 0;
            int.TryParse(hourSelect.GetComponentInChildren<Text>().text, out hours);
            int minutes = 0;
            int.TryParse(minuteSelect.GetComponentInChildren<Text>().text, out minutes);

            if (editTask == null)
            {
                Task toAdd = new Task(taskName.text, description.text, objective1.text, objective2.text, objective3.text, objective4.text, hours, minutes, categories.value, deadline.value);
                mm.taskList.AddTask(toAdd);
            }
            else
            {
                editTask.Edit(taskName.text, description.text, objective1.text, objective2.text, objective3.text, objective4.text, hours, minutes, categories.value, deadline.value);
            }

            mm.SaveTaskList();

            startMenu.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            taskName.text += " - Name already exists";
        }
    }

    public void StopEdit()
    {
        editTask.taskName = taskName.text;
    }

}
