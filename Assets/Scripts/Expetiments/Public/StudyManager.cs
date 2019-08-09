using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StudyManager : MonoBehaviour
{
    // for moving user cursor and dummy cursor params
    public bool isDelay;
    public bool isDiscover;
    public float delayTime;// for recording
    public float delayInterval;// a session
    public float diffDelayInterval;// for 1 up two down method
    public List<int> dummyNumSession;
    public int dummyNum;// for recording
    public List<float> cdrSession;
    public float cdr;// for recording
    public int minAngle;
    public int maxAngle;
    public float discoveredTime;
    // for only experiments mode params
    public string subjectName;
    public bool isStartStudy;
    public bool finishInterval;
    public float sessionIntervalTime;
    public int selectedVisual;
    public List<string> studySessions;
    public List<string> practiceSessions;
    public string perSession;
    public bool isStartSession;
    public int resultState;// 0: success(correct self cursor), 1: error(not correct self cursor), 2: failed(time over)
    public float initx, inity;// initialize cursor position
    public int currentSession;
    public string rootPath, absPath, relPath;
    public float timeLimit;
    public List<Vector2> absPosStock, relPosStock;
    public int positive;// positive for cursr
    public int turn;// 1 up two down judge
    public int continuous;// which is continued ?
    public string preReaction; // a previous judge
    public string currentReaction; // a current judge
    private ExperimentalManager em;
    public bool isPractice;
    public int selfCursorNum;
    public List<int> dummySelectableNumbers;
    public int userSelectCursorNum;
    public int currentPracticeSession, currentStudySession;
    public void Awake()
    {
        subjectName = "your name";
        isStartStudy = false;
        finishInterval = false;
        isDiscover = false;
        sessionIntervalTime = 4.0f;
        perSession = "";
        isStartSession = false;
        initx = 0;
        inity = 0;
        currentSession = 0;
        rootPath = "";
        absPath = "";
        relPath = "";
        absPosStock = new List<Vector2>();
        relPosStock = new List<Vector2>();
        timeLimit = 61.0f;
        positive = 0;
        turn = 0;
        continuous = 0;
        preReaction = "";
        currentReaction = "";
        isPractice = true;
        selfCursorNum = 99999;// init number
        dummySelectableNumbers = new List<int>();
        dummySelectableNumbers.Add(9999);
        userSelectCursorNum = 9999;
        currentPracticeSession = 1;
        currentStudySession = 1;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called before the first frame update
    void Start()
    {
        initializeSettings();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        initializeSettings();
    }
    
    private void initializeSettings()
    {
        em = GameObject.Find("ExperimentalManager").GetComponent<ExperimentalManager>();
        // studySessions = new List<string>(em.ExperimentalSettings);
        studySessions = new List<string>(em.ExperimentalSettings);
        practiceSessions = new List<string>(em.PracticeSettings);
        selectedVisual = em.selectedVisual;
        // dummyNumSession = em.dummyNumSession;
        // delayInterval = em.intervalDelay;
        // diffDelayInterval = delayInterval / 2f;
        // cdrSession = em.cdrSession;
        // GenerateStartStudySession(dummyNumSession, cdrSession);
        minAngle = em.minAngle;
        maxAngle = 360 - minAngle;
    }

    private void GenerateStartStudySession(List<int> _dummy, List<float> _cdr)
    {
        for(int i = 0; i < _dummy.Count; i++){
            for(int j = 0; j < _cdr.Count; j++){
                int _dummies = dummyNumSession[i];
                float _cdrs = cdrSession[j];
                studySessions.Add($"{_dummies.ToString()}" + ",0," + $"{_cdrs.ToString()}");// init delay is 0
            }
        }
    }
}
