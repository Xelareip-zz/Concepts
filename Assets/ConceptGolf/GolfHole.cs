using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
	public float holeSize;
	public int holeDefinition;
	public Vector3 holePosition;
	public Vector3 levelSize;

	public MeshFilter filter;
	public MeshCollider meshCollider;
	public Mesh mesh;

	public List<Vector3> vertices = new List<Vector3>();
	public List<Vector3> normals = new List<Vector3>();
	public List<Vector2> uvs = new List<Vector2>();
	public List<int> triangles = new List<int>();
	public List<Vector2> polygon = new List<Vector2>();

	public bool update;
	public bool updateAlways;

	void Start()
	{
		if (Application.isPlaying)
		{
			mesh = new Mesh();
			mesh.MarkDynamic();
			mesh.name = gameObject.GetInstanceID().ToString();
			filter.mesh = mesh;
		}
		UpdateMesh();
	}

	void Update()
	{
		if (update || updateAlways)
		{
			UpdateMesh();
			update = false;
		}
	}


	public void UpdateMesh()
	{
		vertices.Clear();
		normals.Clear();
		uvs.Clear();
		triangles.Clear();
		polygon.Clear();

		List<Vector3> tempPoints = new List<Vector3>();

		int currentCorner = 0;

		vertices.Add(levelSize);
		vertices.Add(new Vector3(-levelSize.x, levelSize.y, 0.0f));
		vertices.Add(-levelSize);
		vertices.Add(new Vector3(levelSize.x, -levelSize.y, 0.0f));

		for (int holePointIdx = 0; holePointIdx < holeDefinition; ++holePointIdx)
		{
			vertices.Add(holeSize * new Vector3(Mathf.Cos(holePointIdx * 2.0f * Mathf.PI / holeDefinition), Mathf.Sin(holePointIdx * 2.0f * Mathf.PI / holeDefinition), 0.0f));
		}

		for (int pointIdx = 0; pointIdx < holeDefinition; ++pointIdx)
		{
			int vertexIdx = pointIdx + 4;
			int nextVertexIdx = ((pointIdx + 1) % holeDefinition) + 4;

			Vector3 point = vertices[vertexIdx];
			Vector3 nextPoint = vertices[nextVertexIdx];
			if (nextPoint.x == 0.0f ||
				nextPoint.y == 0.0f ||
					(point.x != 0.0f && 
					point.y != 0.0f && 
					(point.x * nextPoint.x < 0.0f || point.y * nextPoint.y < 0.0f)
					)
				)
			{
				triangles.Add(currentCorner);
				triangles.Add(vertexIdx);
				currentCorner = (currentCorner + 1) % 4;
				triangles.Add(currentCorner);
			}

			triangles.Add(vertexIdx);
			triangles.Add(nextVertexIdx);
			triangles.Add(currentCorner);
			
		}

		for (int i = 0; i < vertices.Count; ++i)
		{
			normals.Add(Vector3.back);
			uvs.Add(Vector2.zero);
		}

		if (Application.isPlaying)
		{
			// Avoids inconsistencies between triangles and vertices
			if (mesh.vertexCount != 0)
			{
				mesh.SetTriangles(new int[3] { 0, 0, 0 }, 0);
			}
			mesh.SetVertices(vertices);
			mesh.SetTriangles(triangles.ToArray(), 0);
			mesh.SetNormals(normals);
			mesh.SetUVs(0, uvs);
			mesh.RecalculateBounds();
			filter.mesh = mesh;
			meshCollider.sharedMesh = mesh;
		}
	}

	void OnDrawGizmos()
	{
		Vector3 drawLevelSize = transform.rotation * levelSize;

		Gizmos.DrawLine(transform.position + drawLevelSize, transform.position + new Vector3(-drawLevelSize.x, drawLevelSize.y, drawLevelSize.z));
		Gizmos.DrawLine(transform.position + drawLevelSize, transform.position - new Vector3(-drawLevelSize.x, drawLevelSize.y, drawLevelSize.z));
																															  
		Gizmos.DrawLine(transform.position - drawLevelSize, transform.position + new Vector3(-drawLevelSize.x, drawLevelSize.y, drawLevelSize.z));
		Gizmos.DrawLine(transform.position - drawLevelSize, transform.position - new Vector3(-drawLevelSize.x, drawLevelSize.y, drawLevelSize.z));

		for (int i = 0; i < holeDefinition; ++i)
		{
			Vector3 point =		holeSize * (transform.rotation * new Vector3(Mathf.Cos(i * Mathf.PI * 2.0f / holeDefinition)		, Mathf.Sin(i * Mathf.PI * 2.0f / holeDefinition)		, 0.0f));
			Vector3 nextPoint = holeSize * (transform.rotation * new Vector3(Mathf.Cos((i + 1) * Mathf.PI * 2.0f / holeDefinition)	, Mathf.Sin((i + 1) * Mathf.PI * 2.0f / holeDefinition)	, 0.0f));

			Gizmos.DrawLine(transform.position + point, transform.position + nextPoint);
		}
	}
}
