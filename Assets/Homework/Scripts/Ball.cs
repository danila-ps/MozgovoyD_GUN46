using UnityEngine;

namespace Netologia.Homework
{
	[RequireComponent(typeof(Rigidbody))]
	public class Ball : MonoBehaviour
	{
		[SerializeField]
		private float _lifetime;

		private Rigidbody _body;

		private void Awake()
		{
			_body = GetComponent<Rigidbody>();
		}

		public void Hold()
		{
			_body.isKinematic = true;
		}

		public void Launch(Vector3 velocity)
		{
			transform.parent = null;
			_body.isKinematic = false;
			_body.velocity = velocity;
			Destroy(gameObject, _lifetime);
		}
	}
}
