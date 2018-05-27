using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJson;
using Newtonsoft.Json;
using UnityEngine.UI;

public class CharManager : MonoBehaviour {

	#region Singleton
	private static CharManager instance_;

	public static CharManager instance {
		get {
			return instance_;
		}
	}

	void Awake() {
		if (!instance_) {
			instance_ = this;
            Init();
        } else {
			this.enabled = false;
		}
	}
	#endregion

	#region LoadJson
	[System.Serializable]
	public class CharJsonItem {
		public int id;
		public string character;
		public AttackInfo attackInfo;
	}

	[System.Serializable]
	public class CharJson {
		public CharJsonItem[] data;
		public int[] basicChar;
	}

	[System.Serializable]
	public class CharTreeItem {
		public int keyId;									// 当前搜索用的汉字ID
		public int? toId;										// 匹配到当前情况，识别为的汉字ID
		public CharTreeItem[] nextItem;			// 可以继续搜索树结点
	}

	[System.Serializable]
	public class CharTree {
		public CharTreeItem[] data;
	}

	void BuildTree(CharTreeNode root, CharTreeItem rootItem) {
		root.id = rootItem.keyId;
		root.toId = rootItem.toId;
		if (rootItem.nextItem != null) {
			foreach (var i in rootItem.nextItem) {
				CharTreeNode node = new CharTreeNode(i.keyId, i.toId);
				root.nextNodes.Add(node);
				BuildTree(node, i);
			}
		}
	}

	void LoadJson() {
		var chars = Resources.Load("Jsons/chars") as TextAsset;
		var charsTree = Resources.Load("Jsons/chars_tree") as TextAsset;

		var charJson = JsonUtility.FromJson(chars.text, typeof(CharJson)) as CharJson;
		var charTree = JsonConvert.DeserializeObject<CharTree>(charsTree.text);
		// var charTree = JsonUtility.FromJson(charsTree.text, typeof(CharTree)) as CharTree;

		foreach (var i in charJson.basicChar) {
			basicCharIds.Add(i);
		}

		foreach (var i in charJson.data) {
			characterDict.Add(i.id, i.character);
			characterRevDict.Add(i.character, i.id);
			// !!! 必须手动给到 charId
			i.attackInfo.charId = i.id;
			charaterAttackInfoDict.Add(i.id, i.attackInfo);
		}

		foreach (var i in charTree.data) {
			var node = new CharTreeNode();
			characterTreeRootDict.Add(i.keyId, node);
			// build tree
			BuildTree(node, i);
		}

		Debug.Log("Loading characters finished");
	}
	#endregion

	public class CharTreeNode {
		public int id;
		public int? toId;
		public List<CharTreeNode> nextNodes;
		public CharTreeNode(int id = -1, int? toId = -1) {
			this.id = id;
			this.toId = toId;
			this.nextNodes = new List<CharTreeNode>();
		}
	}

	public Dictionary<int, string> characterDict;
	public Dictionary<string, int> characterRevDict;
	public Dictionary<int, AttackInfo> charaterAttackInfoDict;
	public Dictionary<int, CharTreeNode> characterTreeRootDict;
	public Dictionary<int, List<string>> idToStringsDict;
	List<int> basicCharIds;

	void Init() {
		characterDict = new Dictionary<int, string>();
		characterTreeRootDict = new Dictionary<int, CharTreeNode>();
		charaterAttackInfoDict = new Dictionary<int, AttackInfo>();
		characterRevDict = new Dictionary<string, int>();
		idToStringsDict = new Dictionary<int, List<string>>();
		basicCharIds = new List<int>();
		LoadJson();
		// #if UNITY_EDITOR
		// Traverse to get
		Traverse();
		// #endif
	}

	public int? GetIdByCharacter(string character) {
		if (!characterRevDict.ContainsKey(character)) {
			return null;
		}
		return characterRevDict[character];
	}

	public string GetCharacterById(int id) {
		if (!characterDict.ContainsKey(id)) {
			return null;
		}
		return characterDict[id];
	}

	int? SearchTreeByIds(CharTreeNode node, List<int> ids, int index = 0) {
		if (index >= ids.Count) return null;
		if (index == ids.Count - 1) {
			// 如果是最后一个id
			return node.toId;
		}
		foreach (var i in node.nextNodes) {
			if (i.id == ids[index + 1]) {
				var res = SearchTreeByIds(i, ids, index + 1);
				return res;
			}
		}
		return null;
	}

	List<int> CharStringsToIds(List<string> strs) {
		List<int> ids = new List<int>();
		strs.ForEach((str)=>{
			ids.Add(characterRevDict[str]);
		});
		return ids;
	}

	public string SearchCharacterByStrings(List<string> strings) {
		if (strings.Count <= 0) return null;
		var ids = CharStringsToIds(strings);
		var id = SearchTreeByIds(characterTreeRootDict[ids[0]], ids);
		if (id != null)
			return characterDict[id.Value];
		else
			return null;
	}

	public AttackInfo SearchAttackByStrings(List<string> strings) {
		if (strings.Count <= 0) return null;
		var ids = CharStringsToIds(strings);
		var id = SearchTreeByIds(characterTreeRootDict[ids[0]], ids);
		if (id != null) {
			return charaterAttackInfoDict[id.Value];
		} else {
			return null;
		}
	}
    public string SearchAttackCharacterByStrings(List<string> strings)
    {
        if (strings.Count <= 0) return null;
        var ids = CharStringsToIds(strings);
        var id = SearchTreeByIds(characterTreeRootDict[ids[0]], ids);
        if (id != null)
        {
            return characterDict[id.Value];
        }
        else
        {
            return null;
        }
    }


    public string GetRandomCharacter() {
		var random = Random.Range(0, basicCharIds.Count - 1);
		return characterDict[basicCharIds[random]];
	}

	// #if UNITY_EDITOR
	public void Traverse() {
		foreach (var kv in characterTreeRootDict) {
			List<string> strs = new List<string>();
			_Traverse(kv.Value, strs);
		}
	}

	void _Traverse(CharTreeNode root, List<string> strs) {
		strs.Add(characterDict[root.id]);
		if (root.toId != null) {
			string str = "";
			strs.ForEach((s)=>{
				str += s;
			});
			str += " -> " + characterDict[root.toId.Value];
			Debug.Log(str);
			idToStringsDict[root.toId.Value] = strs;
		}
		foreach (var i in root.nextNodes) {
			List<string> ss = new List<string>(strs);
			_Traverse(i, ss);
		}
	}
	// #endif
}
