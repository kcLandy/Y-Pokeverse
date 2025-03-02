using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokémon", menuName = "Pokémon/Create new Pokémon")]

public class PokemonBase : ScriptableObject
{
    [SerializeField] new string name;
    [TextArea] 
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    // Stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spéAttack;
    [SerializeField] int spéDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }
    public PokemonType Type2
    {
        get { return type2; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int Attack
    {
        get { return attack;}
    }

    public int Defense
    {
        get { return defense;}
    }
    public int SpéAttack
    {
        get { return spéAttack;}
    }
    public int SpéDefense
    {
        get { return spéDefense;}
    }
    public int Speed
    {
        get { return speed;}
    }
    
    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum PokemonType
{
    None,
    Normal,
    Feu,
    Eau,
    Electrik,
    Plante,
    Glace,
    Combat,
    Poison,
    Sol,
    Vol,
    Psy,
    Insecte,
    Roche,
    Spectre,
    Dragon,
}

public enum Stat
{
    Attaque,
    Defense,
    SpAttaque,
    SpDefense,
    Vitesse,

// Use to boost the moveAccuracy
    Precision,
    Esquive
}

public class TypeChart
{
    static float[][] chart =
    {
        //                       NOR  FEU   EAU   ELE   PLA   GLA   COM   POI   SOL   VOL   PSY   INS   ROC   SPE   DRA
        /* NOR */  new float []{ 1f,  1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   0.5f, 0f,   1f   },
        /* FEU */  new float []{ 1f,  0.5f, 0.5f, 1f,   2f,   2f,   1f,   1f,   1f,   1f,   1f,   2f,   0.5f, 1f,   0.5f },
        /* EAU */  new float []{ 1f,  2f,   0.5f, 1f,   0.5f, 1f,   1f,   1f,   2f,   1f,   1f,   1f,   2f,   1f,   0.5f },
        /* ELE */  new float []{ 1f,  1f,   2f,   0.5f, 0.5f, 1f,   1f,   1f,   0f,   2f,   1f,   1f,   1f,   1f,   0.5f },
        /* PLA */  new float []{ 1f,  0.5f, 2f,   1f,   0.5f, 1f,   1f,   0.5f, 2f,   0.5f, 1f,   0.5f, 2f,   1f,   0.5f },
        /* GLA */  new float []{ 1f,  0.5f, 0.5f, 1f,   2f,   0.5f, 1f,   1f,   2f,   2f,   1f,   1f,   1f,   1f,   2f   },
        /* COM */  new float []{ 2f,  1f,   1f,   1f,   1f,   2f,   1f,   0.5f, 1f,   0.5f ,0.5f, 0.5f, 2f,   0f,   1f   },
        /* POI */  new float []{ 1f,  1f,   1f,   1f,   2f,   1f,   1f,   0.5f, 0.5f, 1f,   1f,   2f,   0.5f, 0.5f, 1f   },
        /* SOL */  new float []{ 1f,  2f,   1f,   2f,   0.5f, 1f,   1f,   2f,   1f,   0f,   1f,   0.5f, 2f,   1f,   1f   },
        /* VOL */  new float []{ 1f,  1f,   1f,   0.5f, 2f,   1f,   2f,   1f,   1f,   1f,   1f,   2f,   0.5f, 1f,   1f   },
        /* PSY */  new float []{ 1f,  1f,   1f,   1f,   1f,   1f,   2f,   2f,   1f,   1f,   0.5f, 1f,   1f,   1f,   1f   },
        /* INS */  new float []{ 1f,  0.5f, 1f,   1f,   2f,   1f,   0.5f, 0.5f, 1f,   0.5f, 2f,   1f,   1f,   0.5f, 1f   },
        /* ROC */  new float []{ 1f,  2f,   1f,   1f,   1f,   2f,   0.5f, 1f,   0.5f, 2f,   1f,   1f,   1f,   1f,   1f   },
        /* SPE */  new float []{ 0f,  1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   2f,   1f   },
        /* DRA */  new float []{ 1f,  1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   2f   }
    };

    public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)
        {
            return 1;
        }

        int row = (int) attackType - 1;
        int col = (int) defenseType - 1;

        return chart[row][col];
    }
}
