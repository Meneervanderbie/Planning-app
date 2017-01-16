using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;

public class Event {

    [XmlAttribute("EventName")]
    public string eventName;

    [XmlElement("EventDescription")]
    public string eventDescription;

    [XmlElement("Objective1")]
    public string objective1;

    [XmlElement("Objective2")]
    public string objective2;

    [XmlElement("Objective3")]
    public string objective3;

    [XmlElement("Objective4")]
    public string objective4;

    [XmlElement("EventLocation")]
    public string eventLocation;

    [XmlElement("EventStart")]
    public DateTime startTime;

    [XmlElement("EventEnd")]
    public DateTime endTime;

    public Event() {

    }

    public Event(string eName, string description, string obj1, string obj2, string obj3, string obj4,  string location, DateTime start, DateTime end)
    {
        eventName = eName;
        eventDescription = description;
        eventLocation = location;
        startTime = start;
        endTime = end;
    }

}
