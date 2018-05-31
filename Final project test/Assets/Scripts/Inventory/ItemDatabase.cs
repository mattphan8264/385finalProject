using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> database = new List<Item>();
    public JsonData itemData;
	bool isDone = false;
	string url = "http://students.washington.edu/mattphan/StreamingAssets/Items.json";
	void Awake() {
		Start ();
	}
			
	IEnumerator Start() {
		WWW link = new WWW (url);
		yield return link;
		isDone = true;
		if (isDone) {
			itemData = JsonMapper.ToObject (new LitJson.JsonReader (link.text));
			ConstructItemDatabase ();
		}
	}

	public Item RerollFetch(int id) {
		for (int i = 0; i < database.Count; i++)
			if (database [i].ID == id) {
				Item temp = database [id];
				temp = rerollStats (temp);
				return temp;
			}
		return null;
	}

    public Item FetchItemByID(int id)
    {
		for (int i = 0; i < database.Count; i++)
			if (database [i].ID == id) {
				Item temp = database [id];
				return temp;
			}
        return null;
    }

	public string FetchItemName(int id) {
		for (int i = 0; i < database.Count; i++)
			if (database [i].ID == id) {
				Item temp = database [id];
				return temp.Title;
			}
		return null;
	}

    void ConstructItemDatabase()
    {
		database = new List<Item> ();
        for (int i = 0; i < itemData.Count; i++)
        {
			Item temp = new Item ((int)itemData [i] ["id"], itemData [i] ["title"].ToString (), itemData [i] ["type"].ToString (), (int)itemData [i] ["value"],
				            (int)itemData [i] ["stats"] ["str"], (int)itemData [i] ["stats"] ["dex"], (int)itemData [i] ["stats"] ["wis"], (int)itemData [i] ["stats"] ["luk"],
				            (int)itemData [i] ["stats"] ["atk"], (int)itemData [i] ["stats"] ["def"], itemData [i] ["description"].ToString (),
				            (int)itemData [i] ["rarity"], itemData [i] ["slug"].ToString ());
			database.Add(temp);
        }
    }

	Item rerollStats(Item input) {
		Item temp = new Item (input.ID, input.Title, input.type, input.Value, input.str, input.dex, input.wis, input.luk, input.atk, input.def, input.Description, input.Rarity, input.Slug);

		int boosted = Random.Range (0, 100);
		if (boosted < 5) {
			temp.str += (int)temp.str / 2;
			temp.dex += (int)temp.dex / 2;
			temp.wis += (int)temp.wis / 2;
			temp.luk += (int)temp.luk / 2;
			temp.atk += (int)temp.atk / 4;
			temp.def += (int)temp.def / 4;
			temp.Title = temp.Title + " (Boosted)";
		}

		if (PlayerStatistics.level <= 3) {
			int random = Random.Range (-3, 4);
			temp.str += random;
			random = Random.Range (-3, 4);
			temp.dex += random;
			random = Random.Range (-3, 4);
			temp.wis += random;
			random = Random.Range (-3, 4);
			temp.luk += random;
			random = Random.Range (-3, 4);
			temp.atk += random;
			random = Random.Range (-3, 4);
			temp.def += random;
		} else {
			int random = Random.Range (-3, (int)(temp.str / 5) + ((int)PlayerStatistics.level / 5) + 1);
			temp.str += random;
			random = Random.Range (-3, (int)(temp.dex / 5) + ((int)PlayerStatistics.level / 5) + 1);
			temp.dex += random;
			random = Random.Range (-3, (int)(temp.wis / 5) + ((int)PlayerStatistics.level / 5) + 1);
			temp.wis += random;
			random = Random.Range (-3, (int)(temp.luk / 5) + ((int)PlayerStatistics.level / 5) + 1);
			temp.luk += random;
			random = Random.Range (-3, (int)(temp.atk / 5) + ((int)PlayerStatistics.level / 5) + 1);
			temp.atk += random;
			random = Random.Range (-3, (int)(temp.def / 5) + ((int)PlayerStatistics.level / 5) + 1);
			temp.def += random;
		}
		return temp;
	}

}

public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
	public string type { get; set; }
    public int Value { get; set; }
	public int str { get; set; }
	public int dex { get; set; }
	public int wis { get; set; }
	public int luk { get; set; }
	public int atk { get; set; }
	public int def { get; set; }
    public string Description { get; set; }
    public int Rarity { get; set; }
    public string Slug { get; set; }
    public Sprite Sprite { get; set; }

	public Item(int id, string title, string type, int value, int str, int dex, int wis, int luk, int atk, int def, string description, int rarity, string slug)
    {
        this.ID = id;
        this.Title = title;
		this.type = type;
        this.Value = value;
		this.str = str;
		this.dex = dex;
		this.wis = wis;
		this.luk = luk;
		this.atk = atk;
		this.def = def;
        this.Description = description;
        this.Rarity = rarity;
        this.Slug = slug;
		this.Sprite = Resources.Load<Sprite> ("DrawingsV2/Items/Equipment/" + slug);
    }

	public Item(Item input) {
		this.ID = input.ID;
		this.Title = input.Title;
		this.type = input.type;
		this.Value = input.Value;
		this.str = input.str;
		this.dex = input.dex;
		this.wis = input.wis;
		this.luk = input.luk;
		this.atk = input.atk;
		this.def = input.def;
		this.Description = input.Description;
		this.Rarity = input.Rarity;
		this.Slug = input.Slug;
		this.Sprite = input.Sprite;
	}

    public Item()
    {
        this.ID = -1;
    }
}
	