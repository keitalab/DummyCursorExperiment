using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CreateDummyNumbeerView : MonoBehaviour
{
    private StudyManager sm;
    public GameObject textPrefab;
    private GameObject[] text;
    public GameObject dummies;
    private float drx, dry, drangle;
    private int counter;
    public Canvas selectPanel;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("StudyManager").GetComponent<StudyManager>();
        text = new GameObject[1];
    }

    // Update is called once per frame
    void Update()
    {
        if(sm.isStartSession)
        {
            counter = 0;
            foreach(Transform child in dummies.transform) {
                Vector3 pos = Camera.main.WorldToScreenPoint(new Vector3(child.transform.position.x, child.transform.position.y, 0));
                pos.x = pos.x - Screen.width / 2;
                pos.y = pos.y - Screen.height / 2 + 1;
                text[counter].GetComponent<RectTransform>().localPosition = pos;
                if(sm.dummySelectableNumbers.Count != 0){
                    int randomNumber = UnityEngine.Random.Range(0, sm.dummySelectableNumbers.Count);
                    text[counter].GetComponent<Text>().text = sm.dummySelectableNumbers[randomNumber].ToString();
                    sm.dummySelectableNumbers.RemoveAt(randomNumber);
                }
                counter++;
            }
        }
    }

    public void InitDummyNumberView()
    {
        text = new GameObject[sm.dummyNum];
        GenerateDummyNumberView(sm.dummyNum);
    }

    private void GenerateDummyNumberView(int _num)
    {
        for(int i = 0; i < _num; i++)
        {
            text[i] = Instantiate(textPrefab) as GameObject;
            text[i].transform.SetParent(selectPanel.transform);
        }
    }

    public void DestroyDummyView()
    {
        for(int i = 0; i < text.Length; i++)
        {
            DestroyImmediate(text[i]);
        }
        Array.Clear(text, 0, text.Length);
    }
}
