using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIControl : MonoBehaviour {

	public int uiState = 0;
	public Vector2[] editingPoints;
	private UIObject wipObject;

	public GameObject worldContainer;
	public static World world;

	private Vector2 mousePos;

	// Use this for initialization
	void Start () {
		world = worldContainer.GetComponent<World> ();
		editingPoints = new Vector2[2]{new Vector2(-1,-1),new Vector2(-1,-1)};
	}
	
	// Update is called once per frame
	/*void FixedUpdate () {
		mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		if (Input.GetKeyDown (KeyCode.Mouse0) && uiState != (int)uiStates.None && editingPoints[0].x == -1) {
			editingPoints[0] = mousePos;
		}
		if (Input.GetKeyUp (KeyCode.Mouse0) && editingPoints[1].x == -1 && editingPoints[0].x != -1) {
			editingPoints [1] = mousePos;
			wipObject.toAdherent(world.Add(new Vector3(editingPoints[0].x,editingPoints[0].y,0)),editingPoints);
			uiState = (int)uiStates.None;
			editingPoints = new Vector2[2]{new Vector2(-1,-1),new Vector2(-1,-1)};
		}
	}*/

	void OnMouseUp() {
		mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		if (editingPoints [1].x == -1 && editingPoints [0].x != -1) {
			editingPoints [1] = mousePos;
			wipObject.toAdherent(world.Add(new Vector3(editingPoints[0].x,editingPoints[0].y,0)),editingPoints);
			uiState = (int)uiStates.None;
			editingPoints = new Vector2[2]{new Vector2(-1,-1),new Vector2(-1,-1)};
		}
	}

	void OnMouseDown(){
		mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		if (uiState != (int)uiStates.None && editingPoints[0].x == -1) {
			editingPoints[0] = mousePos;
		}
	}

	public void rectangleCreator(){
		uiState = (int)uiStates.createRectangle;
		wipObject = (UIObject)new UIRectangle ();
	}

	public void circleCreator(){
		uiState = (int)uiStates.createCircle;
		wipObject = (UIObject)new UICircle ();
	}

	public void diamondCreator(){
		uiState = (int)uiStates.createDiamond;
		wipObject = (UIObject)new UIDiamond ();
	}

	public void triangleCreator(){
		uiState = (int)uiStates.createTriangle;
		wipObject = (UIObject)new UITriangle ();
	}

	public enum uiStates {
		None = 0,
		createRectangle = 1,
		createCircle = 2,
		createDiamond = 3,
		createTriangle = 4,
	}
}
