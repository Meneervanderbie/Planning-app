using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("Tasks")]
public class TaskList {

    [XmlArray("TaskList")]
    [XmlArrayItem("Task")]
    public List<Task> taskList;

    [XmlArray("CategoryList")]
    [XmlArrayItem("Category")]
    public List<Category> categoryList;

    public TaskList()
    {
        taskList = new List<Task>();
    }

    // TODO: prevent duplicate tasks from being added
    public void AddTask(Task task)
    {
        taskList.Add(task);
    }

    public void DeleteTask(Task task)
    {
        taskList.Remove(task);
    }

    public List<Task> GetList()
    {
        return taskList;
    }

    public List<string> GetCategoryStrings()
    {
        List<string> toReturn = new List<string>();
        foreach(Category cat in categoryList)
        {
            toReturn.Add(cat.categoryName);
        }
        return (toReturn);
    }

    public List<Task> GetDay(int year, int month, int day)
    {
        List<Task> returnList = new List<Task>();

        foreach(Task task in taskList)
        {
            if(task.datePlanned.Year == year && task.datePlanned.Month == month && task.datePlanned.Day == day)
            {
                returnList.Add(task);
            }
        }

        return returnList;
    }
}
