using UnityEngine;
namespace Netologia.Homework
{
 public class Gates : MonoBehaviour
 {
 private int _score;
 private void OnTriggerEnter(Collider other)
 {
 if (!other.TryGetComponent<Ball>(out _)) return;
 _score++;
 Destroy(other.gameObject);
 Debug.Log($"Score: {_score}");
 }
 }
}
