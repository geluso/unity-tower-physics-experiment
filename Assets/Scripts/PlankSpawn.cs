using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankSpawn : MonoBehaviour {

	public GameObject originPlank;
	public GameObject plankPrefab;
	public int TowerHeight;

	float length;
	float width;
	float height;

	// Use this for initialization
	void Start () {
		length = plankPrefab.transform.localScale.x;
		width = plankPrefab.transform.localScale.z;
		height = plankPrefab.transform.localScale.y;

		GameObject oldCenter = originPlank;
		GameObject oldLeft = createNewLeft(oldCenter);
		GameObject oldRight = createNewRight(oldCenter);

		float rotationIncrement = 90f;
		float rotation = rotationIncrement;
		Vector3 elevationIncrement = Vector3.up * height;

		for (int i = 0; i < TowerHeight; i++) {
			oldLeft = createNewLeft(oldCenter);
			oldCenter = createNewCenter(oldCenter);
			oldRight = createNewRight(oldCenter);

			oldLeft.transform.position += elevationIncrement;
			oldCenter.transform.position += elevationIncrement;
			oldRight.transform.position += elevationIncrement;

			oldLeft.transform.RotateAround(oldCenter.transform.position, Vector3.up, rotation);
			oldCenter.transform.RotateAround(oldCenter.transform.position, Vector3.up, rotation);
			oldRight.transform.RotateAround(oldCenter.transform.position, Vector3.up, rotation);

			rotation += rotationIncrement;
		}
	}

	GameObject createNewLeft(GameObject oldCenter) {
		Vector3 left = oldCenter.transform.position + (2 * width) * Vector3.forward;
		GameObject newLeft = (GameObject)Instantiate(plankPrefab, left, Quaternion.identity);
		return newLeft;
	}

	GameObject createNewRight(GameObject oldCenter) {
		Vector3 right = oldCenter.transform.position + (2 * width) * Vector3.back;
		GameObject newRight = (GameObject)Instantiate(plankPrefab, right, Quaternion.identity);
		return newRight;
	}

	GameObject createNewCenter(GameObject oldCenter) {
		Vector3 center = oldCenter.transform.position;
		GameObject newCenter = (GameObject)Instantiate(plankPrefab, center, Quaternion.identity);
		return newCenter;
	}

	GameObject createRaisedPlank(GameObject oldCenter) {
		return null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
