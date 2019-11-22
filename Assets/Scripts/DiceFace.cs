using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DiceFace
{
    private DiceFaceColor faceColor;
    private List<Effect> effects = new List<Effect>();

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
                Addeffect(new Damage(1, ""));
                Addeffect(new Heal(1, ""));
                break;
            case DiceFaceColor.EARTH :
                Addeffect(new Damage(1, ""));
                Addeffect(new Shield(1, ""));
                break;
            case DiceFaceColor.FIRE :
                Addeffect(new AddBuff(new Fire(), 1, ""));
                break;
            case DiceFaceColor.NEUTRAL :
                Addeffect(new Damage(1, ""));
                break;
            case DiceFaceColor.LAVA :
                Addeffect(new Lava(2, ""));
                break;
            case DiceFaceColor.ROCK :
                Addeffect(new ShieldDamage(2, ""));
                break;
            case DiceFaceColor.ICE :
                Addeffect(new AddBuff(new Ice(), 1, ""));
                break;
            case DiceFaceColor.PHYSICAL :
                Addeffect(new TrueDamage(2, ""));
                break;
            case DiceFaceColor.POISON :
                Addeffect(new AddBuff(new Poison(), 3, ""));
                break;
            case DiceFaceColor.RADIATION :
                Addeffect(new Confuse(0, ""));
                break;
        }
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
}
