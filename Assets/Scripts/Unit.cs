using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Netologia.Gameplay
{
    public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public event Action OnMoveEndCallback;

        [SerializeField] private float _speed = 3f;
        [SerializeField] private Team _team;

        public Cell Cell { get; set; }

        public Team Team => _team;

        private Cell _targetCell;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Cell != null)
                Cell.OnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Cell != null)
                Cell.OnPointerExit(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Cell != null)
                Cell.OnPointerClick(eventData);
        }

        public void Move(Cell cell)
        {
            _targetCell = cell;
        }

        private void Update()
        {
            if (_targetCell == null)
                return;

            var targetPosition = new Vector3(
                _targetCell.transform.position.x,
                transform.position.y,
                _targetCell.transform.position.z);

            transform.position = Vector3.MoveTowards(
                transform.position, targetPosition, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                Cell = _targetCell;
                _targetCell = null;
                OnMoveEndCallback?.Invoke();
            }
        }
    }
}
