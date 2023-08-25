using UnityEditor;

namespace Units.Player.Editor
{
    [CustomEditor(typeof(PlayerAttack))]
    public class PlayerAttackEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            PlayerAttack playerAttack = (PlayerAttack)target;
            EditorGUILayout.FloatField("Damage", playerAttack.AttackDamage);
            
            base.OnInspectorGUI();
        }
    }
}
