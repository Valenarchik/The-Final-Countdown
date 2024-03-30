using UnityEngine;

namespace CountDown
{
    public class Cell: MonoBehaviour
    {
        [SerializeField] private CellType cellType;
        public CellType CellType => cellType;
    }
}