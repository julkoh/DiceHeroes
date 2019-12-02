using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CustomizationController : MonoBehaviour
{
    private Player player;
    private List<Dice> diceBag;
    public GameObject dicePrefab;
    public GameObject diceFacePrefab;
    public GameObject newDiceFacePrefab;
    private List<GameObject> displayedDices;
    private List<List<GameObject>> displayedDiceFaces;
    private ModificationType? modificationType;

    private enum ModificationType {
        ADD, REMOVE, SWAP
    }

    // ========================= Display Management =========================

    /// <summary>
    /// Sets up the combat environment
    /// </summary>
    void Start(){
        modificationType = null;
        player = GameController.getPlayer();
        diceBag = player.getDices();
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
        NewDiceFace.Create(new DiceFace((DiceFaceColor)new System.Random().Next(Enum.GetNames(typeof(DiceFaceColor)).Length),1), newDiceFacePrefab, newDiceFacePrefab.transform.position);
    }

    public void RemoveDiceFace(GameObject go){
        showConfirmationBox(() => {
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
            //Update Controller
            FinishCustomization();
        });
    }

    public void AddDiceFace(GameObject go){
        showConfirmationBox(() => {
            //Display new
            int diceID = displayedDices.IndexOf(go);
            List<GameObject> diceFaces = displayedDiceFaces[diceID];
            diceFaces.Add(CustomizationDiceFace.Create(new DiceFace(),diceFacePrefab,new Vector3(5 + diceFaces.Count * 55, diceFacePrefab.transform.position.y), go.transform, this));
            //Resize Dice Box
            Vector3 diceSize = go.GetComponent<RectTransform>().sizeDelta;
            diceSize.x += 55.0f;
            go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,diceSize.x);
            //Update Controller
            FinishCustomization();
        });
    }

    public void SwapDiceFaces(GameObject source, GameObject target){
        source.GetComponent<NewDiceFace>().enabled = false;
        showConfirmationBox(() => {
            target.GetComponent<CustomizationDiceFace>().setDiceFace(source.GetComponent<NewDiceFace>().getDiceFace());
            target.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/Effects/effect_"+target.GetComponent<CustomizationDiceFace>().getDiceFace().getFaceColor().ToString().ToLower());
            FinishCustomization();
            //DrawNewFace();
        });
    }

    public void FinishCustomization(){
        saveChangestoController();
        GameObject newFace = GameObject.FindGameObjectWithTag("NewDiceFace");
        if(newFace != null){
            newFace.GetComponent<NewDiceFace>().enabled = false;
            newFace.GetComponent<BoxCollider2D>().enabled = false;
            Color c = newFace.GetComponentInChildren<Image>().color;
            c.a = 0.0f;
            newFace.GetComponentInChildren<Image>().color = c;
        }
        foreach(GameObject d in displayedDices){
            d.GetComponentInChildren<Button>().interactable = false;
            d.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "";
            foreach(GameObject df in displayedDiceFaces[displayedDices.IndexOf(d)]){
                df.GetComponentInChildren<Button>().interactable = false;
            }
        }
        saveChangestoPlayer();
        GameController.setPlayer(player);
        GameController.mapScene=true;
        SceneManager.UnloadSceneAsync("CustomizationScene");   
    }

    void saveChangestoController(){
        foreach(GameObject dgo in displayedDices){
            List<DiceFace> diceFaces = new List<DiceFace>();
            foreach(GameObject dfgo in displayedDiceFaces[displayedDices.IndexOf(dgo)]){
                diceFaces.Add(dfgo.GetComponent<CustomizationDiceFace>().getDiceFace());
            }
            dgo.GetComponent<CustomizationDice>().getDice().setFaces(diceFaces);
        }
    }

    void saveChangestoPlayer(){
        List<Dice> dices = new List<Dice>();
        foreach(GameObject go in displayedDices){
            dices.Add(go.GetComponent<CustomizationDice>().getDice());
        }
        player.setDices(dices);
    }

    void showConfirmationBox(Action action){
        foreach(Button btn in GameObject.Find("Canvas").GetComponentsInChildren<Button>()){
            btn.interactable = false;
            if(btn.GetComponentInChildren<Text>() != null){
                Color c = btn.GetComponentInChildren<Text>().color;
                c.a = 0.0f;
                btn.GetComponentInChildren<Text>().color = c;
            }
        }
        foreach(BoxCollider2D col in GameObject.Find("Canvas").GetComponentsInChildren<BoxCollider2D>()){
            col.enabled = false;
        }
        GameObject box = GameObject.Find("ConfirmationBox");
        box.transform.SetAsLastSibling();
        foreach(Image img in box.GetComponentsInChildren<Image>()){
            Color c = img.color;
            c.a = 1.0f;
            img.color = c;
        }
        foreach(Text txt in box.GetComponentsInChildren<Text>()){
            Color c = txt.color;
            c.a = 1.0f;
            txt.color = c;
        }
        foreach(Button btn in box.GetComponentsInChildren<Button>()){
            btn.interactable = true;
        }
        box.transform.Find("ConfirmButton").GetComponent<Button>().onClick.AddListener(() => {
            ConfirmCustomization(action);
        });
        box.transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener(() => {
            ConfirmCustomization(()=>{
                box.transform.Find("CancelButton").GetComponent<Button>().onClick.RemoveAllListeners();
                box.transform.Find("ConfirmButton").GetComponent<Button>().onClick.RemoveAllListeners();
                GameObject newFace = GameObject.FindGameObjectWithTag("NewDiceFace");
                if(newFace != null){
                    newFace.transform.position = newFace.GetComponent<NewDiceFace>().getBasePosition();
                }
                newFace.GetComponent<NewDiceFace>().enabled = true;
            });
        });
    }

    void ConfirmCustomization(Action action){
        foreach(Button btn in GameObject.Find("Canvas").GetComponentsInChildren<Button>()){
            btn.interactable = true;
            if(btn.GetComponentInChildren<Text>() != null){
                Color c = btn.GetComponentInChildren<Text>().color;
                c.a = 1.0f;
                btn.GetComponentInChildren<Text>().color = c;
            }
        }
        foreach(BoxCollider2D col in GameObject.Find("Canvas").GetComponentsInChildren<BoxCollider2D>()){
            col.enabled = true;
        }
        GameObject box = GameObject.Find("ConfirmationBox");
        box.transform.SetAsFirstSibling();
        foreach(Image img in box.GetComponentsInChildren<Image>()){
            Color c = img.color;
            c.a = 0.0f;
            img.color = c;
        }
        foreach(Text txt in box.GetComponentsInChildren<Text>()){
            Color c = txt.color;
            c.a = 0.0f;
            txt.color = c;
        }
        foreach(Button btn in box.GetComponentsInChildren<Button>()){
            btn.interactable = false;
        }
        action();
    }
}
