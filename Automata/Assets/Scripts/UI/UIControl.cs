﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIControl : MonoBehaviour {

	private int uiState = 0;
	private Vector2[] editingPoints;
    private bool editing = false;
	private UIObject wipObject;

    public GameObject worldContainer;
    public PathObject pathPreview;
    private Renderer pathPreviewRenderer;
    public static World world;

	private Vector2 mousePos;

	// Use this for initialization
	void Start () {
		world = worldContainer.GetComponent<World> ();
		editingPoints = new Vector2[2]{new Vector2(-1,-1),new Vector2(-1,-1)};

        pathPreviewRenderer = pathPreview.GetComponent<Renderer>();
        pathPreviewRenderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        mousePos = new Vector2(mouseWorldPos[0], mouseWorldPos[1]);
        if (Input.GetKeyDown (KeyCode.Mouse0) && uiState != (int)uiStates.None && !editing) {
			editingPoints[0] = mousePos;
            editing = true;
            pathPreviewRenderer.enabled = true;
        }
		if (Input.GetKeyUp (KeyCode.Mouse0) && editing) {
            editingPoints[1] = mousePos;
            wipObject.toAdherent(world.Add(new Vector3(editingPoints[0].x,editingPoints[0].y,0)),editingPoints);
			uiState = (int)uiStates.None;
			editingPoints = new Vector2[2]{new Vector2(-1,-1),new Vector2(-1,-1)};
            editing = false;
            pathPreviewRenderer.enabled = false;
        }
        if (editing) {
            editingPoints[1] = mousePos;
            pathPreview.transform.position = new Vector3(editingPoints[0].x, editingPoints[0].y, 0);
            pathPreview.setShape(Path.createRect(0, 0, editingPoints[1].x - editingPoints[0].x, editingPoints[1].y - editingPoints[0].y));
        }
    }

	public void rectangleCreator(){
		uiState = (int)uiStates.createRectangle;
		wipObject = (UIObject)new UIRectangle ();
	}

	public enum uiStates {
		None = 0,
		createRectangle = 1,
	}
}
