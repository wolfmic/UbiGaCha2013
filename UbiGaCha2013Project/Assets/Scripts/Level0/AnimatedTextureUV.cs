﻿using UnityEngine;
using System.Collections;

public class AnimatedTextureUV : MonoBehaviour
{
	public bool lastBell;
	public int rank;
	public float timerMax = 5;
	public GameObject character;
	private bool interactionPossible = false;
	private bool running = false;
	private bool levelClear = false;
	public float timer;
	
	//vars for the whole sheet
	public int colCount =  3;
	public int rowCount =  3;
	
	//vars for animation
	public int  rowNumber  =  0; //Zero Indexed
	public int colNumber = 0; //Zero Indexed
	public int totalCells = 1;
	public int  fps     = 10;
	//Maybe this should be a private var
	private Vector2 offset;
	//Update
	void Update () {
		Interact();
		RunAction ();
		SetSpriteAnimation(colCount,rowCount,rowNumber,colNumber,totalCells,fps);
	}

	void SetAnimation(bool activation) {
		if (activation)
		{
			totalCells = 3;
		}
		else
		{
			totalCells = 1;
		}
	}
	
	//SetSpriteAnimation
	void SetSpriteAnimation(int colCount ,int rowCount ,int rowNumber ,int colNumber,int totalCells,int fps ){
		
		// Calculate index
		int index  = (int)(Time.time * fps);
		// Repeat when exhausting all cells
		index = index % totalCells;
		
		// Size of every cell
		float sizeX = 1.0f / colCount;
		float sizeY = 1.0f / rowCount;
		Vector2 size =  new Vector2(sizeX,sizeY);
		
		// split into horizontal and vertical index
		var uIndex = index % colCount;
		var vIndex = index / colCount;
		
		// build offset
		// v coordinate is the bottom of the image in opengl so we need to invert.
		float offsetX = (uIndex+colNumber) * size.x;
		float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
		Vector2 offset = new Vector2(offsetX,offsetY);
		
		renderer.material.SetTextureOffset ("_MainTex", offset);
		renderer.material.SetTextureScale  ("_MainTex", size);
	}

	void RunAction() {
		if (running)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				running = false;
				SetAnimation(false);
				character.SendMessage("SetFreeze",false);
			}
		}
	}
	
	void Interact() {
		if ((interactionPossible) && (!levelClear))
		{
			if ((Input.GetKey(KeyCode.E)) && (!running))
			{
				timer = timerMax;
				running = true;
				SetAnimation(true);
				character.SendMessage("SetFreeze",true);
				character.SendMessage("SetRank",rank);
				character.SendMessage("SetLastBell",lastBell);
			}
		}
	}

	void SetLevelClear() {
		levelClear = true;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other == character.collider2D)
		{
			interactionPossible = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if (other == character.collider2D)
		{
			interactionPossible = false;
		}
	}
}
