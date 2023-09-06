#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EquipItemEditor
{
    public class EquipItemEditor : EditorWindow
    {
        public BodyParts BodyParts;
        public Transform EquipmentParent;

        public GameObject HelmetPrefab;
        public GameObject BreatplatePrefab;
        public GameObject ShoulderPrefab;
        public GameObject GauntletsPrefab;
        public GameObject PantsPrefab;
        public GameObject ShoesPrefab;
        public GameObject PrimaryWeaponPrefab;
        public GameObject SecondaryWeaponPrefab;

        private ScriptableObject scriptableObj;
        private SerializedObject serialObj;

        private Vector2 viewScrollPosition;

        [MenuItem("TUNC/Equip Item")]
        private static void OpenWindow()
        {
            EditorWindow editorWindow = (EquipItemEditor)GetWindow(typeof(EquipItemEditor), false, "Equip Item");
            editorWindow.minSize = new Vector2(400, 500);
            GUI.contentColor = Color.white;
            editorWindow.Show();
        }

        private void OnEnable()
        {
            scriptableObj = this;
            serialObj = new SerializedObject(scriptableObj);
        }

        private void OnGUI()
        {
            DrawMain();
        }

        private void DrawMain()
        {
            viewScrollPosition = EditorGUILayout.BeginScrollView(viewScrollPosition, false, false);


            BodyParts = (BodyParts)EditorGUILayout.ObjectField("BodyPartsParent", BodyParts, typeof(BodyParts), true);
            GUILayout.Space(7);
            EquipmentParent = (Transform)EditorGUILayout.ObjectField("Equipment Parent", EquipmentParent, typeof(Transform), true);
            GUILayout.Space(20);
            HelmetPrefab = (GameObject)EditorGUILayout.ObjectField("Helmet", HelmetPrefab, typeof(GameObject), true);
            GUILayout.Space(7);
            BreatplatePrefab = (GameObject)EditorGUILayout.ObjectField("Breatplate", BreatplatePrefab, typeof(GameObject), true);
            GUILayout.Space(7);
            ShoulderPrefab = (GameObject)EditorGUILayout.ObjectField("Shoulder", ShoulderPrefab, typeof(GameObject), true);
            GUILayout.Space(7);
            GauntletsPrefab = (GameObject)EditorGUILayout.ObjectField("Gauntlets", GauntletsPrefab, typeof(GameObject), true);
            GUILayout.Space(7);
            PantsPrefab = (GameObject)EditorGUILayout.ObjectField("Pants", PantsPrefab, typeof(GameObject), true);
            GUILayout.Space(7);
            ShoesPrefab = (GameObject)EditorGUILayout.ObjectField("Shoes", ShoesPrefab, typeof(GameObject), true);
            GUILayout.Space(7);
            PrimaryWeaponPrefab = (GameObject)EditorGUILayout.ObjectField("PrimaryWeapon", PrimaryWeaponPrefab, typeof(GameObject), true);
            GUILayout.Space(7);
            SecondaryWeaponPrefab = (GameObject)EditorGUILayout.ObjectField("SecondaryWeapon", SecondaryWeaponPrefab, typeof(GameObject), true);
            GUILayout.Space(15);

            if (GUILayout.Button("TRANSFER", GUILayout.MinWidth(150), GUILayout.MinHeight(30), GUILayout.ExpandWidth(true)))
            {
                EquipAll();
            }

            if (GUILayout.Button("REMOVE ALL", GUILayout.MinWidth(150), GUILayout.MinHeight(30), GUILayout.ExpandWidth(true)))
            {
                RemoveAllEquipment();
            }

            serialObj.ApplyModifiedProperties();

            GUILayout.Space(20);
            GUILayout.EndScrollView();
        }

        private void EquipAll()
        {
            List<GameObject> EquipmentList = new List<GameObject>();
            EquipmentList.Add(HelmetPrefab);
            EquipmentList.Add(BreatplatePrefab);
            EquipmentList.Add(ShoulderPrefab);
            EquipmentList.Add(GauntletsPrefab);
            EquipmentList.Add(PantsPrefab);
            EquipmentList.Add(ShoesPrefab);
            EquipmentList.Add(PrimaryWeaponPrefab);
            EquipmentList.Add(SecondaryWeaponPrefab);

            // Destroy Already Equipped Items To Prevevnt Stacking in Inventory
            for (int i = EquipmentParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(EquipmentParent.GetChild(i).gameObject);
            }

            if (BodyParts.EquipmentParts.Count < EquipmentList.Count)
            {
                Debug.LogError("Missing Body Parts! Set them in scene.(Character -> EquipmentGO)");
                return;
            }
            for (int i = 0; i < EquipmentList.Count; i++)
            {
                // if armor is not equipped for some parts
                if (EquipmentList[i] == null)
                    continue;

                GameObject BodyPart = BodyParts.EquipmentParts[i];

                GameObject prefab_to_clone = PrefabUtility.InstantiatePrefab(EquipmentList[i]) as GameObject;
                PrefabUtility.UnpackPrefabInstance(prefab_to_clone, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                if (BodyPart == null)
                {
                    Debug.LogError("Body Parts Parent is NOT set!");
                }
                else
                {
                    SkinnedMeshRenderer instanceSMR = prefab_to_clone.GetComponentInChildren<SkinnedMeshRenderer>();
                    SkinnedMeshRenderer TargetSMR = BodyPart.GetComponentInChildren<SkinnedMeshRenderer>();
                    TransferSkinnedMeshes(instanceSMR, TargetSMR, EquipmentParent);
                }
            }
        }

        private void RemoveAllEquipment()
        {
            // Destroy Already Equipped Items To Prevevnt Stacking in Inventory
            for (int i = EquipmentParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(EquipmentParent.GetChild(i).gameObject);
            }
        }

        public void TransferSkinnedMeshes(SkinnedMeshRenderer SkinnedMeshRenderer, SkinnedMeshRenderer TargetSkinnedMeshRenderer, Transform NewParent)
        {
            GameObject SkinnedMeshRendererParent = SkinnedMeshRenderer.transform.parent.gameObject;
            Transform newArmature = TargetSkinnedMeshRenderer.rootBone;
            Transform[] newBones = TargetSkinnedMeshRenderer.bones;

            SkinnedMeshRenderer.rootBone = newArmature;
            SkinnedMeshRenderer.bones = newBones;
            SkinnedMeshRenderer.transform.SetParent(NewParent);
            SkinnedMeshRenderer.transform.localPosition = Vector3.zero;

            if (!Application.isPlaying)
            {
                DestroyImmediate(SkinnedMeshRendererParent);
            }
            else
            {
                Destroy(SkinnedMeshRendererParent);
            }
        }
    }
}
#endif