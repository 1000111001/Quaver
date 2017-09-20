﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quaver.Gameplay
{
    public partial class PlayScreen
    {
        private Text[] _maText;
        private GameObject _maDisplay;

        private void ma_init()
        {
            _maDisplay = uiCanvas.transform.Find("maInfo").gameObject;
            _maText = new Text[9];
            _maText[0] = _maDisplay.transform.Find("Marv").GetComponent<Text>();
            _maText[1] = _maDisplay.transform.Find("Perf").GetComponent<Text>();
            _maText[2] = _maDisplay.transform.Find("Great").GetComponent<Text>();
            _maText[3] = _maDisplay.transform.Find("Good").GetComponent<Text>();
            _maText[4] = _maDisplay.transform.Find("Bad").GetComponent<Text>();
            _maText[5] = _maDisplay.transform.Find("Miss").GetComponent<Text>();
            _maText[6] = _maDisplay.transform.Find("Ace").GetComponent<Text>();
            _maText[7] = _maDisplay.transform.Find("Early").GetComponent<Text>();
            _maText[8] = _maDisplay.transform.Find("Late").GetComponent<Text>();
        }

        private void ma_Update(int toChange)
        {
            _maText[toChange].text = _ScoreSpread[toChange].ToString();
        }
    }
}
