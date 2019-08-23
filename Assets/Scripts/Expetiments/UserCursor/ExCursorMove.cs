using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoveFunction;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExCursorMove : MonoBehaviour
{
    public GameObject studyManager;
    private StudyManager sm;
    private float rx, ry, ax, ay;
    private Vector3 ConvertPosition;
    private float cx, cy;
    private float px, py;

    public Text judgeAndTimeView;
    private JudgeAndTimeViewer jatv;
    public Canvas timerPanel;
    private IntervalTimerViewController itvc;
    public Text intervalTimer;
    private IntervalTimer it;

    public Canvas trialPanel;
    private ControlTrialPanel ctp;
    public Text timerView;
    private TimerView tv;

    public Canvas selectorPanel;
    private CursorSelectorController csc;
    public GameObject createDumyNumberView;
    private CreateDummyNumbeerView cdnv;
    private ExCursorView ecv;
    public GameObject dummyCursor;

    // Start is called before the first frame update
    void Start()
    {
        sm = studyManager.GetComponent<StudyManager>();
        jatv = judgeAndTimeView.GetComponent<JudgeAndTimeViewer>();
        itvc = timerPanel.GetComponent<IntervalTimerViewController>();
        it = intervalTimer.GetComponent<IntervalTimer>();
        ctp = trialPanel.GetComponent<ControlTrialPanel>();
        tv = timerView.GetComponent<TimerView>();
        csc = selectorPanel.GetComponent<CursorSelectorController>();
        cdnv = createDumyNumberView.GetComponent<CreateDummyNumbeerView>();
        ecv = gameObject.GetComponent<ExCursorView>();
        cx = Screen.width/2;
        cy = Screen.height/2;
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.isStartSession)
        {
            ax = Input.GetAxis("Mouse X");
            ay = Input.GetAxis("Mouse Y");
            Vector3 direction = new Vector3(ax, ay, 0) * 0.5f;

            if(sm.isDelay) {
                StartCoroutine(UserCursorFunc.DelayCursor(sm.delayTime, () =>
                    {
                        UserCursorFunc.MoveCursor(gameObject, direction, sm.cdr);
                    }));
            } else {
                UserCursorFunc.MoveCursor(gameObject, direction, sm.cdr);
            }

            sm.absPosStock.Add(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));
            sm.relPosStock.Add(new Vector2(gameObject.transform.position.x - px, gameObject.transform.position.y - py));
            px = gameObject.transform.position.x;
            py = gameObject.transform.position.y;
            
            if (Input.GetKeyDown(KeyCode.Space) && sm.isStartStudy) FinishSession(false);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(sm.isPractice) {
                if(sm.practiceSessions.Count != 0)
                {
                    if(sm.isPracticeReady) NextSession();
                } else {
                    ecv.UnableView();
                    UnableDummyView();
                    NextSession();
                }
            } else if(sm.isReady) NextSession();
        }
    }

    public void RandomizeCursorPos() {
        rx = UnityEngine.Random.Range(-9.0f, 9.0f);
        ry = UnityEngine.Random.Range(-5.0f, 5.0f);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(new Vector3(rx, ry, 0));
        sm.initx = screenPos.x;
        sm.inity = screenPos.y;
        px = rx;
        py = ry;
        sm.absPosStock.Clear();
        sm.relPosStock.Clear();
        gameObject.transform.position = new Vector3(rx, ry, 0);
    }

    public void FinishSession(bool over)
    {
        if(over) {
            sm.resultState = 2;
        } else {
            sm.resultState = 0;
        }
        tv.ResetTimer();
        jatv.FinishRecording();
        csc.ShowCursorSelector();
        sm.isStartSession = false;
        sm.isStartStudy = false;
    }

    private void NextSession()
    {
        itvc.ShowIntervalTimer();
        sm.isStartStudy = true;
        if(sm.isPractice) sm.isPracticeReady = !sm.isPracticeReady;
    }

    private void UnableDummyView()
    {
        foreach(Transform child in dummyCursor.transform) {
            child.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
