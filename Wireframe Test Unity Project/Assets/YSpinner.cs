using UnityEngine;
using System.Collections;

public class YSpinner : MonoBehaviour
{
	public float RATE = 10.0f;

	Vector3 rotation = Vector3.zero;
		
	// Update is called once per frame
	void Update ()
	{
		rotation.y += RATE * Time.deltaTime;
		this.transform.eulerAngles = rotation;

		if (Input.GetMouseButtonDown(0))
		{
			rotation.y += 180.0f;
			this.transform.eulerAngles = rotation;
			Application.Quit();
		}
	}
}
