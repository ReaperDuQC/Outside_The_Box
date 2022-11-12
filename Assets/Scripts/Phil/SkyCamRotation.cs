using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyCamRotation : MonoBehaviour
{
    [SerializeField] Vector3 axe;
    [SerializeField] float speed;
    [SerializeField] bool fovVariation;
    [SerializeField] float minFov;
    [SerializeField] float maxFov;
    [SerializeField] float fovSpeed;

    Camera camera;
    float currentFov;
    bool fovUp;

	private void Awake()
	{
        camera = GetComponent<Camera>();
        currentFov = camera.fieldOfView;

        if (currentFov <= minFov)
        {
            fovUp = true;
        }
    }

	void Update()
    {
        transform.Rotate(axe, speed * Time.deltaTime);

        if (fovVariation)
        {
            if (fovUp)
            {
                //currentFov += Time.deltaTime * fovSpeed;

                //if (currentFov > maxFov)
                //{
                //    currentFov = maxFov;
                //    fovUp = false;
                //}
                currentFov = maxFov;
                camera.fieldOfView = currentFov;
                fovUp = false;
            }
            else
            {
                currentFov -= Time.deltaTime * fovSpeed;

                if (currentFov < minFov)
                {
                    currentFov = minFov;
                    fovUp = true;
                }

                camera.fieldOfView = currentFov;
            }
        }
    }
}
