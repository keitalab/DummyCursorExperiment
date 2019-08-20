using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class IntervalTimer : MonoBehaviour
{
    private StudyManager sm;
    private Text timerView;
    private float intervalTime;
    private int seconds;
    public GameObject userCursor;
    private ExCursorMove excv;
    private ExCursorView ecv;
    public GameObject dummyCursor;
    private ExDummyCreator exdc;
    public Text judgeAndTimeView;
    private JudgeAndTimeViewer jatv;

    public Canvas timerPanel;
    private IntervalTimerViewController itvc;
    public Canvas trialPanel;
    private ControlTrialPanel ctp;
    public Canvas selectorPanel;
    private CursorSelectorController csc;
    public GameObject createDumyNumberView;
    private CreateDummyNumbeerView cdnv;
    
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("StudyManager").GetComponent<StudyManager>();
        excv = userCursor.GetComponent<ExCursorMove>();
        ecv = userCursor.GetComponent<ExCursorView>();
        exdc = dummyCursor.GetComponent<ExDummyCreator>();
        jatv = judgeAndTimeView.GetComponent<JudgeAndTimeViewer>();
        itvc = timerPanel.GetComponent<IntervalTimerViewController>();
        csc = selectorPanel.GetComponent<CursorSelectorController>();
        timerView = gameObject.GetComponent<Text>();
        ctp = trialPanel.GetComponent<ControlTrialPanel>();
        cdnv = createDumyNumberView.GetComponent<CreateDummyNumbeerView>();
        intervalTime = sm.sessionIntervalTime;// interval is 3 seconds
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.isStartStudy && !sm.isStartSession) StartCountdown();
    }

    public void StartCountdown()
    {
        if(!sm.isPractice) {
            ecv.UnableView();
            UnableDummyView();
        }
        intervalTime -= Time.deltaTime;
        seconds = (int)intervalTime;
        timerView.text = seconds.ToString();
        if(seconds <= 0){
            sm.isDiscover = false;
            intervalTime = sm.sessionIntervalTime;
            seconds = (int)intervalTime;
            sm.finishInterval = true;
            ecv.EnableView();
            EnableDummyView();
            SetStudyParams();
            cdnv.DestroyDummyView();
            cdnv.InitDummyNumberView();
            jatv.StartRecording();
            itvc.HideIntervalTimer();
            csc.HideCursorSelector();
            if(sm.isPractice) sm.currentPracticeSession++; else sm.currentStudySession++;
            sm.isStartSession = !sm.isStartSession;
            ctp.ShowTrialPanel();
            return;
        }
    }

    private void SetStudyParams()
    {
        sm.isPractice = sm.practiceSessions.Count != 0 ? true : false;
        if(sm.isPractice)
        {   
            int sessionCount = sm.practiceSessions.Count;
            if(sessionCount == 0) Quit();
            if(sm.init)
            {
                sm.perSession = sm.practiceSessions[0];
                sm.practiceSessions.RemoveAt(0);
            } else {
                int sessionNum = UnityEngine.Random.Range(0, sessionCount);
                sm.perSession = sm.practiceSessions[sessionNum];
                sm.practiceSessions.RemoveAt(sessionNum);
            }

            string[] _params = sm.perSession.Split(',');
            sm.dummyNum = int.Parse(_params[0]);
            sm.delayTime = float.Parse(_params[1]) / 1000f;
            sm.isDelay = (sm.delayTime) == 0f ? false : true;
            sm.cdr = float.Parse(_params[2]);

            // generate correct user cursor number by randomizer
            int totalCursors = 1 + sm.dummyNum;
            sm.dummySelectableNumbers.Clear();
            sm.dummySelectableNumbers = new List<int>();
            for(int i = 0; i < totalCursors; i++){
                sm.dummySelectableNumbers.Add(i);
            }
            int _selectorNum = UnityEngine.Random.Range(0, totalCursors);
            sm.selfCursorNum = sm.dummySelectableNumbers[_selectorNum];// set correct cursor number
            sm.dummySelectableNumbers.RemoveAt(_selectorNum); // remove true cursor number from list

            excv.RandomizeCursorPos();// generate user cursor
            if(sm.dummyNum > 1) exdc.GenerateDummyCursor(sm.dummyNum);// generate dummy curosr
        } else {
            int sessionCount = sm.studySessions.Count;
            if(sessionCount == 0) Quit();
            int sessionNum = UnityEngine.Random.Range(0, sessionCount);
            sm.perSession = sm.studySessions[sessionNum];
            sm.studySessions.RemoveAt(sessionNum);

            string[] _params = sm.perSession.Split(',');
            sm.dummyNum = int.Parse(_params[0]);
            sm.delayTime = float.Parse(_params[1]) / 1000f;
            sm.isDelay = (sm.delayTime) == 0f ? false : true;
            sm.cdr = float.Parse(_params[2]);

            // generate correct user cursor number by randomizer
            int totalCursors = 1 + sm.dummyNum;
            sm.dummySelectableNumbers.Clear();
            sm.dummySelectableNumbers = new List<int>();
            for(int i = 0; i < totalCursors; i++){
                sm.dummySelectableNumbers.Add(i);
            }
            int _selectorNum = UnityEngine.Random.Range(0, totalCursors);
            sm.selfCursorNum = sm.dummySelectableNumbers[_selectorNum];// set correct cursor number
            sm.dummySelectableNumbers.RemoveAt(_selectorNum); // remove true cursor number from list

            excv.RandomizeCursorPos();// generate user cursor
            if(sm.dummyNum > 1) exdc.GenerateDummyCursor(sm.dummyNum);// generate dummy curosr
        }
    }

    // for delay one-up-two-down    
    private void _SetStudyParams()
    {
        int sessionCount = sm.studySessions.Count;
        if(sessionCount == 0){
            if(sm.positive > 1){
                sm.currentReaction = "s";
                if(sm.preReaction == ""){
                    sm.continuous++;
                    sm.preReaction = sm.currentReaction;
                    GeneratePositiveSession();
                } else if(sm.preReaction == "s"){
                    sm.continuous++;
                    GeneratePositiveSession();
                } else if(sm.preReaction == "f"){
                    if(sm.continuous > 1){
                        sm.turn++;
                        sm.continuous = 0;
                        GeneratePositiveSession();
                    } else {
                        sm.continuous = 0;
                        GeneratePositiveSession();
                    }
                }
                sm.preReaction = sm.currentReaction;
            } else {
                sm.currentReaction = "f";
                if(sm.preReaction == ""){
                    sm.continuous++;
                    sm.preReaction = sm.currentReaction;
                    Quit();
                } else if(sm.preReaction == "s"){
                    if(sm.continuous > 1){
                        sm.turn++;
                        sm.continuous = 0;
                        GenerateNegativeSession();
                    } else {
                        sm.continuous = 0;
                        GenerateNegativeSession();
                    }
                } else if(sm.preReaction == "f"){
                    sm.continuous++;
                    GenerateNegativeSession();
                }
                sm.preReaction = sm.currentReaction;
            }
        }

        if(sm.turn > 2) Quit();

        int sessionNum = UnityEngine.Random.Range(0, sm.studySessions.Count);
        sm.perSession = sm.studySessions[sessionNum];
        sm.studySessions.RemoveAt(sessionNum);

        string[] _params = sm.perSession.Split(',');
        sm.dummyNum = int.Parse(_params[0]);
        sm.delayTime = float.Parse(_params[1]);
        sm.isDelay = (sm.delayTime) == 0f ? false : true;
        sm.cdr = float.Parse(_params[2]);

        excv.RandomizeCursorPos();// generate user cursor
        if(sm.dummyNum > 1) exdc.GenerateDummyCursor(sm.dummyNum);// generate dummy curosr
    }

    private void GeneratePositiveSession()
    {
        sm.studySessions.Clear();
        sm.positive = 0;
        sm.delayTime += sm.delayInterval;
        for(int i = 0; i < sm.dummyNumSession.Count; i++){
            for(int j = 0; j < sm.cdrSession.Count; j++){
                int _dummies = sm.dummyNumSession[i];
                float _cdr = sm.cdrSession[j];
                sm.studySessions.Add($"{_dummies.ToString()}" + "," + $"{sm.delayTime.ToString()}" + "," + $"{_cdr.ToString()}");
            }
        }
    }

    private void GenerateNegativeSession()
    {
        sm.studySessions.Clear();
        sm.positive = 0;
        sm.delayTime -= sm.diffDelayInterval;
        for(int i = 0; i < sm.dummyNumSession.Count; i++){
            for(int j = 0; j < sm.cdrSession.Count; j++){
                int _dummies = sm.dummyNumSession[i];
                float _cdr = sm.cdrSession[j];
                sm.studySessions.Add($"{_dummies.ToString()}" + "," + $"{sm.delayTime.ToString()}" + "," + $"{_cdr.ToString()}");
            }
        }
    }

    private void ShowListContentsInTheDebugLog<T>(List<T> list)
        {
            string log = "";

            foreach(var content in list.Select((val, idx) => new {val, idx}))
            {
                if (content.idx == list.Count - 1)
                    log += content.val.ToString();
                else
                    log += content.val.ToString() + ", ";
            }

        Debug.Log(log);
        }

    void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    private void EnableDummyView()
    {
        foreach(Transform child in dummyCursor.transform) {
            child.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void UnableDummyView()
    {
        foreach(Transform child in dummyCursor.transform) {
            child.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
