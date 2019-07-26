using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class App : MonoBehaviour
{
    private GameObject[] inventory;
    private Item[] itemsInInventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectsWithTag("Inventory");
        itemsInInventory = new Item[inventory.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeNewItem()
    {
        int index = SearchSpotForItem();

        if(index == -1)
        {
            return;
        }

        Item nItem = new Item();
        inventory[index].GetComponent<Image>().color = nItem.GetRankColor();
        itemsInInventory[index] = nItem;
    }

    public int SearchSpotForItem()
    {
        int t = 0;
        while(itemsInInventory[t] != null)
        {
            t++;
            if (t > inventory.Length - 1)
            {
                return -1;
            }
        }       
        float max = inventory[t].transform.position.y;
        List<int> possiblePlaces = new List<int>();
        possiblePlaces.Add(t);
        for (int i = t + 1; i < inventory.Length; i++)
        {
            if(itemsInInventory[i] == null)
            {
                if (inventory[i].transform.position.y > max)
                {
                    max = inventory[i].transform.position.y;
                    possiblePlaces = new List<int>();
                    possiblePlaces.Add(i);
                    Debug.Log("Reset " + i);
                }
                else if (inventory[i].transform.position.y == max)
                {
                    possiblePlaces.Add(i);
                    Debug.Log("Possible place: " + i);
                }
            }
        }

        int choice = possiblePlaces.ToArray()[0];
        max = inventory[possiblePlaces.ToArray()[0]].transform.position.x;
        foreach (int i in possiblePlaces)
        {
            Debug.Log(inventory[i].transform.position.x + " / " + max);
            if(inventory[i].transform.position.x < max)
            {
                choice = i;
            }
        }

        return choice;
    }
}

public enum ItemRanks : int
{
    Common = 0,
    Rare = 1,
    Epic = 2,
    Legendary = 3
}

public class Item
{
    private ItemRanks rank;
    private int lvl;

    public Item()
    {
        this.rank = (ItemRanks)Mathf.RoundToInt(Random.Range(0, 3));
    }

    public Item(ItemRanks rank)
    {
        this.rank = rank;
        switch (rank)
        {
            case ItemRanks.Common:
                this.lvl = Mathf.RoundToInt(Random.Range(1, 20));
                break;
            case ItemRanks.Rare:
                this.lvl = Mathf.RoundToInt(Random.Range(15, 50));
                break;
            case ItemRanks.Epic:
                this.lvl = Mathf.RoundToInt(Random.Range(40, 90));
                break;
            case ItemRanks.Legendary:
                this.lvl = Mathf.RoundToInt(Random.Range(80, 100));
                break;
            default:
                break;
        }
    }

    public Color GetRankColor()
    {
        switch (this.rank)
        {
            case ItemRanks.Common:
                return Color.gray;
            case ItemRanks.Rare:
                return Color.blue;
            case ItemRanks.Epic:
                return Color.green;
            case ItemRanks.Legendary:
                return Color.red;
            default:
                break;
        }
        return Color.black;
    }

    public int GetLvl()
    {
        return lvl;
    }
}


