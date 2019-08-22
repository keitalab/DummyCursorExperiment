using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class ExperimentalManager : MonoBehaviour
{
    public List<int> dummyNumSession;
    public float minDelay;
    public float maxDelay;
    public float intervalDelay;
    public int minAngle;
    public int maxAngle;
    public float cdr;
    public List<float> cdrSession;
    public int selectedVisual;
    public List<int> delaySession;
    public List<string> ExperimentalSettings;
    public int trial;
    public int practiceSession;
    public List<string> PracticeSettings;

    void Awake()
    {
        dummyNumSession = new List<int>();
        dummyNumSession.Add(5);
        dummyNumSession.Add(10);
        dummyNumSession.Add(20);
        dummyNumSession.Add(50);
        minDelay = 0.0f;
        maxDelay = 1.0f;
        intervalDelay = 0.1f;
        minAngle = 45;
        maxAngle = 360-minAngle;
        selectedVisual = 1;
        cdr = 1.0f;// default cdr
        cdrSession = new List<float>();
        cdrSession.Add(cdr);
        delaySession = new List<int>();
        delaySession.Add(1);
        ExperimentalSettings = new List<string>();
        ExperimentalSettings.Add("init");
        trial = 3;
        practiceSession = 1;
        PracticeSettings = new List<string>();
        PracticeSettings.Add("1,0.0,1.0");
        PracticeSettings.Add("5,0.0,1.0");
        PracticeSettings.Add("10,0.0,1.0");
        PracticeSettings.Add("20,0.0,1.0");
        PracticeSettings.Add("50,0.0,1.0");
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene()
    {
        SaveSeaquence();
        DataManager.Instance.PreviousScene = "Settings";
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Experiments", LoadSceneMode.Single);
    }

    public void SaveSeaquence()
    {
        ExperimentalSettings.Clear();
        for(int i = 0; i < dummyNumSession.Count; i++){
            for(int j = 0; j < trial; j++){
                int _dummies = dummyNumSession[i];
                ExperimentalSettings.Add($"{_dummies.ToString()}" + ",0.0,1.0");
            }
        }
    }
}
