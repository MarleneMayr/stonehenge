using UnityEngine;

namespace Bricks
{
    [System.Serializable]
    public class RecipeBrick : IBrick
    {
        [SerializeField] private int identifier;
        public int GetID() { return identifier; }

        [SerializeField] private Voxel[] voxels;
        public virtual Voxel[] GetVoxels() { return voxels; }

        public RecipeBrick(int id = 0, Voxel[] voxels = null)
        {
            identifier = id;
            this.voxels = voxels;
        }

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