using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankSpawn : MonoBehaviour {
	public Transform originPlank;
	public GameObject plankPrefab;
	public int TowerHeight;

	public bool isGrid;
	public int gridWidth;
	public int gridLength;
	public float gridHeightDecay;

	float length;
	float width;
	float height;

	// Use this for initialization
	void Start () {
		if (isGrid) {
			GenerateTowerGrid(originPlank);
		} else {
			GenerateTallTower(originPlank.transform);
		}
	}

	void GenerateTowerGrid(Transform origin) {
		GenerateTallTower(origin);

		for (int x = -gridWidth / 2; x < gridWidth / 2; x++) {
			for (int z = -gridLength / 2; z < gridLength / 2; z++) {

			}
		}
	}

	void GenerateTallTower(Transform origin) {
		length = plankPrefab.transform.localScale.x;
		width = plankPrefab.transform.localScale.z;
		height = plankPrefab.transform.localScale.y;

		Transform oldCenter = origin;
		Transform oldLeft = createNewLeft(oldCenter);
		Transform oldRight = createNewRight(oldCenter);

		float rotationIncrement = 90f;
		float rotation = rotationIncrement;
		Vector3 elevationIncrement = Vector3.up * height;

		for (int i = 0; i < TowerHeight; i++) {
			oldLeft = createNewLeft(oldCenter);
			oldCenter = createNewCenter(oldCenter);
			oldRight = createNewRight(oldCenter);

			oldLeft.position += elevationIncrement;
			oldCenter.position += elevationIncrement;
			oldRight.position += elevationIncrement;

			oldLeft.RotateAround(oldCenter.transform.position, Vector3.up, rotation);
			oldCenter.RotateAround(oldCenter.transform.position, Vector3.up, rotation);
			oldRight.RotateAround(oldCenter.transform.position, Vector3.up, rotation);

			rotation += rotationIncrement;
		}
	}

	Transform createNewLeft(Transform oldCenter) {
		Transform newLeft = createPlank(oldCenter);
		newLeft.position += (2 * width) * Vector3.forward;
		return newLeft;
	}

	Transform createNewRight(Transform oldCenter) {
		Transform newRight = createPlank(oldCenter);
		newRight.position += (2 * width) * Vector3.back;
		return newRight;
	}

	Transform createNewCenter(Transform oldCenter) {
		return createPlank(oldCenter);
	}

	Transform createPlank(Transform oldCenter){
		Vector3 center = oldCenter.transform.position;
		GameObject newPlank = (GameObject)Instantiate(plankPrefab, center, Quaternion.identity);
		return newPlank.transform;
	}
}
