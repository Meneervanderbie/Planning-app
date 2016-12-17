using UnityEngine;
using System.Collections;
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

    public string GetName()
    {
        return taskName;
    }

    public void SetName(string name)
    {
        taskName = name;
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
