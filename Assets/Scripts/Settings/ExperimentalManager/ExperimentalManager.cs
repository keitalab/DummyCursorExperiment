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

    void Awake()
    {
        dummyNumSession = new List<int>();
        dummyNumSession.Add(5);
        dummyNumSession.Add(10);
        dummyNumSession.Add(15);
        minDelay = 0.0f;
        maxDelay = 1.0f;
        intervalDelay = 0.1f;
        minAngle = 30;
        maxAngle = 360-minAngle;
        selectedVisual = 0;
        cdr = 0.7f;// default cdr
        cdrSession = new List<float>();
        cdrSession.Add(cdr);
        delaySession = new List<int>();
        delaySession.Add(1);
        ExperimentalSettings = new List<string>();
        ExperimentalSettings.Add("init");
        trial = 5;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene()
    {
        SaveSeaquence();
        DontDestroyOnLoad(this);
        SceneManager.LoadScene("Experiments", LoadSceneMode.Single);
    }

    private int CalcDelaySession(float _min, float _max, float _interval)
    {
        delaySession.Clear();
        int min = Mathf.CeilToInt(_min * 1000f);
        int max = Mathf.CeilToInt(_max * 1000f);
        int interval = Mathf.CeilToInt(_interval * 1000f);

        if(min >= max) return 0;

        if(interval == 0)
        {
            delaySession.Add(min);
            delaySession.Add(max);
            return delaySession.Count;
        }

        if((max-min) % interval == 0)
        {
            for(int i = min; i < (max-min)/interval; i++)
            {
                delaySession.Add(i*interval);
            }
            delaySession.Add(max);
            return delaySession.Count;
        } else
        {
            for(int i = min; i < (max-min)/interval; i++)
            {
                delaySession.Add(i*interval);
            }
            delaySession.Add(max);
            return delaySession.Count;
        }
    }

    public void SaveSeaquence()
    {
        ExperimentalSettings.Clear();
        int delaySessionSum = CalcDelaySession(minDelay, maxDelay, intervalDelay);
        for(int i = 0; i < dummyNumSession.Count; i++){
            for(int j = 0; j < delaySessionSum; j++){
                for(int k = 0; k < cdrSession.Count; k++){
                    for(int l = 0; l < trial; l++){
                        int _dummies = dummyNumSession[i];
                        int _delay = delaySession[j];
                        float _cdr = cdrSession[k];
                        ExperimentalSettings.Add($"{_dummies.ToString()}" + "," + $"{_delay.ToString()}" + "," + $"{_cdr.ToString()}");
                    }
                }
            }
        }
    }
}
