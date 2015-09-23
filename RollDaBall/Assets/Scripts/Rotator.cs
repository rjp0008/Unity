using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    private static int RotatorIdTracker = 0;
    private int id;

    Rotator()
    {
        id = RotatorIdTracker;
        RotatorIdTracker++;
    }

	void Update () {
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
