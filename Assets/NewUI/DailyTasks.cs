using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyTasks : MonoBehaviour {

    public MenuManager mm;
    public PlanMenu pm;
    public NewTask nt;

    public GameObject Buttons;
    public Button[] ButtonList;
    public int numTasks;

    public void Back()
    {
        pm.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        ButtonList = Buttons.GetComponentsInChildren<Button>(true);
        numTasks = mm.taskList.dailyList.Count;
        for (int i = 0; i < numTasks; i++)
        {
            ButtonList[i].GetComponentInChildren<Text>().text = mm.taskList.dailyList[i].GetName();
        }
    }

    public void ButtonPressed(int butt)
    {
        if(butt < numTasks)
        {
            nt.gameObject.SetActive(true);
            nt.Edit(mm.taskList.dailyList[butt]);
            gameObject.SetActive(false);
        }
        else
        {
            nt.gameObject.SetActive(true);
            nt.dailyTask = true;
            gameObject.SetActive(false);
        }
    }



}
