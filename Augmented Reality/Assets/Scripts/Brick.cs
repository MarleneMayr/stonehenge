namespace Bricks
{
    public interface IBrick
    {
        ID GetID();
        Voxel[] GetVoxels();
    }

    public enum ID
    {
        red,
        blue,
        yellow,
        green,
        white
    }
}
