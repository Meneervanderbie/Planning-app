using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public TaskList taskList;
    public IO io;
    public string dataPath = "/Data/Tasklist.xml";

    public ButtonList buttonList;
    public Calendar calendar;

    public TaskPopup taskPopup;
    public Dropdown popupCategories;
    public bool popupActive;

    void Start()
    {
        io = new IO();
        taskPopup.ui = this;
        taskList = io.Load(Application.dataPath + dataPath);
        if (taskList.categoryList.Count == 0)
        {
            TempCategoryMaker();
        }
        calendar.StartUp(taskList);

        popupCategories.ClearOptions(); 
        popupCategories.AddOptions(taskList.GetCategoryStrings());

        buttonList.MakeButtonList(taskList.taskList, false);
    }

    public void AddTask()
    {
        // make new constructor with all values?
        Task newTask = new Task("New task");
        taskList.AddTask(newTask);
        buttonList.SpawnButton(newTask);
        taskPopup.SetTask(newTask);
        taskPopup.gameObject.SetActive(true);
    }

    public void DeleteTask(Task task)
    {
        taskList.DeleteTask(task);
        buttonList.MakeButtonList(taskList.taskList, false);
    }

    public void SaveTasks()
    {
        io.Save(Application.dataPath + "/Data/Tasklist.xml", taskList);
        buttonList.MakeButtonList(taskList.taskList, false);
        calendar.UpdateCalendar();
    }

    public void TempCategoryMaker()
    {
        taskList.categoryList = new List<Category>();
        taskList.categoryList.Add(new Category("Werk", 0));
        taskList.categoryList.Add(new Category("Studie", 1));
        taskList.categoryList.Add(new Category("Huishouden", 2));
        taskList.categoryList.Add(new Category("Persoonlijk", 3));
        taskList.categoryList.Add(new Category("Planner", 4));
    }

}
