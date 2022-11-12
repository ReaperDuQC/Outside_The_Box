using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	[SerializeField] float delaisMove;
	[SerializeField] bool isMoving;
	 Vector3 initialPos;
	[SerializeField] float leftTranslation;
	[SerializeField] float rightTranslation;
	[SerializeField] bool startMoveR = true;
	bool goR;



	private void Start()
	{
		initialPos = transform.position;
		if (isMoving)
		{
			if (startMoveR)
			{
				goR = true;
			}
		}
	}

	private void Update()
	{
		if (delaisMove > 0)
		{
			delaisMove -= Time.deltaTime;
		}
		else if (isMoving)
		{
			switch (goR)
			{
				case true:
					if (transform.position.x < initialPos.x + rightTranslation)
					{
						transform.position += new Vector3(rightTranslation, 0, 0) * Time.deltaTime * 0.2f;
					}
					else
					{
						goR = false;
					}
					break;
				case false:
					if (transform.position.x > initialPos.x + leftTranslation)
					{
						transform.position += new Vector3(leftTranslation, 0, 0) * Time.deltaTime * 0.2f;
					}
					else
					{
						goR = true;
					}
					break;
			}
		}
	}
}
