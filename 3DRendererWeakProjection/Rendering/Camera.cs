using System.Numerics;

namespace _3DRendererWeakProjection.Rendering
{
    internal class Camera
    {
        internal static Camera? Instance;

        internal Vector3 Position;
        internal Vector3 Rotation;

        internal Camera(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;

            Instance = this;
        }
    }
}
