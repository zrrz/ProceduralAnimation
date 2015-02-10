﻿using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInput {
	
	public override void Start () {
	
	}

	public override void Update () {
		dir = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));
		sprint = Input.GetButton("Sprint");
		jump = Input.GetButtonDown("Jump");
	}
}
