using UnityEngine;

namespace Netologia.Gameplay
{
    [CreateAssetMenu(fileName = "CellPalette", menuName = "Settings/Cell Palette")]
    public class CellPaletteSettings : ScriptableObject
    {
        [SerializeField] private Material _selected;
        [SerializeField] private Material _moveOrAttack;
        [SerializeField] private Material _moveAndAttack;

        public Material Selected => _selected;
        public Material MoveOrAttack => _moveOrAttack;
        public Material MoveAndAttack => _moveAndAttack;
    }
}
