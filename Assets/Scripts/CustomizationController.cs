using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationController : MonoBehaviour
{
    public GameController gc;
    private GameObject player;
    private List<Dice> diceBag;
    public GameObject dicePrefab;
    public GameObject diceFacePrefab;
    public GameObject newDiceFacePrefab;
    private List<GameObject> displayedDices;
    private List<List<GameObject>> displayedDiceFaces;

    public GameObject getPlayer(){
        return player;
    }

    // ========================= Display Management =========================

    /// <summary>
    /// Sets up the combat environment
    /// </summary>
    void Start(){
        player = gc.getPlayer();
        diceBag = player.GetComponent<Player>().getDices();
        displayedDices = new List<GameObject>();
        displayedDiceFaces = new List<List<GameObject>>();
        DisplayDices();
        DrawNewFace();
    }
    
    void DisplayDices(){
        foreach(Dice d in diceBag){
            GameObject dice = CustomizationDice.Create(d, dicePrefab, new Vector3(dicePrefab.transform.position.x, -70 * displayedDices.Count), this);
            List<GameObject> diceFaces = new List<GameObject>();
            foreach(DiceFace df in d.getFaces()){
                GameObject diceFace = CustomizationDiceFace.Create(df,diceFacePrefab, new Vector3(5 + diceFaces.Count * 55, diceFacePrefab.transform.position.y), dice.transform, this);
                diceFaces.Add(diceFace);
                Vector3 diceSize = dice.GetComponent<RectTransform>().sizeDelta;
                diceSize.x += 55.0f;
                dice.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,diceSize.x);
            }
            displayedDiceFaces.Add(diceFaces);
            displayedDices.Add(dice);
        }
    }

    public void DrawNewFace(){
        NewDiceFace.Create(new DiceFace(), newDiceFacePrefab, newDiceFacePrefab.transform.position);
    }

    public void RemoveDiceFace(GameObject go){
        //Move further DiceFaces back, then remove DiceFace from Controller's data and display
        List<GameObject> dice = displayedDiceFaces.Find(l2 => l2.Contains(go));
        int i = dice.IndexOf(go);
        foreach(GameObject dgo in dice.GetRange(i+1,dice.Count-(i+1))){
            Vector3 pos = dgo.GetComponent<RectTransform>().anchoredPosition;
            pos.x -= 55.0f;
            dgo.GetComponent<RectTransform>().anchoredPosition = pos;
        }
        dice.Remove(go);
        Destroy(go);
        //Resize Dice Box
        Vector3 diceSize = go.transform.parent.GetComponent<RectTransform>().sizeDelta;
        diceSize.x -= 55.0f;
        go.transform.parent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,diceSize.x);
    }

    public void AddDiceFace(GameObject go){
        //Display new
        int diceID = displayedDices.IndexOf(go);
        List<GameObject> diceFaces = displayedDiceFaces[diceID];
        diceFaces.Add(CustomizationDiceFace.Create(new DiceFace(),diceFacePrefab,new Vector3(5 + diceFaces.Count * 55, diceFacePrefab.transform.position.y), go.transform, this));
        //Resize Dice Box
        Vector3 diceSize = go.GetComponent<RectTransform>().sizeDelta;
        diceSize.x += 55.0f;
        go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,diceSize.x);
    }
}
