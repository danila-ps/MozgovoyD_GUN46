using System;
using System.Collections.Generic;
using UnityEngine;

namespace Netologia.Gameplay
{
    public class CellManager : MonoBehaviour
    {
        [SerializeField] private CellPaletteSettings _palette;

        public event Action<Cell> OnCellClicked;

        private readonly List<Cell> _cells = new List<Cell>();
        private readonly HashSet<Cell> _selected = new HashSet<Cell>();

        public IReadOnlyList<Cell> Cells => _cells;

        private void Awake()
        {
            _cells.AddRange(FindObjectsOfType<Cell>());

            foreach (var cell in _cells)
            {
                cell.OnPointerClickEvent += HandleCellClicked;
                InitNeighbours(cell);
            }

            foreach (var unit in FindObjectsOfType<Unit>())
            {
                var cell = FindNearestCell(unit.transform.position);
                unit.Cell = cell;
                cell.Unit = unit;
            }
        }

        private void OnDestroy()
        {
            foreach (var cell in _cells)
                cell.OnPointerClickEvent -= HandleCellClicked;
        }

        private void HandleCellClicked(Cell cell)
        {
            if (_palette != null)
            {
                if (!_selected.Add(cell))
                {
                    _selected.Remove(cell);
                    cell.ResetSelect();
                }
                else
                {
                    cell.SetSelect(_palette.Selected);
                }
            }

            OnCellClicked?.Invoke(cell);
        }

        private void InitNeighbours(Cell cell)
        {
            foreach (var other in _cells)
            {
                if (other == cell)
                    continue;

                var delta = other.transform.position - cell.transform.position;
                var type = NeighbourType.None;

                if (Mathf.Approximately(delta.x, -1f)) type |= NeighbourType.Left;
                if (Mathf.Approximately(delta.x, 1f)) type |= NeighbourType.Right;
                if (Mathf.Approximately(delta.z, 1f)) type |= NeighbourType.Top;
                if (Mathf.Approximately(delta.z, -1f)) type |= NeighbourType.Bottom;

                if (type != NeighbourType.None)
                    cell.Neighbours[type] = other;
            }
        }

        private Cell FindNearestCell(Vector3 position)
        {
            Cell nearest = null;
            var nearestDistance = float.MaxValue;

            foreach (var cell in _cells)
            {
                var distance = (cell.transform.position - position).sqrMagnitude;
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = cell;
                }
            }

            return nearest;
        }
    }
}
