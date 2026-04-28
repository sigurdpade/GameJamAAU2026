using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

[System.Serializable]
public class LearningInformation
{
    public string name;

    [TextArea]
    public string description; 
}

public class LearningPopUp : MonoBehaviour
{
    public static LearningPopUp instance;

    public GameObject infoTextPanel;
    public TMP_Text infoName;
    public TMP_Text infoText;

    //private HashSet<string> seenEnemies = new HashSet<string>();
    private HashSet<string> builtBuildings = new HashSet<string>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TryShowInfo(LearningInformation info, string buildingId)
    {
        if (builtBuildings.Contains(buildingId))
            return;

        builtBuildings.Add(buildingId);
        ShowInformationBox(info);
    }

    public void ShowInformationBox(LearningInformation learningInformation)
    {
        infoName.text = learningInformation.name;
        infoText.text = learningInformation.description;
        PauseManager.instance.PauseTime();
        infoTextPanel.SetActive(true);
    }

    public void HideInformationBox()
    {
        PauseManager.instance.ResumeTime();
        infoTextPanel.SetActive(false);
    }
}
