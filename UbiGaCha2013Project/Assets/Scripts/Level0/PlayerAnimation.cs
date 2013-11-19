using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	//vars for the whole sheet
	public int colCount =  6;
	public int rowCount =  5;
	
	//vars for animation
	public int  rowNumber  =  1; //Zero Indexed
	public int colNumber = 0; //Zero Indexed
	public int totalCells = 1;
	public int  fps     = 6;
	//Maybe this should be a private var
	private Vector2 offset;
	//Update

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SetParameters ();
		SetSpriteAnimation(colCount,rowCount,rowNumber,colNumber,totalCells,fps);
	}

	void SetParameters(){
		if (rigidbody2D.velocity.x > 0)
		{
			rowNumber = 1;
			colNumber = 0;
			totalCells = 6;
		}
		else if (rigidbody2D.velocity.x < 0)
		{
			rowNumber = 2;
			colNumber = 0;
			totalCells = 6;
		}
		else
		{
			rowNumber = 0;
			colNumber = 0;
			totalCells = 1;
		}
	}

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
}
