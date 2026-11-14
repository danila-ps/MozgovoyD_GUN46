using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Netologia.Gameplay
{
    public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public event Action<Cell> OnPointerClickEvent;

        [SerializeField] private MeshRenderer _focusMesh;
        [SerializeField] private MeshRenderer _selectMesh;

        public Unit Unit { get; set; }
        public Dictionary<NeighbourType, Cell> Neighbours { get; } = new Dictionary<NeighbourType, Cell>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            _focusMesh.enabled = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _focusMesh.enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke(this);
        }

        public void SetSelect(Material material)
        {
            _selectMesh.enabled = true;
            _selectMesh.sharedMaterial = material;
        }

        public void ResetSelect()
        {
            _selectMesh.enabled = false;
        }
    }
}
