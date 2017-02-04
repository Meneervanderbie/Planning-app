﻿using UnityEngine;
using System;
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

    [XmlArray("WeekList")]
    [XmlArrayItem("WeekDay")]
    public List<WeekDay> weekList;

    // list of days with points and date. probably have max number of 20 days?
    [XmlArray("HighScores")]
    [XmlArrayItem("Days")]
    public List<Day> highscores;

    public TaskList()
    {
        //taskList = new List<Task>();
        //categoryList = new List<Category>();
        //weekList = new List<WeekDay>();
        //WeekDay monday = new WeekDay("Monday");
        //weekList.Add(monday);
        //WeekDay tuesday = new WeekDay("Tuesday");
        //weekList.Add(tuesday);
        //WeekDay wednesday = new WeekDay("Wednesday");
        //weekList.Add(wednesday);
        //WeekDay thursday = new WeekDay("Thursday");
        //weekList.Add(thursday);
        //WeekDay friday = new WeekDay("Friday");
        //weekList.Add(friday);
        //WeekDay saturday = new WeekDay("Saturday");
        //weekList.Add(saturday);
        //WeekDay sunday = new WeekDay("Sunday");
        //weekList.Add(sunday);
    }

    // Add other things here too?
    public void Initialize()
    {
        if (highscores == null)
        {
            highscores = new List<Day>();
            highscores.Add(new Day(DateTime.Today, 0));
        }
        else
        {
            bool gotOne = false;
            foreach (Day day in highscores)
            {
                if (day.date == DateTime.Today)
                {
                    gotOne = true;
                }
            }
            if (!gotOne)
            {
                highscores.Insert(0,new Day(DateTime.Today, 0));
                if (highscores.Count > 20)
                {
                    highscores.RemoveAt(highscores.Count - 1);
                }
            }
        }
    }

    // TODO: prevent duplicate tasks from being added
    public void AddTask(Task task)
    {
        taskList.Add(task);
    }

    public void DeleteTask(Task task)
    {
        highscores[0].score += task.taskPoints;
        taskList.Remove(task);
    }

    public List<Task> GetList()
    {
        return taskList;
    }

    public List<WeekDay> GetWeek()
    {
        return weekList;
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


    // Sortby gives the criterium for sorting
    public List<Task> SortList(int sortBy)
    {
        List<Task> sortedList = taskList;
        bool sorted = false;

        while (!sorted)
        {
            bool switched = false;
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                // sort by deadline
                if (sortBy == 0)
                {
                    if (sortedList[i].taskDeadline > sortedList[i + 1].taskDeadline)
                    {
                        Task tempTask = sortedList[i];
                        sortedList[i] = sortedList[i + 1];
                        sortedList[i + 1] = tempTask;
                        switched = true;
                    }
                }
                // sort by date added
                if (sortBy == 1)
                {
                    if (sortedList[i].dateAdded > sortedList[i + 1].dateAdded)
                    {
                        Task tempTask = sortedList[i];
                        sortedList[i] = sortedList[i + 1];
                        sortedList[i + 1] = tempTask;
                        switched = true;
                    }
                }
                // sort by points
                if(sortBy == 2)
                {
                    foreach(Task task in taskList)
                    {
                        task.GetPoints(categoryList);
                    }
                    if (sortedList[i].taskPoints < sortedList[i + 1].taskPoints)
                    {
                        Task tempTask = sortedList[i];
                        sortedList[i] = sortedList[i + 1];
                        sortedList[i + 1] = tempTask;
                        switched = true;
                    }
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
