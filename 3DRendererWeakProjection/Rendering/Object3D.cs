using System.Numerics;

namespace _3DRendererWeakProjection.Rendering
{
    internal struct Vertex { internal int start, end; internal Vertex(int start, int end) { this.start = start; this.end = end; } }

    internal struct Object3D
    {
        internal Vector3 Position { get; set; }
        internal Vector3 Rotation { get; set; }
        internal Vector3 Scale { get; set; }
        internal Vector3[] Points { get; }
        internal Vertex[] Verticies { get; }

        internal Object3D(Vector3 position, Vector3 rotation, Vector3 scale, PrimalTypes primalType)
                : this(position, rotation, scale, PrimalTypesData.GetPointsFromPrimalType(primalType), PrimalTypesData.GetVerticiesFromPrimalType(primalType))
        {

        }
        internal Object3D(Vector3 position, Vector3 rotation, Vector3 scale, Vector3[] points, Vertex[] verticies)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;

            Points = points;
            Verticies = verticies;
        }
    }
}