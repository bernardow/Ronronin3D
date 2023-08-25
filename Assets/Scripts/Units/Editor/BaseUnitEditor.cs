using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

namespace Units.Editor
{
    [CustomEditor(typeof(BaseUnit))]
    public class BaseUnitEditor : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            BaseUnit baseUnit = (BaseUnit)target;
            
            EditorGUILayout.FloatField("Damage", baseUnit.Damage);
            base.OnInspectorGUI();

            if (baseUnit.HasHealthBar)
            {
                baseUnit.HealthBar = EditorGUILayout.ObjectField("HealthBar", baseUnit.HealthBar, typeof(GameObject), true) as GameObject;
            }
                
        }
    }
}
