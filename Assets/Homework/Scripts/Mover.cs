using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
	[RequireComponent(typeof(Rigidbody))]
	public class Mover : MonoBehaviour
	{
		[SerializeField]
		private Vector3 _start;
		[SerializeField]
		private Vector3 _end;
		[SerializeField]
		private float _speed = 1f;
		[SerializeField]
		private float _delay = 2f;

		private readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
		private WaitForSeconds _waitForDelay;
		private Rigidbody _body;
		private Vector3 _origin;

		private IEnumerator Start()
		{
			_body = GetComponent<Rigidbody>();
			_origin = _body.position;
			_waitForDelay = new WaitForSeconds(_delay);
			while (true)
			{
				yield return MoveBetween(_origin + _start, _origin + _end);
				yield return MoveBetween(_origin + _end, _origin + _start);
			}
		}

		private IEnumerator MoveBetween(Vector3 from, Vector3 to)
		{
			var duration = Vector3.Distance(from, to) / _speed;
			var time = 0f;
			while (time < duration)
			{
				yield return _waitForFixedUpdate;
				time += Time.fixedDeltaTime;
				_body.MovePosition(Vector3.Lerp(from, to, time / duration));
			}
			yield return _waitForDelay;
		}

		private void OnDrawGizmos()
		{
			var origin = Application.isPlaying ? _origin : transform.position;
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere(origin + _start, 0.5f);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(origin + _end, 0.5f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(origin + _start, origin + _end);
		}
	}
}
