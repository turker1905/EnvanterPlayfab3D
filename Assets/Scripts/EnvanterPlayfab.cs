using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;
using UnityEngine.UI;

public class EnvanterPlayfab : MonoBehaviour
{

    [SerializeField] Text _elmaText, _mantarText;
    string ıd;
    Dictionary<string, string> Foods = new Dictionary<string, string>();
    Dictionary<string, string> Materials = new Dictionary<string, string>();
    Dictionary<string, string> Items = new Dictionary<string, string>();

    int _appleCount, _mantarCount, _woodCount, _stoneCount;


    private void Start()
    {
        AddItem();
        Foods.Add("Apple", 0.ToString());
        Foods.Add("Mantar", 0.ToString());
        Materials.Add("Odun", 0.ToString());
        Materials.Add("Tas", 0.ToString());

       

    }

    void addFood()
    {
        PlayFabServerAPI.UpdateUserInventoryItemCustomData(new UpdateUserInventoryItemDataRequest() 
        {
            PlayFabId =PlayGuess._playerId,
            ItemInstanceId = ıd,
            Data = Items
        }
        , Result => 
        {
            

        }, Error => 
        {
        
        });


    }


    void addMaterial()
    {
        PlayFabServerAPI.UpdateUserInventoryItemCustomData(new UpdateUserInventoryItemDataRequest()
        {
            PlayFabId = PlayGuess._playerId,
            ItemInstanceId = ıd,
            Data = Materials
        }
        , Result =>
        {


        }, Error =>
        {

        });


    }


    void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest() 
        {
         

        }, 
        Result => 
        {
            foreach (var item in Result.Inventory)
            {
                if (item.ItemId=="Foods")
                {
                    if (item.CustomData!=null)
                    {



                        foreach (var a in item.CustomData)
                        {
                            if (a.Key == "Apple")
                            {
                                _appleCount = int.Parse(a.Value);
                                Foods.Clear();
                                Foods.Add(a.Key, a.Value);
                                _elmaText.text = _appleCount.ToString();
                            }
                            if (a.Key == "Mantar")
                            {
                                _mantarCount = int.Parse(a.Value);
                                Foods.Clear();
                                Foods.Add(a.Key, a.Value);
                                _mantarText.text = _mantarCount.ToString();
                            }

                        }
                    }
                    else if (item.CustomData == null)
                    {
                        foreach (KeyValuePair<string,string> entry in Foods)
                        {
                            if (entry.Key=="Apple")
                            {
                                _elmaText.text = entry.Value;
                            }
                            if (entry.Key=="Mantar")
                            {
                                _mantarText.text = entry.Value;
                            }
                        }
                        //PlayerPrefs.SetInt("NewGame",1);
                    }


                    ıd = item.ItemInstanceId;
                    addFood();

                }

            }
        }, 
        Error => 
        {
        
        });


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _appleCount++;
            Foods.Clear();
            Foods.Add("Apple", _appleCount.ToString());
            Foods.Add("Mantar", _mantarCount.ToString());
            Items = Foods;
            StartCoroutine(Bekle());
          
    }
        }


    IEnumerator Bekle()
    {
        addFood();
        yield return new WaitForSeconds(0.2f);
        GetInventory();
    }




    void AddItem()
    {
        List<string> _items = new List<string>();
        _items.Add("Foods");
        _items.Add("Materials");

        PlayFabServerAPI.GrantItemsToUser(new GrantItemsToUserRequest()
        {
            PlayFabId = PlayGuess._playerId,
            CatalogVersion="Items",
            ItemIds=_items

        },
        Result =>
        {
            GetInventory();
        },
        Error =>
        {

        });


    }



}
