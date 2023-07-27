using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;
    public GameObject Panel;
    public GameObject SelectOptionRoot;
    public GameObject OptionPrefab;
    public UI_ChoiceContent[] OptionObjects;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        init();
    }
    public void setChoiceObjects(Choice[] selectOptions)
    {
        for (int i = 0; i < selectOptions.Length; i++)
        {
            OptionObjects[i].gameObject.SetActive(true);
            OptionObjects[i].init(selectOptions[i]);
        }
        Panel.SetActive(true);
    }
    public void setChoiceObjects(Choice[] selectOptions,GameEvent gameEvent, GameEventAction gameEventAction)
    {
        for (int i = 0; i < selectOptions.Length; i++)
        {
            OptionObjects[i].gameObject.SetActive(true);
            OptionObjects[i].init(selectOptions[i], gameEvent, gameEventAction);
        }
        Panel.SetActive(true);
    }
    private void init()
    {
        OptionObjects = new UI_ChoiceContent[SystemSetting.capacity_Choices];
        for (int i = 0; i < SystemSetting.capacity_Choices; i++)
        {
            GameObject temp = Instantiate(OptionPrefab, SelectOptionRoot.transform);
            OptionObjects[i] = temp.GetComponent<UI_ChoiceContent>();
            temp.SetActive(false);
        }
    }
    public void SelectEnd()
    {
        for (int i = 0; i < OptionObjects.Length; i++)
        {
            Debug.Log("SelectOption False");
            OptionObjects[i].gameObject.SetActive(false);
        }
        Panel.SetActive(false);
    }
}
