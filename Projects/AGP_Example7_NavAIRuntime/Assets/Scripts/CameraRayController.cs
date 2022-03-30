using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayController : MonoBehaviour
{
    [SerializeField]
    LayerMask rayLayerMask_Floor;
    Camera mainCamera;

    public delegate void OnTouchPoint(Vector3 pos);
	public OnTouchPoint onTouchPoint;

	public OnTouchPoint onCreateCubePoint;

	// Start is called before the first frame update
	void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
	{
		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			Vector2 touchUpPos = Input.mousePosition;
			Ray currentRay = mainCamera.ScreenPointToRay(touchUpPos);
			RaycastHit hit;
			if (Physics.Raycast(currentRay, out hit, 3000, rayLayerMask_Floor))
			{
				if (Input.GetMouseButtonUp(0))
				{
					onTouchPoint.Invoke(hit.point);
				}
				else if (Input.GetMouseButtonUp(1))
				{
					onCreateCubePoint.Invoke(hit.point);
				}
			}
		}
    }
}
