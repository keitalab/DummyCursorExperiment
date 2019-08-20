using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text;
using System;

public class RegisteringSubject : MonoBehaviour
{
    public GameObject studyManager;
    private StudyManager sm;
    private InputField subjectNameField;
    public Canvas timerPanel;
    private IntervalTimerViewController itvc;
    public Canvas startPanel;
    private ControlStartPanel csp;
    public Text intervalTimer;
    private IntervalTimer it;
    // Start is called before the first frame update
    void Start()
    {
        sm = studyManager.GetComponent<StudyManager>();
        subjectNameField = GameObject.Find("SubjectNameField").GetComponent<InputField>();
        itvc = timerPanel.GetComponent<IntervalTimerViewController>();
        csp = startPanel.GetComponent<ControlStartPanel>();
        it = intervalTimer.GetComponent<IntervalTimer>();
        subjectNameField.text = "OC2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(subjectNameField.text != "") {
            sm.subjectName = subjectNameField.text;
            sm.isStartStudy = !sm.isStartStudy;
            GenerateFile(sm.subjectName);
            csp.HideStartPanel();
            itvc.ShowIntervalTimer();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void GenerateFile(string _filename)
    {
        string directoryPath = Path.Combine(Application.dataPath, "../data/" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + "-" + _filename);
        sm.rootPath = directoryPath;
        string filepath = Path.Combine(directoryPath, "result.csv");
    
        string perSessionAbsDirectoryPath = Path.Combine(directoryPath, "absolute");
        string perSessionRelDirectoryPath = Path.Combine(directoryPath, "relative");

        StreamWriter sw = null;
        try {
            Directory.CreateDirectory(directoryPath);
            Directory.CreateDirectory(perSessionAbsDirectoryPath);
            Directory.CreateDirectory(perSessionRelDirectoryPath);
            sw = File.CreateText(filepath);
        } catch(Exception e) {
            Debug.Log (e.Message);
        } finally {
            if(sw != null)
            {
                try {
                    sw.Dispose();
                } catch(Exception e){
                    Debug.Log(e.Message);
                }
            }
        }
        FileInfo fi = new FileInfo(filepath);
        sw = fi.AppendText();
        string[] column = {"dummyNum", "selfCursorNum", "userSelectedNum", "delay", "cdr", "minAngle", "maxAngle", "InitX", "InitY","time", "Judge", "AbsolutePos", "RelativePos"};
        string _column = string.Join(",", column);
        sw.WriteLine(_column);
        sw.Flush();
        sw.Close();
    }
}
