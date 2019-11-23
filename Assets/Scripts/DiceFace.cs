using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DiceFace
{
    private DiceFaceColor faceColor;
    private List<Effect> effects = new List<Effect>();

    public DiceFace()
    {
        setFaceColor((DiceFaceColor)new System.Random().Next(4));
    }

    public DiceFace(DiceFaceColor dfc)
    {
        setFaceColor(dfc);
    }

    public DiceFaceColor getFaceColor(){
        return faceColor;
    }

    public void setFaceColor(DiceFaceColor dfc){
        faceColor = dfc;
        switch(faceColor){
            case DiceFaceColor.WATER :
                AddEffect(new Damage(1, ""));
                AddEffect(new Heal(1, ""));
                break;
            case DiceFaceColor.EARTH :
                AddEffect(new Damage(1, ""));
                AddEffect(new Shield(1, ""));
                break;
            case DiceFaceColor.FIRE :
                AddEffect(new AddBuff(new Fire(), 1, ""));
                break;
            case DiceFaceColor.NEUTRAL :
                AddEffect(new Damage(1, ""));
                break;
            case DiceFaceColor.LAVA :
                AddEffect(new Lava(2, ""));
                break;
            case DiceFaceColor.ROCK :
                AddEffect(new ShieldDamage(2, ""));
                break;
            case DiceFaceColor.ICE :
                AddEffect(new AddBuff(new Ice(), 1, ""));
                break;
            case DiceFaceColor.PHYSICAL :
                AddEffect(new TrueDamage(2, ""));
                break;
            case DiceFaceColor.POISON :
                AddEffect(new AddBuff(new Poison(), 3, ""));
                break;
            case DiceFaceColor.RADIATION :
                AddEffect(new Confuse(0, ""));
                break;
        }
    }
    
    public List<Effect> getEffect(){
        return effects;
    }

    public void AddEffect(Effect e){
        effects.Add(e);
    }

    public void applyEffects(Character source, Character target){
        foreach(Effect e in effects){
            e.apply(source, target);
        }
    }
}
