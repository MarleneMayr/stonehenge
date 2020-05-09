using UnityEngine;

namespace Bricks
{
    [System.Serializable]
    public class RecipeBrick : IBrick
    {
        [SerializeField] private ID identifier;
        public ID GetID() { return identifier; }

        [SerializeField] private Voxel[] voxels;
        public virtual Voxel[] GetVoxels() { return voxels; }

        public RecipeBrick(ID id = ID.white, Voxel[] voxels = null)
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