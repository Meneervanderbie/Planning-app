using UnityEngine;
using System.IO;
using System.Collections;
using System.Xml.Serialization;

public class IO {

    public TaskList Load(string filePath)
    {
        XmlSerializer reader = new XmlSerializer(typeof(TaskList));
        FileStream stream = new FileStream(filePath, FileMode.Open);
        TaskList list = reader.Deserialize(stream) as TaskList;
        stream.Close();

        return list;
    }

    public void Save(string filePath, TaskList list)
    {
        FileStream stream = new FileStream(filePath, FileMode.Create);
        XmlSerializer writer = new XmlSerializer(typeof(TaskList));
        writer.Serialize(stream, list);
        stream.Close();
    }

}
