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
		length = plankPrefab.transform.localScale.x;
		width = plankPrefab.transform.localScale.z;
		height = plankPrefab.transform.localScale.y;

		if (isGrid) {
			GenerateTowerGrid(originPlank);
		} else {
			GenerateTallTower(originPlank.transform, TowerHeight);
		}
	}

	void GenerateTowerGrid(Transform origin) {
		//GenerateTallTower(origin);

		Vector3 originalPosition = new Vector3(origin.position.x, origin.position.y, origin.position.z);
		float currentHeight = TowerHeight;

		for (int x = -gridWidth; x < gridWidth; x++) {
			for (int z = -gridLength; z < gridLength; z++) {
				Vector3 translated = originalPosition + (Vector3.right * x * width + Vector3.forward * z * length);
				origin.position = translated;
				GenerateTallTower(origin, currentHeight);
			}
		}

		origin.position = originalPosition;
	}

	void GenerateTallTower(Transform origin, float height) {
		Transform oldCenter = origin;
		Transform oldLeft = createNewLeft(oldCenter);
		Transform oldRight = createNewRight(oldCenter);

		float rotationIncrement = 90f;
		float rotation = rotationIncrement;
		Vector3 elevationIncrement = Vector3.up * height;

		for (int i = 0; i < height; i++) {
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

	Transform createNewLeft(Transform center) {
		Transform plank = createPlank(center);
		createPlank(center).position += (2 * width) * Vector3.forward;
		return plank;
	}

	Transform createNewRight(Transform center) {
		Transform plank = createPlank(center);
		createPlank(center).position += (2 * width) * Vector3.back;
		return plank;
	}

	Transform createNewCenter(Transform center) {
		return createPlank(center);
	}

	Transform createPlank(Transform center){
		GameObject plank = (GameObject)Instantiate(plankPrefab, center.position, Quaternion.identity);
		return plank.transform;
	}
}
