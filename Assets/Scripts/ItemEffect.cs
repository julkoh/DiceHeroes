using System;

public abstract class ItemEffect
{
    private int value;
    private string description;
    private Type effectTypeToAffect;

    public ItemEffect(int val, string desc, Type type){
        setValue(val);
        setDescription(desc);
        setEffectTypeToAffect(type);
    }

    public int getValue(){
        return value;
    }

    public void setValue(int val){
        value = val;
    }

    public string getDescription(){
        return description;
    }

    public void setDescription(string desc){
        description = desc;
    }

    public Type getEffectTypeToAffect(){
        return effectTypeToAffect;
    }

    public void setEffectTypeToAffect(Type t){
        effectTypeToAffect = t;
    }

    public abstract int changeEffectValue(int val);
}

public class MoreArmor : ItemEffect
{
    public MoreArmor(int val, string desc) : base(val, desc, typeof(Shield)){
    }
    public override int changeEffectValue(int val){
        return val+getValue();
    }
}

public class MoreDamage : ItemEffect
{
    public MoreDamage(int val, string desc) : base(val, desc, typeof(Damage)){
    }
    public override int changeEffectValue(int val){
        return val+getValue();
    }
}