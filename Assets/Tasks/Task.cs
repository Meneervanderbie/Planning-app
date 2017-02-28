using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;

public class Task {
    [XmlAttribute("TaskName")]
    public string taskName;

    [XmlElement("TaskText")]
    public string taskText;

    [XmlElement("TaskPoints")]
    public int taskPoints;

    [XmlElement("TaskCategory")]
    public int taskCategory;

    [XmlElement("TaskDeadline")]
    public DateTime taskDeadline;

    [XmlElement("DateAdded")]
    public DateTime dateAdded;

    [XmlElement("DatePlanned")]
    public DateTime datePlanned;

    [XmlElement("TaskMinutes")]
    public int minutes;

    [XmlElement("TaskHours")]
    public int hours;

    [XmlElement("Objective1")]
    public string objective1;

    [XmlElement("Objective2")]
    public string objective2;

    [XmlElement("Objective3")]
    public string objective3;

    [XmlElement("Objective4")]
    public string objective4;

    [XmlElement("ASAP")]
    public bool asap;

    //[XmlElement("Repeat")]
    //public int repeat;

    TimeSpan taskTime;

    // Task parent; ??

    public Task()
    {

    }

    public Task(string name)
    {
        taskName = name;
        dateAdded = DateTime.Now;
        taskDeadline = DateTime.MaxValue;
    }

    public Task(string name, string text, string obj1, string obj2, string obj3, string obj4, int hrs, int mnts, int cat, int deadline)
    {
        taskName = name;
        taskText = text;
        objective1 = obj1;
        objective2 = obj2;
        objective3 = obj3;
        objective4 = obj4;
        hours = hrs;
        minutes = mnts;
        taskCategory = cat;
        dateAdded = DateTime.Today;
        
        if(deadline == 0)
        {
            taskDeadline = DateTime.Today;
            asap = false;
        }
        else if(deadline == 1)
        {
            taskDeadline = DateTime.MaxValue;
            asap = true;
        }
        else if(deadline == 2)
        {
            taskDeadline = DateTime.Today.AddDays(7);
            asap = false;
        }
        else if(deadline == 3)
        {
            taskDeadline = DateTime.Today.AddMonths(1);
            asap = false;
        }
        else
        {
            taskDeadline = DateTime.MaxValue;
        }
    }

    public void Edit(string name, string text, string obj1, string obj2, string obj3, string obj4, int hrs, int mnts, int cat, int deadline)
    {
        taskName = name;
        taskText = text;
        objective1 = obj1;
        objective2 = obj2;
        objective3 = obj3;
        objective4 = obj4;
        hours = hrs;
        minutes = mnts;
        taskCategory = cat;
        dateAdded = DateTime.Today;

        if (deadline == 0)
        {
            taskDeadline = DateTime.Today;
            asap = false;
        }
        else if(deadline == 1)
        {
            taskDeadline = DateTime.Today.AddDays(1);
            asap = false;
        }
        else if (deadline == 2)
        {
            taskDeadline = DateTime.MaxValue;
            asap = true;
        }
        else if (deadline == 3)
        {
            taskDeadline = DateTime.Today.AddDays(7);
            asap = false;
        }
        else if (deadline == 4)
        {
            taskDeadline = DateTime.Today.AddMonths(1);
            asap = false;
        }
        else
        {
            taskDeadline = DateTime.MaxValue;
        }
    }

    public string GetName()
    {
        return taskName;
    }

    public void SetName(string name)
    {
        taskName = name;
    }

    public int GetPoints(List<Category> categoryList)
    {
        float tempValue = 1f;
        if(taskDeadline == DateTime.Today)
        {
            tempValue = 10;
        }
        else if(taskDeadline < DateTime.Today)
        {
            tempValue = 20;
        }
        else if(taskDeadline != DateTime.MinValue)
        {
            TimeSpan totalTime = taskDeadline - dateAdded;
            TimeSpan timeLeft = taskDeadline - DateTime.Now;
            //Debug.Log(totalTime.TotalMinutes + " " + timeLeft.TotalMinutes);
            tempValue = (float) totalTime.TotalMinutes / (float) timeLeft.TotalMinutes;
        }
        tempValue *= categoryList[taskCategory].points;
        if (asap)
        {
            tempValue *= 4;
        }

        taskPoints = (int)tempValue;
        return (int)tempValue;
    }

    [XmlElement(DataType = "duration", ElementName = "TaskTime")]
    public string taskTimeXML
    {
        get
        {
            return XmlConvert.ToString(taskTime);
        }
        set
        {
            taskTime = string.IsNullOrEmpty(value) ?
                TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
        }
    }

}
