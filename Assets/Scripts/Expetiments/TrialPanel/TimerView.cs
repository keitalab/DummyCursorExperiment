using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    private StudyManager sm;
    private Text timerView;
    private float trialTime;
    private int seconds;
    public GameObject userCursor;
    private ExCursorMove ecm;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("StudyManager").GetComponent<StudyManager>();
        timerView = gameObject.GetComponent<Text>();
        ecm = userCursor.GetComponent<ExCursorMove>();
        trialTime = sm.timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.isStartStudy && sm.isStartSession) StartCountdown();
    }

    public void StartCountdown()
    {
        trialTime -= Time.deltaTime;
        seconds = (int)trialTime;
        timerView.text = seconds.ToString() + "[s]";

        if(seconds <= 0){
            ecm.FinishSession(true);
            return;
        }
    }

    public void ResetTimer()
    {
        trialTime = sm.timeLimit;
        seconds = (int)trialTime;
    }
}
