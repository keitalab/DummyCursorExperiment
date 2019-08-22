using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionView : MonoBehaviour
{
    public GameObject studyManager;
    private StudyManager sm;
    private int totalSessionNum;

    private Text sessionView;

    // Start is called before the first frame update
    void Start()
    {
        sm = studyManager.GetComponent<StudyManager>();
        sessionView = gameObject.GetComponent<Text>();
        totalSessionNum = sm.studySessions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.isStartSession) ShowSession();
    }

    private void ShowSession()
    {
        sessionView.text = (sm.studySessions.Count).ToString() + "/" + (totalSessionNum-1).ToString();
    }
}
