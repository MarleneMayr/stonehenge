//#define CONVERT

using UnityEngine;

namespace Bricks
{
    [System.Serializable]
    public class RecipeBrick : IBrick
    {
        [SerializeField] private int identifier;
        public int GetID() { return identifier; }

#if CONVERT
        [SerializeField] private Vector3[] coordinates;
        private Voxel[] voxels;
#else
        [SerializeField] private Voxel[] voxels;
#endif
        public virtual Voxel[] GetVoxels()
        {
#if CONVERT
            if (voxels.Length == 0)
            {
                voxels = GetVoxelsFromCoordinates();
            }
#endif
            return voxels;
        }

        public RecipeBrick(int id = 0, Voxel[] voxels = null)
        {
            identifier = id;
            this.voxels = voxels;
#if CONVERT
            coordinates = GetCoordinatesFromVoxels(voxels);
#endif
        }

#if CONVERT
        private Vector3[] GetCoordinatesFromVoxels(Voxel[] voxels)
        {
            var newCoordinates = new Vector3[voxels.Length];
            for (int i = 0; i < newCoordinates.Length; i++)
            {
                newCoordinates[i] = voxels[i].Coordinates;
            }
            return newCoordinates;
        }

        private Voxel[] GetVoxelsFromCoordinates()
        {
            var voxels = new Voxel[coordinates.Length];
            for (int i = 0; i < voxels.Length; i++)
            {
                voxels[i] = new Voxel(Vector3Int.RoundToInt(coordinates[i]));
            }
            return voxels;
        }
#endif

        public override string ToString()
        {
            if (voxels == null) return "NaN";
            string result = "";
            foreach (var v in voxels)
            {
                result += $"{v.ToString()}";
            }
            return result;
        }
    }
}