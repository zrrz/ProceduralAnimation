using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;
using System.Collections.Generic;

public class ScriptFixer : Editor {

	List<string> typeUsed;

	bool useHungarian = false;

	[MenuItem("ScriptFixer/Fix")]
	void FixScripts () {
		DirectoryInfo directory = new DirectoryInfo(Application.dataPath);
		FileInfo[] files = directory.GetFiles("*.cs", SearchOption.AllDirectories);

		typeUsed = new List<string>();

		for(int i = 0, n = files.Length; i < n; i++) {
			StreamReader reader = new StreamReader(files[i].FullName);

			int topOfFile = 0;
			Dictionary<string, List<int>> occurences = new Dictionary<string, List<int>>();

			typeUsed.Clear();

			int iter = 0;

			while(!reader.EndOfStream) {
				string line = reader.ReadLine();

				if(line.Contains(" class ")) {
					occurences.Clear();
					topOfFile = iter;

					if (typeUsed.Count > 0) {
						AddTypes (files [i].FullName, ref reader, topOfFile);
					}
				}

				if(CheckType(line, "RigidBody"))
					occurences["RigidBody"].Add(iter);
				if(CheckType(line, "Renderer"))
					occurences["Renderer"].Add(iter);
				if(CheckType(line, "Camera"))
					occurences["Camera"].Add(iter);
				if(CheckType(line, "Collider"))
					occurences["Collider"].Add(iter);

				iter++;
			}
			if (typeUsed.Count > 0) {
				AddTypes (files [i].FullName, ref reader, topOfFile);
			}
		}
	}

	void AddTypes (string fileName, ref StreamReader reader, int topOfFile) {
		reader.Close ();
		reader = new StreamReader (fileName);
		for (int k = 0; k < topOfFile; k++) {
			reader.ReadLine (); //Skip down to top of file
		}
		string[] str = reader.ReadToEnd().Split(topOfFile, 2, System.StringSplitOptions.RemoveEmptyEntries);

		for (int j = 0, n2 = typeUsed.Count; j < n2; j++) {
			str[1] = typeUsed [j] + typeUsed [j].ToLowerInvariant () + ";\n" + str[1];
		}
		File.WriteAllText(fileName, str);
	}

	bool CheckType(string line, string type) {
		if(line.Contains("GetComponent<" + "type" + ">()")) {
			AddType(type);
			line.Replace("GetComponent<" + "type" + ">()", useHungarian? "m_" + type.ToLower() : type.ToLower());
			return true;
		}
		return false;
	}

	void AddType(string type) {
		if(!typeUsed.Contains(type)) {
			typeUsed.Add(type);
		}
	}
}
