using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class JudgeAndTimeViewer : MonoBehaviour
{
    public GameObject studyManager;
    private StudyManager sm;
    private Text judgeAndTimerText;
    private float startTime, discoveredTime;
    // Start is called before the first frame update
    void Start()
    {
        sm = studyManager.GetComponent<StudyManager>();
        judgeAndTimerText = gameObject.GetComponent<Text>();
        discoveredTime = 0.0f;
        judgeAndTimerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRecording()
    {
        startTime = Time.time;
        GenerateTrackFile();
    }

    public void FinishRecording()
    {
        discoveredTime = Time.time - startTime;
        sm.discoveredTime = (discoveredTime * 1000);
        SaveAbsLog();
        SaveRelLog();
        SaveTrial();
        judgeAndTimerText.text = sm.discoveredTime.ToString("F2") + "[ms]";
        sm.isStartSession = !sm.isStartSession;
        sm.finishInterval = false;
        sm.currentSession++;
    }

    private void SaveTrial()
    {
        string filepath = Path.Combine(sm.rootPath, "result.csv");
        StreamWriter sw;
        FileInfo fi = new FileInfo(filepath);
        string[] param = {sm.dummyNum.ToString(), sm.selfCursorNum.ToString(), sm.userSelectCursorNum.ToString(),
            sm.delayTime.ToString(), sm.cdr.ToString(),
            sm.minAngle.ToString(), sm.maxAngle.ToString(),
            sm.initx.ToString(), sm.inity.ToString(),
            sm.discoveredTime.ToString(), sm.resultState.ToString(),
            sm.absPath, sm.relPath
        };
        string _param = string.Join(",", param);
        sw = fi.AppendText();
        sw.WriteLine(_param);
        sw.Flush();
        sw.Close();
    }

    private void GenerateTrackFile()
    {
        string absDirectoryPath = Path.Combine(sm.rootPath, "absolute");
        string relDirectoryPath = Path.Combine(sm.rootPath, "relative");

        string absTrackFilePath = Path.Combine(absDirectoryPath, "session-" + sm.currentSession.ToString() + ".csv");
        sm.absPath = absTrackFilePath;
        string relTrackFilePath = Path.Combine(relDirectoryPath, "session-" + sm.currentSession.ToString() + ".csv");
        sm.relPath = relTrackFilePath;

        StreamWriter sw1 = null;
        StreamWriter sw2 = null;
        try {
            sw1 = File.CreateText(absTrackFilePath);
            sw2 = File.CreateText(relTrackFilePath);
        } catch(Exception e) {
            Debug.Log(e.Message);
        } finally {
            if(sw1 != null)
            {
                try {
                    sw1.Dispose();
                } catch(Exception e){
                    Debug.Log(e.Message);
                }
            }
            if(sw2 != null)
            {
                try {
                    sw2.Dispose();
                } catch(Exception e){
                    Debug.Log(e.Message);
                }
            }
        }

        FileInfo absf = new FileInfo(absTrackFilePath);
        sw1 = absf.AppendText();
        string[] abscolumn = {"x", "y"};
        string _abscolumn = string.Join(",", abscolumn);
        sw1.WriteLine(_abscolumn);
        sw1.Flush();
        sw1.Close();

        FileInfo relf = new FileInfo(relTrackFilePath);
        sw2 = relf.AppendText();
        string[] relcolumn = {"x", "y"};
        string _relcolumn = string.Join(",", relcolumn);
        sw2.WriteLine(_relcolumn);
        sw2.Flush();
        sw2.Close();
    }

    private void SaveAbsLog()
    {   
        StreamWriter sw;
        FileInfo fi = new FileInfo(sm.absPath);
        sw = fi.AppendText();
        for(int i = 0; i < sm.absPosStock.Count; i++)
        {
            Vector3 converted  = Camera.main.WorldToScreenPoint(new Vector3(sm.absPosStock[i].x, sm.absPosStock[i].y, 0));
            string[] pos = {converted.x.ToString(), converted.y.ToString()};
            string _pos = string.Join(",", pos);
            sw.WriteLine(_pos);
        }
        sw.Flush();
        sw.Close();
    }

    private void SaveRelLog()
    {
        StreamWriter sw;
        FileInfo fi = new FileInfo(sm.relPath);
        sw = fi.AppendText();
        for(int i = 0; i < sm.relPosStock.Count; i++)
        {
            Vector3 converted  = Camera.main.WorldToScreenPoint(new Vector3(sm.relPosStock[i].x, sm.relPosStock[i].y, 0));
            string[] pos = {(converted.x - Screen.width/2).ToString(), (converted.y - Screen.height/2).ToString()};
            string _pos = string.Join(",", pos);
            sw.WriteLine(_pos);
        }
        sw.Flush();
        sw.Close();
    }
}
