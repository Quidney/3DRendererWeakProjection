using System.Numerics;

namespace _3DRendererWeakProjection.Rendering
{
    internal static class Renderer3D
    {
        const float FocalLength = 10f;

        internal static Vector2 Project(Vector3 point, Vector3 scale, Vector2 screenSize)
        {
            float x = screenSize.X / 2 + (FocalLength * point.X) / (FocalLength + point.Z) * 100;
            float y = screenSize.Y / 2 + (FocalLength * point.Y) / (FocalLength + point.Z) * 100;
            return new Vector2(x, y);
        }

        internal static Vector3 GetProjectPoint(Vector3 startPoint, Object3D obj, Camera cam)
        {
            return startPoint + obj.Position - new Vector3(cam.Position.X, -cam.Position.Y, cam.Position.Z);
        }

        internal static Vector3 RotateX(Vector3 point, Vector3 rotation) 
        {
            Vector3 returnPoint;
            returnPoint.X = point.X;
            returnPoint.Y = MathF.Cos(rotation.X) * point.Y - MathF.Sin(rotation.X) * point.Z;
            returnPoint.Z = MathF.Sin(rotation.X) * point.Y + MathF.Cos(rotation.X) * point.Z;
            return returnPoint;
        }
        internal static Vector3 RotateY(Vector3 point, Vector3 rotation)
        {
            Vector3 returnPoint;
            returnPoint.X = MathF.Cos(rotation.Z) * point.X - MathF.Sin(rotation.Z) * point.Z;
            returnPoint.Y = point.Y;
            returnPoint.Z = MathF.Sin(rotation.Z) * point.X + MathF.Cos(rotation.Z) * point.Z;
            return returnPoint;
        }
        internal static Vector3 RotateZ(Vector3 point, Vector3 rotation)
        {
            return point;

            Vector3 returnPoint;
            returnPoint.X = MathF.Cos(rotation.Y) * point.Y - MathF.Sin(rotation.Y) * point.Y;
            returnPoint.Y = MathF.Sin(rotation.Y) * point.Y + MathF.Cos(rotation.Y) * point.Y;
            returnPoint.Z = point.Z;
            return returnPoint;
        }

        internal static void Render(Object3D obj3D, Graphics g)
        {
            Camera? cam = Camera.Instance;

            if (cam == null)
                return;

            using Pen pen = new(Color.White);

            Vector3[] points = obj3D.Points;

            foreach (Vertex vertex in obj3D.Verticies)
            {
                Vector3 rotatedStartPoint = RotateX(RotateY(RotateZ(points[vertex.start], obj3D.Rotation), obj3D.Rotation), obj3D.Rotation);
                Vector3 projectPointX = GetProjectPoint(rotatedStartPoint, obj3D, cam);
                Vector2 lineStart = Project(projectPointX, obj3D.Scale, new Vector2(g.ClipBounds.Width, g.ClipBounds.Height));

                Vector3 rotatedEndPoint = RotateX(RotateY(RotateZ(points[vertex.end], obj3D.Rotation), obj3D.Rotation), obj3D.Rotation); 
                Vector3 projectPointY = GetProjectPoint(rotatedEndPoint, obj3D, cam);
                Vector2 lineEnd = Project(projectPointY, obj3D.Scale, new Vector2(g.ClipBounds.Width, g.ClipBounds.Height));

                g.DrawLine(pen, lineStart.X, lineStart.Y, lineEnd.X, lineEnd.Y);
            }
        }
    }
}
