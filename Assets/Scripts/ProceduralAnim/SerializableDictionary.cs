using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable] public class KeyDataMap : SerializableDictionary<string, KeyData> {}
[System.Serializable] public class BodyMap : SerializableDictionary<string, ProceduralAnim.BodyPart> {}

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver {
	[SerializeField]
	private List<TKey> keys = new List<TKey>();
	
	[SerializeField]
	private List<TValue> values = new List<TValue>();

	public Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

	public void Add(TKey key, TValue value) {
		dictionary.Add(key, value);
	}

	public TValue this [TKey key] {
		set{ dictionary[key] = value; }
		get{ return dictionary[key]; }
	}

	// save the dictionary to lists
	public void OnBeforeSerialize() {
		keys.Clear();
		values.Clear();
		foreach(KeyValuePair<TKey, TValue> pair in dictionary) {
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}
	
	// load dictionary from lists
	public void OnAfterDeserialize() {
		dictionary.Clear();
		
		if(keys.Count != values.Count)
			throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));
//		Debug.Log (this.GetType ());
		for (int i = 0; i < Mathf.Min(keys.Count,values.Count); i++)
			dictionary.Add(keys[i],values[i]);
	}	
}