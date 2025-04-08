using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private Vector3 A;
	[SerializeField] private Vector3 B;
	private bool isA = true;
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			isA=!isA;
			if (isA)
				Rotate(A, 1);
			else
				Rotate(B, -1);
		}
    }
	private void Rotate(Vector3 target, float factor)
	{
		StartCoroutine(RotateSmooth(target, factor));
	}
	
	private IEnumerator RotateSmooth(Vector3 target, float factor)
	{
		///ФАКТОР!!!
		var diff = factor * (target - transform.eulerAngles) * .025f;
		
		for (float t=0; t<1; t+=.025f)
		{
			yield return new WaitForSeconds(.025f);
			
			transform.eulerAngles -= diff;
		}
	}
}
