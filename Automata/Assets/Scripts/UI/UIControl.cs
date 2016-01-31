using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIControl : MonoBehaviour {

	public GameObject pathPrefab;

	private int uiState = 0;
	private Vector2[] editingPoints;
    private bool editing = false;
	private UIObject wipObject;

    private Renderer pathPreviewRenderer;
	private World m_World;
	private PathObject m_PathPreview;

	private Vector2 mousePos;

	// Use this for initialization
	void Start () {
		GameObject camera = GameObject.FindWithTag("MainCamera");
		m_World = camera.GetComponent<World>();
		GameObject pPrev = Instantiate(pathPrefab);
		m_PathPreview = pPrev.GetComponent<PathObject>();

		pathPreviewRenderer = m_PathPreview.GetComponent<Renderer>();
        pathPreviewRenderer.enabled = false;

		editingPoints = new Vector2[2]{new Vector2(-1,-1),new Vector2(-1,-1)};
        this.transform.position = new Vector3(this.transform.position[0], this.transform.position[1], -1);
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        mousePos = new Vector2(mouseWorldPos[0], mouseWorldPos[1]);
        if (editing) {
            editingPoints[1] = mousePos;
			m_PathPreview.transform.position = new Vector3(editingPoints[0].x, editingPoints[0].y, 0);
			m_PathPreview.setShape(wipObject.toPath(editingPoints));
        }
    }

	void OnMouseUp() {
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		mousePos = new Vector2(mouseWorldPos[0], mouseWorldPos[1]);
		if (editing) {
			editingPoints [1] = mousePos;
			wipObject.toAdherent(m_World.Add(new Vector3(editingPoints[0].x,editingPoints[0].y,0)),editingPoints);
            m_World.FindIntersections();
            uiState = (int)uiStates.None;
            Cursor.SetCursor(m_World.cursors[0], new Vector2(20, 20), CursorMode.Auto);
            editingPoints = new Vector2[2]{new Vector2(-1,-1),new Vector2(-1,-1)};
			editing = false;
			pathPreviewRenderer.enabled = false;
		} else {
            Adherent adherent = m_World.FindNearestAdherent(mousePos, 20);
            if (adherent != null) {
                m_World.Remove(adherent);
            }
        }
	}
	void OnMouseDown(){
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		mousePos = new Vector2(mouseWorldPos[0], mouseWorldPos[1]);
		if (uiState != (int)uiStates.None && !editing) {
			editingPoints[0] = mousePos;
			editing = true;
			pathPreviewRenderer.enabled = true;
		}
	}

	public void rectangleCreator(){
		uiState = (int)uiStates.createRectangle;
		wipObject = (UIObject)new UIRectangle ();
        Cursor.SetCursor(m_World.cursors[3], new Vector2(32, 32), CursorMode.Auto);
    }

    public void circleCreator(){
		uiState = (int)uiStates.createCircle;
		wipObject = (UIObject)new UICircle ();
        Cursor.SetCursor(m_World.cursors[4], new Vector2(32, 32), CursorMode.Auto);
    }

    public void diamondCreator(){
		uiState = (int)uiStates.createDiamond;
		wipObject = (UIObject)new UIDiamond ();
        Cursor.SetCursor(m_World.cursors[2], new Vector2(32, 32), CursorMode.Auto);
    }

    public void triangleCreator(){
		uiState = (int)uiStates.createTriangle;
		wipObject = (UIObject)new UITriangle ();
        Cursor.SetCursor(m_World.cursors[1], new Vector2(32, 32), CursorMode.Auto);
    }

    public enum uiStates {
		None = 0,
		createRectangle = 1,
		createCircle = 2,
		createDiamond = 3,
		createTriangle = 4,
	}
}
