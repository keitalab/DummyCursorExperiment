﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExAmountoFCursorSettings : MonoBehaviour
{
    public InputField inputField;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        inputField = inputField.GetComponent<InputField>();
        text = text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputNum() {
        text.text = inputField.text;
    }
}
