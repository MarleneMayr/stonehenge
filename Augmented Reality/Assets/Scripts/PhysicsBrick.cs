using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Bricks
{
    public class PhysicsBrick : MonoBehaviour, IBrick
    {
        [Serializable] public class PhysicsBrickEvent : UnityEvent<PhysicsBrick> { }
        public PhysicsBrickEvent OnBrickFellDown;

        [SerializeField] private int identifier;
        public int GetID() { return identifier; }

        private Voxel[] voxels;
        public VoxelReference reference;

        [SerializeField] private Transform[] anchors;
        private static int anchorsSize = 3;

        private void OnValidate()
        {
            if (anchors.Length != anchorsSize)
            {
                Debug.LogWarning("Don't change the 'anchors' array size!");
                Array.Resize(ref anchors, anchorsSize);
            }
        }

        private void Update()
        {
            if (transform.position.y < -1)
            {
                OnBrickFellDown?.Invoke(this);
            }
        }

        public void SetReferenceBrick(VoxelReference referenceBrick)
        {
            reference = referenceBrick;
        }

        public void SetID(int id, Color color)
        {
            identifier = id;

            /*
             * An explanation about material problems:
             * For some reason Unity returns an Array when asking for the renderers materials which creates a copy of the instances.
             * Unity also addresses Materials like Assets and does not provide regular garbage collection.
             * The <Renderer>().materials array returns an array of per-object materials,
             * while .sharedMaterials gives you the possibility of editing materials on all instances.
             * If you want to make changes to a specific material you have to reassign the whole array after modification.
             * .material and .sharedMaterial, however, retrieve only the first loaded and active material.
             * Therefore, changing a value on .materials[0] without reassignment causes an error with duplicated materials and leaking,
             * while .material does the same thing without and error.
             * More information here: https://docs.unity3d.com/ScriptReference/Renderer-materials.html
             * and here: https://answers.unity.com/questions/192561/renderermaterials-leaking-materials.html
            */
            var mat = GetComponent<Renderer>().material;
            mat.SetColor("_Color", color);
        }

        void OnDrawGizmosSelected()
        {
            // Draw a yellow sphere at the anchors' positions
            Gizmos.color = Color.yellow;
            foreach (var t in anchors)
            {
                Gizmos.DrawSphere(t.position, 0.005f);
            }

            // Draw the occupied voxels
            Gizmos.color = Color.yellow;
            foreach (var t in anchors)
            {
                Voxel v = new Voxel(t.position);
                Gizmos.DrawWireCube(v.getCenter(), Voxel.SCALE3D);
                // Debug.Log($"{t.gameObject.name}: {t.position.ToString("F3")}, {v.Coordinates}");
            }
        }

        public Voxel[] GetVoxels()
        {
            UpdateVoxels();
            return voxels;
        }

        public void UpdateVoxels()
        {
            voxels = new Voxel[anchorsSize];
            for (int i = 0; i < anchorsSize; i++)
            {
                voxels[i] = new Voxel(anchors[i].position) - reference.GetVoxel();
            }
        }

        public bool Match(RecipeBrick brick)
        {
            if (identifier == brick.GetID())
            {
                foreach (var recipeVoxel in brick.GetVoxels())
                {
                    // if at least one voxel does not match, the whole brick does not match
                    if (MatchVoxel(recipeVoxel) == false) return false;
                }
                // if the check passed, then all voxels match
                return true;
            }
            else
            {
                // return false if the identifier does not match
                return false;
            }
        }

        // assuming two center points can never be in the same voxel
        private bool MatchVoxel(Voxel other)
        {
            foreach (var v in voxels)
            {
                if (v.Equals(other)) return true;
            }
            return false;
        }

        // assuming that the center of the brick is always equal to the position of the center anchor
        public void SnapToVoxels()
        {
            Voxel v = new Voxel(transform.position);
            transform.DOMove(v.getCenter(), 0.2f);

            Vector3 majorAxis = GetMajorAxis();
            transform.DOLookAt(transform.position + majorAxis, 0.2f);
        }

        public Vector3 GetMajorAxis()
        {
            Vector3 direction = anchors[1].position - anchors[2].position; // get the delta of all axes
            Vector3 directionAbs = new Vector3(Mathf.Abs(direction.x), Mathf.Abs(direction.y), Mathf.Abs(direction.z));

            if (directionAbs.x > directionAbs.y)
            {
                if (directionAbs.x > directionAbs.z)
                {
                    return new Vector3(Math.Sign(direction.x), 0, 0);
                }
                else
                {
                    return new Vector3(0, 0, Math.Sign(direction.z));
                }
            }
            else if (directionAbs.y > directionAbs.z)
            {
                return new Vector3(0, Math.Sign(direction.y), 0);
            }
            else
            {
                return new Vector3(0, 0, Math.Sign(direction.z));
            }
        }
    }
}