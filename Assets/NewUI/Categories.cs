using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Categories : MonoBehaviour {

    public MenuManager mm;
    public GameObject planMenu;
    public GameObject startMenu;

    public Dropdown categories;
    public InputField catName;
    public InputField catPoints;
    public Image colorPreview;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Category current;
    public Color catColor;

    // Back button (to where?)
    public void Back()
    {
        planMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    // Initialize dropdown
    public void Initialize()
    {
        categories.ClearOptions();
        List<string> categoryList = mm.taskList.GetCategoryStrings();
        categoryList.Add("New");
        categories.AddOptions(categoryList);
        categories.value = 0;
        SetFields(0);
    }

    public void SelectCategory()
    {
        if(categories.value == mm.taskList.GetCategoryStrings().Count) {
            mm.taskList.categoryList.Add(new Category("New", 0, GetStringFromColor(Color.white)));
        }
        SetFields(categories.value);
    }

    public void DeleteCategory() {
        mm.taskList.categoryList.RemoveAt(categories.value);
    }

    public void SetFields(int cat) { 
        current = mm.taskList.categoryList[cat];
        catName.text = current.categoryName;
        catPoints.text = current.points.ToString();
        if(current.categoryColor == null)
        {
            current.categoryColor = GetStringFromColor(Color.white);
            catColor = Color.white;
        }
        else
        {
            catColor = GetColorFromString(current.categoryColor);
        }
        colorPreview.color = catColor;
        redSlider.value = catColor.r;
        greenSlider.value = catColor.g;
        blueSlider.value = catColor.b;
    }

    // Change slider values
    public void RedValueChanged()
    {
        catColor.r = redSlider.value;
        colorPreview.color = catColor;
    }

    public void GreenValueChanged()
    {
        catColor.g = greenSlider.value;
        colorPreview.color = catColor;
    }

    public void BlueValueChanged()
    {
        catColor.b = blueSlider.value;
        colorPreview.color = catColor;
    }

    // Save to tasklist
    public void Save()
    {
        current.categoryName = catName.text;
        int tempPoints;
        int.TryParse(catPoints.text, out tempPoints);
        current.points = tempPoints;
        current.categoryColor = GetStringFromColor(catColor);
        mm.taskList.categoryList[current.categoryNumber] = current;
        mm.SaveTaskList();
        startMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public Color GetColorFromString(string colStr)
    {
        int count = 0;
        // get r
        string rStr = "";
        rStr += colStr[count];
        count++;
        while(colStr[count] != ',')
        {
            rStr += colStr[count];
            count++;
        }
        float r;
        float.TryParse(rStr, out r);
        count++;
        // get g
        string gStr = "";
        gStr += colStr[count];
        count++;
        while (colStr[count] != ',')
        {
            gStr += colStr[count];
            count++;
        }
        float g;
        float.TryParse(gStr, out g);
        count++;
        // get b
        string bStr = "";
        bStr += colStr[count];
        count++;
        for(int i = count; i < colStr.Length; i++)
        {
            bStr += colStr[i];
        }
        float b;
        float.TryParse(bStr, out b);

        //print(r + " " + g + " " + b);

        return new Color(r,g,b);
    }

    public string GetStringFromColor(Color color)
    {
        return color.r.ToString() + ',' + color.g.ToString() + ',' + color.b.ToString();
    }
}
