﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMoney : MonoBehaviour {

	Text moneyText;
	// Use this for initialization
	void Start () {

		moneyText = GetComponent<Text> ();
		moneyText.text = "" + PlayerPrefs.GetInt ("Cash");

	}
	

}
