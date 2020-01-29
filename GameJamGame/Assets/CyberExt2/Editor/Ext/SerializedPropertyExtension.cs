using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Cyberevolver.Unity
{
	public static class SerializedPropertyExtension
    {
        public static object GetJustValue(this SerializedProperty prop)
        {
            switch (prop.propertyType)
            {
                case SerializedPropertyType.String:
                    return prop.stringValue;                 
                case SerializedPropertyType.Boolean:
                    return  prop.boolValue;                  
                case SerializedPropertyType.Integer:
                    return prop.intValue;                   
                case SerializedPropertyType.Float:
                    return prop.floatValue;                  
                case SerializedPropertyType.Vector2:
                    return prop.vector2Value;             
                case SerializedPropertyType.Vector3:
                    return prop.vector3Value;                
                case SerializedPropertyType.Color:
                    return prop.colorValue;                   
                case SerializedPropertyType.Rect:
                    return prop.rectValue;                  
                case SerializedPropertyType.Enum:
                    return prop.enumValueIndex;                 
                case SerializedPropertyType.ObjectReference:
                    return prop.objectReferenceValue;                 
                case SerializedPropertyType.RectInt:
                    return prop.rectIntValue;                
                case SerializedPropertyType.Vector2Int:
                    return prop.vector2IntValue;                
                case SerializedPropertyType.Vector3Int:
                    return prop.vector3IntValue;                                
                default:
                    return null;
                    

            }
        }
        public static void SetValue(this SerializedProperty prop, object value)
        {
            switch (prop.propertyType)
            {
                case SerializedPropertyType.String:
                    prop.stringValue = value?.ToString()??"null";break;
                case SerializedPropertyType.Boolean:
                    prop.boolValue = (bool)value; break;
                case SerializedPropertyType.Integer:
                    prop.intValue =(int) Convert.ChangeType(value,typeof(int)); break;
                case SerializedPropertyType.Float:
                    prop.floatValue = (float)Convert.ChangeType(value, typeof(float)); break;
                case SerializedPropertyType.Vector2:
                    prop.vector2Value = (Vector2)value; break;
                case SerializedPropertyType.Vector3:
                    prop.vector2Value = (Vector3)value; break;
                case SerializedPropertyType.Color:
                    prop.colorValue = (Color)value; break;
                case SerializedPropertyType.Rect:
                    prop.rectValue = (Rect)value; break;
                case SerializedPropertyType.Enum:
                    prop.enumValueIndex = (int)Convert.ChangeType(value, typeof(int)); break;
                case SerializedPropertyType.ObjectReference:
                    prop.objectReferenceValue = (UnityEngine.Object)value;break;
                case SerializedPropertyType.RectInt:
                    prop.rectIntValue = (RectInt)value; break;
                case SerializedPropertyType.Vector2Int:
                    prop.vector2IntValue = (Vector2Int)value; break;
                case SerializedPropertyType.Vector3Int:
                    prop.vector3IntValue = (Vector3Int)value;break;
                default:
                    break;
            }

         
        }
        public static IEnumerable<SerializedProperty> ToEnumerable(this SerializedProperty pr)
        {
            for (int x = 0; x < pr.arraySize; x++)
            {
                yield return pr.GetArrayElementAtIndex(x);
            }
        }
        public static System.Type GetFieldType(this SerializedProperty prop)
        {


            switch (prop.type)
            {
                case "double": return typeof(double);
                case "int": return typeof(int);
                case "uint": return typeof(uint);
                case "short": return typeof(short);
                case "ushort": return typeof(ushort);
                case "byte": return typeof(byte);
                case "sbyte": return typeof(sbyte);
                case "long": return typeof(long);
                case "ulong": return typeof(ulong);
                case "string": return typeof(string);
                case "bool": return typeof(bool);

            }
            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer: return typeof(int);
                case SerializedPropertyType.Float: return typeof(float);            
                case SerializedPropertyType.AnimationCurve:return typeof(AnimationCurve);
                case SerializedPropertyType.Boolean:return typeof(bool);
                case SerializedPropertyType.Color:return typeof(AColor);
                case SerializedPropertyType.Enum:return typeof(BackgroundMode);              
                case SerializedPropertyType.Gradient:return typeof(Gradient);
                case SerializedPropertyType.Vector2:return typeof(Vector2);
                case SerializedPropertyType.Vector2Int:return typeof(Vector2Int);
                case SerializedPropertyType.Vector3:return typeof(Vector3);
                case SerializedPropertyType.Vector3Int:return typeof(Vector3Int);
                case SerializedPropertyType.Vector4:return typeof(Vector4);
                case SerializedPropertyType.String:return typeof(string);
                case SerializedPropertyType.Rect:return typeof(Rect);
                case SerializedPropertyType.RectInt:return typeof(RectInt);
                case SerializedPropertyType.Quaternion:return typeof(Quaternion);
                case SerializedPropertyType.ObjectReference: return typeof(UnityEngine.Object);
                default: return null;
            }


        }
    }
}
#endif

