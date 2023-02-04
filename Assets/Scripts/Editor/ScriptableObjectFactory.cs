using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AtW
{
	/// <summary>
	/// Helper class for instantiating ScriptableObjects.
	/// </summary>
	public sealed class ScriptableObjectFactory
	{
		[MenuItem("Assets/Create/ScriptableObject")]
		public static void Create()
		{
			Assembly assembly = GetAssembly();

			// Get all classes derived from ScriptableObject
			Type[] allScriptableObjects = (from t in assembly.GetTypes()
										   where t.IsSubclassOf(typeof(ScriptableObject))
										   select t).ToArray();
			// Show the selection window.
			ScriptableObjectWindow window = EditorWindow.GetWindow<ScriptableObjectWindow>(true, "Create a new ScriptableObject", true);
			window.ShowPopup();
			window.Types = allScriptableObjects;
		}

		/// <summary>
		/// Returns the assembly that contains the script code for this project (currently hard coded)
		/// </summary>
		private static Assembly GetAssembly() => Assembly.Load(new AssemblyName("Assembly-CSharp"));
	}
}
