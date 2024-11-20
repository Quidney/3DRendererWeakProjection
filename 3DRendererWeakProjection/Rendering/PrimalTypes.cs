using System.Numerics;

namespace _3DRendererWeakProjection.Rendering
{
    enum PrimalTypes
    {
        Cube,
        Thingy
    }

    internal static class PrimalTypesData
    {
        internal static Vector3[] GetPointsFromPrimalType(PrimalTypes primalType) => Points[primalType];
        internal static Vertex[] GetVerticiesFromPrimalType(PrimalTypes primalType) => Vertices[primalType];

        internal static Vector3[] CubePoints { get; } =
        [
            new(0, 0, 0), //0
            new(1, 0, 0), //1 X
            new(0, 1, 0), //2 Y
            new(0, 0, 1), //3 Z

            new(1, 1, 0), //4 XY
            new(1, 0, 1), //5 XZ
            new(0, 1, 1), //6 YZ
            new(1, 1, 1), //7
            ];
        internal static Vertex[] CubeVerticies { get; } =
        [
            new(0, 3), new(0, 1), new(0, 2),
            new(1, 5), new(2, 4), new(2, 6),
            new(6, 7), new(4, 7), new(7, 5),
            new(5, 3), new(6, 3), new(1, 4)
        ];


        internal static Vector3[] ThingyPoints { get; } =
        [
            new(0, 0, 0), //0
            new(1, 0, 0), //1 X
            new(0, 1, 0), //2 Y
            new(0, 0, 1), //3 Z

            new(1, 0, 1), //4 XZ
            new(0, 1, 1), //5 YZ
        ];
        internal static Vertex[] ThingyVerticies { get; } =
        [
            new(0, 3), new(0, 1), new(3, 4), new(1, 4),
            new(0, 2), new(3, 2), new(1, 2), new(4, 2)
        ];


        private static readonly Dictionary<PrimalTypes, Vector3[]> Points = new()
        {
            {PrimalTypes.Cube, CubePoints},
            {PrimalTypes.Thingy, ThingyPoints},
        };
        private static readonly Dictionary<PrimalTypes, Vertex[]> Vertices = new()
        {
            {PrimalTypes.Cube, CubeVerticies},
            {PrimalTypes.Thingy, ThingyVerticies},
        };
    }
}