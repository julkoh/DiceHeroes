using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DiceFace
{
    private DiceFaceColor faceColor;
    private List<Effect> effects = new List<Effect>();
    private float factor;

    public DiceFace()
    {
        setFaceColor((DiceFaceColor)new System.Random().Next(4));
    }

    public DiceFace(DiceFaceColor dfc, float f)
    {
        setFactor(f);
        setFaceColor(dfc);
    }

    public DiceFaceColor getFaceColor(){
        return faceColor;
    }

    public void setFaceColor(DiceFaceColor dfc){
        faceColor = dfc;
        switch(faceColor){
            case DiceFaceColor.WATER :
                AddEffect(new Damage((int)factor, ""));
                AddEffect(new Heal((int)factor, ""));
                break;
            case DiceFaceColor.EARTH :
                AddEffect(new Damage((int)factor, ""));
                AddEffect(new Shield((int)factor, ""));
                break;
            case DiceFaceColor.FIRE :
                AddEffect(new AddBuff(new Fire(), (int)factor, ""));
                break;
            case DiceFaceColor.NEUTRAL :
                AddEffect(new Damage((int)factor, ""));
                break;
            case DiceFaceColor.LAVA :
                AddEffect(new Lava((int)(2*factor), ""));
                break;
            case DiceFaceColor.ROCK :
                AddEffect(new ShieldDamage((int)(2*factor), ""));
                break;
            case DiceFaceColor.ICE :
                AddEffect(new AddBuff(new Ice(), (int)factor, ""));
                break;
            case DiceFaceColor.PHYSICAL :
                AddEffect(new TrueDamage((int)(2*factor), ""));
                break;
            case DiceFaceColor.POISON :
                AddEffect(new AddBuff(new Poison(), (int)(2*factor), ""));
                break;
            case DiceFaceColor.RADIATION :
                AddEffect(new Confuse((int)factor, ""));
                break;
        }
    }
    
    public List<Effect> getEffect(){
        return effects;
    }

    public void AddEffect(Effect e){
        effects.Add(e);
    }

    public float getFactor(){
        return factor;
    }

    public void setFactor(float f){
        factor = f;
    }

    public void applyEffects(Character source, Character target){
        foreach(Effect e in effects){
            e.apply(source, target);
        }
    }
}
