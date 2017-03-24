using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

public class Category {

    [XmlAttribute("CategoryName")]
    public string categoryName;

    [XmlElement("CategoryNumber")]
    public int categoryNumber;

    [XmlElement("CategoryPoints")]
    public int points;

    [XmlElement("CategoryColor")]
    public string categoryColor;

    public Category()
    {

    }

    public Category(string name, int number, string color)
    {
        categoryName = name;
        categoryNumber = number;
        categoryColor = color;
    }
}
