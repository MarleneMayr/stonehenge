using Bricks;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsBrickSpawner : MonoBehaviour
{
    [SerializeField] private PhysicsBrick brickPrefab;
    [SerializeField] private VoxelReference referenceBrickPrefab;
    [SerializeField] private int brickAmount; // 6 bricks for now
    [SerializeField] private float spacing; // should be 0.5f but is open for tweaking
    [SerializeField] private Vector3 orientation; // should be Vector3(0, 0, 1) but is open for tweaking
    [SerializeField] private ColorPalette colorPalette;

    private List<PhysicsBrick> spawnedBricks = new List<PhysicsBrick>();
    [Serializable] public class BrickEvent : UnityEvent<PhysicsBrick[]> { }
    public BrickEvent OnSpawnedAllBricks;

    private void OnValidate()
    {
        if (colorPalette.Colors.Length < brickAmount)
        {
            Debug.LogWarning($"{brickAmount} bricks need at least {brickAmount} colors!");
        }
    }

    public void Start()
    {
        UpdateSpawnerLocation();
        SpawnAllBricks();
    }

    private void UpdateSpawnerLocation()
    {
        float x = (brickAmount + (brickAmount - 1) * spacing) / 2.0f; // half of (bricks + spacings)
        x -= 0.5f; // subtract width of first brick to start in the middle of the brick
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    public void SpawnAllBricks()
    {
        // first spawn the reference brick for all other bricks and itself with id=0
        var reference = Instantiate(referenceBrickPrefab, transform);
        InitBrick(reference.GetComponent<PhysicsBrick>(), 0, reference);

        // then spawn the remaining bricks
        for (int i = 1; i < brickAmount; i++)
        {
            var brick = Instantiate(brickPrefab, transform);
            InitBrick(brick, i, reference);
        }
        OnSpawnedAllBricks?.Invoke(spawnedBricks.ToArray());
    }

    private void InitBrick(PhysicsBrick brick, int identifier, VoxelReference reference)
    {
        brick.SetID(identifier, colorPalette.Colors[identifier]);
        brick.SetReferenceBrick(reference);
        BrickUtility.PlaceBrickAbsolute(brick, GetSpawnPosition(identifier), orientation);
        brick.OnBrickFellDown.AddListener(RespawnBrick);
        spawnedBricks.Add(brick);
    }

    private void RespawnBrick(PhysicsBrick brick)
    {
        BrickUtility.PlaceBrickAbsolute(brick, GetSpawnPosition(brick.GetID()), orientation);
    }

    private Vector3 GetSpawnPosition(int id)
    {
        float distance = id * Voxel.SCALE;
        float x = transform.position.x - distance - (spacing * distance);
        return new Vector3(x, transform.position.y, transform.position.z);
    }
}
