using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessionCounter : MonoBehaviour
{
    public GameObject studyManger;
    private StudyManager sm;
    private Text sessionCounter;
    private int practiceCounter, studyCounter;
    private int initPracticeCounter, initStudyCounter;
    // Start is called before the first frame update
    void Start()
    {
        sm = studyManger.GetComponent<StudyManager>();
        sessionCounter = gameObject.GetComponent<Text>();
        sessionCounter.text = "";
        initPracticeCounter = sm.practiceSessions.Count;
        initStudyCounter = sm.studySessions.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.isStartStudy && !sm.isStartSession) ShowNextSession();
    }

    private void ShowNextSession()
    {
        if(sm.isPractice)
        {
            sessionCounter.text =  "Next Practice Session" +
                "\n" + sm.currentPracticeSession.ToString() + "/" + initPracticeCounter.ToString();
        } else {
            sessionCounter.text = "Next Study Session" +
            "\n" + sm.currentStudySession.ToString() + "/" + initStudyCounter.ToString();
        }
    }
}
