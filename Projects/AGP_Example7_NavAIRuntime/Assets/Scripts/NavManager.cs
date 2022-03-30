using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavManager : MonoBehaviour
{
    [SerializeField]
    GameObject prefabCube;

    NavMeshSurface navMeshSurface;

    CameraRayController cameraRayController;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        cameraRayController = camera.GetComponent<CameraRayController>();
        cameraRayController.onCreateCubePoint += CreateCubeOnPoint;
        navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.BuildNavMesh();
    }

    // Update is called once per frame
    void CreateCubeOnPoint(Vector3 pos)
    {
        GameObject cube = Instantiate(prefabCube, transform);
        cube.transform.position = pos;
        navMeshSurface.BuildNavMesh();
    }
}
