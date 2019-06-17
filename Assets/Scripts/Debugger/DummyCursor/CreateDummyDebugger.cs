﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateDummyDebugger : MonoBehaviour
{
    private ModeManager modeManager;
    public GameObject textPrefab;
    private GameObject[] text;

    public GameObject dummyClones;

    //public Text textPrefab;
    private int dummyNum;
    private float drx, dry, drangle;
    private int counter;
    // Start is called before the first frame update
    void Start()
    {
        modeManager = GetComponent<ModeManager>();
        dummyNum = modeManager.dummyNum;
        text = new GameObject[dummyNum];
        GenerateDummyDebugger(dummyNum);
    }

    // Update is called once per frame
    void Update()
    {
        counter = 0;
        foreach(Transform child in dummyClones.transform) {
            Vector3 pos = Camera.main.WorldToScreenPoint(new Vector3(child.transform.position.x, child.transform.position.y, 0));
            pos.x = pos.x - Screen.width / 4 + 680;
            pos.y = pos.y - Screen.height / 2 + 150;
            text[counter].GetComponent<RectTransform>().localPosition = pos;
            text[counter].GetComponent<Text>().text = "x: " + pos.x + ", y: " + pos.y + ", angle: " + child.transform.position.z.ToString();
            counter++;
        }
    }

    private void GenerateDummyDebugger(int _num)
    {
        for (int i = 0; i < _num; i++)
        {
            text[i] = Instantiate(textPrefab) as GameObject;
            text[i].transform.SetParent(this.transform);
        }
    }
}
