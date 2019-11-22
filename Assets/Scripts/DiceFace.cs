using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DiceFace : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private DiceFaceColor faceColor;
    private Color color;
    private List<Effect> effects = new List<Effect>();
    private Vector3 basePosition;
    private bool reposition;
    private bool action;
    private int slot;

    public DiceFace(DiceFaceColor dfc)
    {
        setFaceColor(dfc);
    }

    void Awake(){
        reposition = true;
        action = true;
        effects = new List<Effect>();
    }

    public DiceFaceColor getFaceColor(){
        return faceColor;
    }

    public void setFaceColor(DiceFaceColor dfc){
        faceColor = dfc;
        switch(faceColor){
            case DiceFaceColor.WATER :
                color = new Color32(88, 189, 255,255);
                Addeffect(new Damage(1, ""));
                Addeffect(new Heal(1, ""));
                break;
            case DiceFaceColor.EARTH :
                color = new Color32(127,64,0,255);
                Addeffect(new Damage(1, ""));
                Addeffect(new Shield(1, ""));
                break;
            case DiceFaceColor.FIRE :
                color = new Color32(255,0,0,255);
                Addeffect(new AddBuff(new Fire(), 1, ""));
                break;
            case DiceFaceColor.NEUTRAL :
                color = new Color32(127,127,127,255);
                Addeffect(new Damage(1, ""));
                break;
            case DiceFaceColor.LAVA :
                color = new Color32(127,0,0,255);
                Addeffect(new Lava(2, ""));
                break;
            case DiceFaceColor.ROCK :
                color = new Color32(64,0,0,255);
                Addeffect(new ShieldDamage(2, ""));
                break;
            case DiceFaceColor.ICE :
                color = new Color32(0, 105, 176,255);
                Addeffect(new AddBuff(new Ice(), 1, ""));
                break;
            case DiceFaceColor.PHYSICAL :
                color = new Color32(255,200,0,255);
                Addeffect(new TrueDamage(2, ""));
                break;
            case DiceFaceColor.POISON :
                color = new Color32(127,0,255,255);
                Addeffect(new AddBuff(new Poison(), 3, ""));
                break;
            case DiceFaceColor.RADIATION :
                color = new Color32(0,127,0,255);
                Addeffect(new Confuse(0, ""));
                break;
        }
    }
    
    public Color getColor(){
        return color;
    }

    public void setColor(Color c){
        color = c;
    }

    public List<Effect> geteffect(){
        return effects;
    }

    public void Addeffect(Effect e){
        effects.Add(e);
    }

    public void applyEffects(Character source, Character target){
        foreach(Effect e in effects){
            e.apply(source, target);
        }
    }

    public Vector3 getBasePosition(){
        return basePosition;
    }

    public void setBasePosition(Vector3 pos){
        basePosition = pos;
    }

    public bool getAction(){
        return action;
    }

    public int getSlot(){
        return slot;
    }

    public void setSlot(int s){
        slot = s;
    }

    public void OnDrag(PointerEventData pointerEventData){
        action = false;
        Vector3 pos = Input.mousePosition;
        pos.z = 100.0f;
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    public void OnEndDrag(PointerEventData pointerEventData){
        action = true;
        if(reposition)
            gameObject.transform.position = basePosition;
    }

    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("FusionZone") || other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.CompareTag("FusionZone") && !other.gameObject.GetComponent<FusionZone>().getActive()){
                reposition = true;
            }else{
                reposition = false;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("FusionZone") || other.gameObject.CompareTag("Enemy"))
        {
            reposition = true;
        }
    }
}
