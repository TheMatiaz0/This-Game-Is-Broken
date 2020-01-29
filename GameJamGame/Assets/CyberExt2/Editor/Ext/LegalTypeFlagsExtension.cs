using Cyberevolver.Unity;
using UnityEditor;

namespace Cyberevolver.EditorUnity
{
    public static class LegalTypeFlagsExtension
    {
        public static bool IsGoodWith(this LegalTypeFlags flag, SerializedProperty prop)
        {

            if (flag.HasFlag(LegalTypeFlags.Array) && prop.isArray)
                return true;
            if (flag.HasFlag(LegalTypeFlags.GenericNonArray) && prop.propertyType == SerializedPropertyType.Generic && prop.isArray == false)
                return true;
            if (flag.HasFlag(LegalTypeFlags.NonGeneric) && prop.propertyType != SerializedPropertyType.Generic)
                return true;
            if (flag.HasFlag(LegalTypeFlags.ObjectReference) && prop.propertyType == SerializedPropertyType.ObjectReference)
                return true;
            if (flag.HasFlag(LegalTypeFlags.String) && prop.propertyType == SerializedPropertyType.String)
                return true;
            if (flag.HasFlag(LegalTypeFlags.Vector2) && prop.propertyType == SerializedPropertyType.Vector2)
                return true;
            if (flag.HasFlag(LegalTypeFlags.NumberValue) && prop.GetFieldType().IsNumber())
                return true;
            if (flag.HasFlag(LegalTypeFlags.Vector2Int) && prop.propertyType == SerializedPropertyType.Vector2Int)
                return true;
            if (flag.HasFlag(LegalTypeFlags.Enum) && prop.propertyType == SerializedPropertyType.Enum)
                return true;
            return false;

        }
    }
}
