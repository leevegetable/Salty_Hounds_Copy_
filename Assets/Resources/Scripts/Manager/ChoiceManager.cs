using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public GameObject Panel;
    public GameObject SelectOptionRoot;
    public GameObject OptionPrefab;
    public UI_ChoiceContent[] OptionObjects;

    private void Start()
    {
        init();
    }
    public void setChoiceObjects(ChoicesData selectOptions)
    {
        for(int i = 0; i < selectOptions.Options.Length; i++)
        {
            OptionObjects[i].gameObject.SetActive(true);
            OptionObjects[i].init(selectOptions.Options[i]);
        }
        Panel.SetActive(true);
    }
    private void init()
    {
        //Debug.Log(SystemSetting.capacity_Choices);
        OptionObjects = new UI_ChoiceContent[SystemSetting.capacity_Choices];
        for (int i = 0; i < SystemSetting.capacity_Choices; i++)
        {
            GameObject temp = Instantiate(OptionPrefab, SelectOptionRoot.transform);
            OptionObjects[i] = temp.GetComponent<UI_ChoiceContent>();
            temp.SetActive(false);
        }
    }
}
