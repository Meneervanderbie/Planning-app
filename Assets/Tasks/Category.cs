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

    // color thing etc;

    public Category()
    {

    }

    public Category(string name, int number)
    {
        categoryName = name;
        categoryNumber = number;
    }
}
