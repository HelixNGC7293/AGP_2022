using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    CameraRayController cameraRayController;
    NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        //Touch tracking position
        Camera camera = Camera.main;
        cameraRayController = camera.GetComponent<CameraRayController>();
        cameraRayController.onTouchPoint += CameraNewTouchPosition;

        nav = GetComponent<NavMeshAgent>();
    }


	private void OnDestroy()
	{
        cameraRayController.onTouchPoint -= CameraNewTouchPosition;
    }

	void CameraNewTouchPosition(Vector3 pos)
	{
        nav.SetDestination(pos);

    }
}
