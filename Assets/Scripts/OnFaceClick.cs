using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFaceClick : MonoBehaviour {

	public Camera camera;
	public GameObject cubePrefab;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

		RaycastHit hit;
		if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
			return;

		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (meshCollider == null || meshCollider.sharedMesh == null)
			return;

		Mesh mesh = meshCollider.sharedMesh;
		Vector3[] vertices = mesh.vertices;
		int[] triangles = mesh.triangles;
		Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
		Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
		Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
		Transform hitTransform = hit.collider.transform;
		p0 = hitTransform.TransformPoint(p0);
		p1 = hitTransform.TransformPoint(p1);
		p2 = hitTransform.TransformPoint(p2);
		Debug.DrawLine(p0, p1);
		Debug.DrawLine(p1, p2);
		Debug.DrawLine(p2, p0);
	}

	void OnMouseDown() {
		print("mouse down");

		RaycastHit hit;
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
		if (Physics.Raycast(ray, out hit)) {
			int index = hit.triangleIndex;
			print("Hit triangle index: " + index);

			if (index < 0) {
				print("ignore -1");
				return;
			} else if (index >= 12) {
				print("large index:" + index);
			}

			GameObject cubeClone = (GameObject) Instantiate(cubePrefab, hit.collider.transform.position, Quaternion.identity);

			if (index < 2) {
				cubeClone.transform.position += Vector3.forward;
			} else if (index < 4) {
				print("hit up");
				cubeClone.transform.position += Vector3.up;
			} else if (index < 6) {
				print("hit back");
				cubeClone.transform.position += Vector3.back;
			} else if (index < 8) {
				print("hit down");
				cubeClone.transform.position += Vector3.down;
			} else if (index < 10) {
				print("hit left");
				cubeClone.transform.position += Vector3.left;
			} else if (index < 12) {
				print("hit right");
				cubeClone.transform.position += Vector3.right;
			}
		}

	}
}
