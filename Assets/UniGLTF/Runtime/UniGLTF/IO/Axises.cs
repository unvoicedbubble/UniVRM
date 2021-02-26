using System;
using UnityEngine;

namespace UniGLTF
{
    public enum Axises
    {
        Z,
        X,
    }

    public interface IAxisInverter
    {
        Vector3 InvertVector3(Vector3 src);
        Vector4 InvertVector4(Vector4 src);
        Quaternion InvertQuaternion(Quaternion src);
        Matrix4x4 InvertMat4(Matrix4x4 src);
    }

    public struct ReverseZ : IAxisInverter
    {
        public Matrix4x4 InvertMat4(Matrix4x4 src)
        {
            return src.ReverseZ();
        }

        public Quaternion InvertQuaternion(Quaternion src)
        {
            return src.ReverseZ();
        }

        public Vector3 InvertVector3(Vector3 src)
        {
            return src.ReverseZ();
        }

        public Vector4 InvertVector4(Vector4 src)
        {
            return src.ReverseZ();
        }
    }

    public class ReverseX : IAxisInverter
    {
        public Matrix4x4 InvertMat4(Matrix4x4 src)
        {
            return src.ReverseX();
        }

        public Quaternion InvertQuaternion(Quaternion src)
        {
            return src.ReverseX();
        }

        public Vector3 InvertVector3(Vector3 src)
        {
            return src.ReverseX();
        }

        public Vector4 InvertVector4(Vector4 src)
        {
            return src.ReverseX();
        }
    }

    public static class AxisesExtensions
    {
        public static IAxisInverter Create(this Axises axis)
        {
            switch (axis)
            {
                case Axises.Z: return new ReverseZ();
                case Axises.X: return new ReverseX();
                default: throw new NotImplementedException();
            }
        }
    }
}
