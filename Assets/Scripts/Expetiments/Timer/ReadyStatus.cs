using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyStatus : MonoBehaviour
{
    public GameObject _StudyManager;
    private StudyManager studyManager;
    private Text ReadyStateText;

    // Start is called before the first frame update
    void Start()
    {
        studyManager = _StudyManager.GetComponent<StudyManager>();
        ReadyStateText = gameObject.GetComponent<Text>();
        ReadyStateText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(!studyManager.isPractice && !studyManager.isReady) ReadyStateText.text = "Are you ready ?";
            else ReadyStateText.text = "";
    }
}
