using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
	[RequireComponent(typeof(Rigidbody))]
	public class Rotator : MonoBehaviour
	{
		[SerializeField]
		private Vector3 _rotate;

		private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

		private IEnumerator Start()
		{
			var body = GetComponent<Rigidbody>();
			while (true)
			{
				yield return _waitForFixedUpdate;
				var delta = Quaternion.Euler(_rotate * Time.fixedDeltaTime);
				body.MoveRotation(body.rotation * delta);
			}
		}
	}
}
