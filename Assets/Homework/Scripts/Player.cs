using System.Collections;
using UnityEngine;

namespace Netologia.Homework
{
	public class Player : MonoBehaviour
	{
		[SerializeField]
		private Ball _ballPrefab;
		[SerializeField]
		private float _startVelocity;
		[SerializeField]
		private float _respawnDelay;

		private Ball _ball;
		private bool _ready;

		private void Start()
		{
			Spawn();
		}

		private void Update()
		{
			if (!_ready) return;
			if (Input.GetKey(KeyCode.Space))
			{
				_ball.Launch(transform.forward * _startVelocity);
				StartCoroutine(Reloader());
			}
		}

		private IEnumerator Reloader()
		{
			_ready = false;
			yield return new WaitForSeconds(_respawnDelay);
			Spawn();
		}

		private void Spawn()
		{
			_ball = Instantiate(_ballPrefab, transform);
			_ball.Hold();
			_ready = true;
		}
	}
}
