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

    public enum sortBy {Deadline, DateAdded};

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

    public List<Task> Sort(int value)
    {
        List<Task> sortedList = taskList;
        bool sorted = false;

        sortBy typeSort = (sortBy) value;

        while (!sorted)
        {
            bool switched = false;
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                // TODO: make this switch pretty ;)
                switch (typeSort)
                {
                    case sortBy.Deadline:
                        if (sortedList[i].taskDeadline > sortedList[i + 1].taskDeadline)
                        {
                            Task tempTask = sortedList[i];
                            sortedList[i] = sortedList[i + 1];
                            sortedList[i + 1] = tempTask;
                            switched = true;
                        }
                        break;
                    case sortBy.DateAdded:
                        if (sortedList[i].dateAdded > sortedList[i + 1].dateAdded)
                        {
                            Task tempTask = sortedList[i];
                            sortedList[i] = sortedList[i + 1];
                            sortedList[i + 1] = tempTask;
                            switched = true;
                        }
                        break;
                }
            }
            if (!switched)
            {
                sorted = true;
            }
        }


        return sortedList;
    }
}
