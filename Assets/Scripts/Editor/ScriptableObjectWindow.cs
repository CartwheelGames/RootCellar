using System;
using System.Linq;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace AtW
{
	/// <summary>
	/// Scriptable object window.
	/// </summary>
	public sealed class ScriptableObjectWindow : EditorWindow
	{
		private string[] names;

		private int selectedIndex;

		private Type[] types;

		public Type[] Types
		{
			get => types;
			set
			{
				types = value;
				names = types.Select(t => t.FullName).ToArray();
			}
		}

		public void OnGUI()
		{
			GUILayout.Label("ScriptableObject Class");
			selectedIndex = EditorGUILayout.Popup(selectedIndex, names);
			if (GUILayout.Button("Create"))
			{
				ScriptableObject asset = CreateInstance(types[selectedIndex]);
				ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
					asset.GetInstanceID(),
					CreateInstance<EndNameEdit>(),
					$"{names[selectedIndex]}.asset",
					AssetPreview.GetMiniThumbnail(asset),
					null);
				Close();
			}
		}
	}

	internal class EndNameEdit : EndNameEditAction
	{
		public override void Action(int instanceId, string pathName, string resourceFile)
		{
			AssetDatabase.CreateAsset(
				EditorUtility.InstanceIDToObject(instanceId),
				AssetDatabase.GenerateUniqueAssetPath(pathName));
		}
	}
}
