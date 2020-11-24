using System;
using System.Collections.Generic;
using System.Text;
using UniJSON;

namespace UniGLTF
{
    /// <summary>
    /// Extension または Extras に使う
    /// </summary>
    public class glTFExtension
    {
        // NO BOM
        static Encoding Utf8 = new UTF8Encoding(false);

        #region for Export
        public readonly Dictionary<string, ArraySegment<byte>> Serialized;
        public glTFExtension()
        {
            Serialized = new Dictionary<string, ArraySegment<byte>>();
        }
        public static glTFExtension Create(string key, ArraySegment<byte> raw)
        {
            var e = new glTFExtension();
            e.Serialized.Add(key, raw);
            return e;
        }
        #endregion

        #region for Import
        readonly ListTreeNode<JsonValue> m_json;
        public glTFExtension(ListTreeNode<JsonValue> json)
        {
            m_json = json;
        }

        public IEnumerable<KeyValuePair<ListTreeNode<JsonValue>, ListTreeNode<JsonValue>>> ObjectItems()
        {
            if (m_json.Value.ValueType == ValueNodeType.Object)
            {
                foreach (var kv in m_json.ObjectItems())
                {
                    yield return kv;
                }
            }
        }
        #endregion

        /// <summary>
        /// for unit test
        /// 
        /// parse exported value
        /// </summary>
        public glTFExtension Parse()
        {
            var f = new JsonFormatter();
            f.BeginMap();
            foreach (var kv in Serialized)
            {
                f.Key(kv.Key);
                f.Raw(kv.Value);
            }
            f.EndMap();

            var b = f.GetStoreBytes();
            var json = Encoding.UTF8.GetString(b.Array, b.Offset, b.Count);
            return new glTFExtension(json.ParseAsJson());
        }
    }

    public static class GltfExtensionFormatterExtensions
    {
        public static void GenSerialize(this JsonFormatter f, glTFExtension v)
        {
            //CommaCheck();
            f.BeginMap();
            if (v.Serialized != null)
            {
                foreach (var kv in v.Serialized)
                {
                    f.Key(kv.Key);
                    f.Raw(kv.Value);
                }
            }
            f.EndMap();
        }
    }
}