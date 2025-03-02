using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB
{
    public static void Init()
    {
        foreach (var kvp in Conditions)
        {
            var condition = kvp.Value;
            condition.Id = kvp.Key;

            condition.Id = condition.Id;
        }
    }
    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        {
            ConditionID.psn, new Condition()
            {
                Name = "Poison",
                StartMessage = "a été empoisonné!",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    pokemon.UpdateHP(pokemon.MaxHP/8);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} est blessé par le poison!");
                }
            }
        },
        {
            ConditionID.brn, new Condition()
            {
                Name = "brûlure",
                StartMessage = "a été brûlé!",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    pokemon.UpdateHP(pokemon.MaxHP/16);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} brûle!");
                }
            }
        },{
            ConditionID.slp, new Condition()
            {
                Name = "sommeil",
                StartMessage = "s'est endormi!",
                OnStart = (Pokemon pokemon) =>
                {
                    // Sleep for 1-3 turns
                    pokemon.Statustime = UnityEngine.Random.Range(1, 4);
                    Debug.Log($"Pokemon sera endormi pour {pokemon.Statustime} tours");
                },
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if (pokemon.Statustime <= 0)
                    {
                        pokemon.CureStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} s'est réveillé!");
                        return true;
                    }

                    pokemon.Statustime--;
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} dort!");
                    return false;
                }
            }
        },
        {
            ConditionID.par, new Condition()
            {
                Name = "paralysie",
                StartMessage = "est paralysé!",
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if (UnityEngine.Random.Range(1, 5) == 1)
                    {
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} est paralysé et ne peut pas bouger!");
                        return false;
                    }
                    return true;
                }
            }
        },
        {
            ConditionID.frz, new Condition()
            {
                Name = "geler",
                StartMessage = "a été gelé!",
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if (UnityEngine.Random.Range(1, 5) == 1)
                    {
                        pokemon.CureStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} n’est plus gelée!");
                        return true;
                    }
                    return false;
                }
            }
        },
        {
            ConditionID.confusion, new Condition()
            {
                Name = "Confusion",
                StartMessage = "est confus!",
                OnStart = (Pokemon pokemon) =>
                {
                    // Confusion for 1-4 turns
                    pokemon.VolatileStatustime = UnityEngine.Random.Range(1, 5);
                    Debug.Log($"Pokemon sera confus pour {pokemon.VolatileStatustime} tours");
                },
                OnBeforeMove = (Pokemon pokemon) =>
                {
                    if (pokemon.VolatileStatustime <= 0)
                    {
                        pokemon.CureVolatileStatus();
                        pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} n’est plus confus!");
                        return true;
                    }

                    pokemon.VolatileStatustime--;

                    // 50% chance to hurt itself
                    if (UnityEngine.Random.Range(1, 3) == 1)
                    return true;
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} est confus!");
                    pokemon.UpdateHP(pokemon.MaxHP/8);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} se blesse dans la confusion!");
                    return false;
                }
            }
        }
    };
}
            


public enum ConditionID
{
    none, psn, brn, slp, par, frz,
    confusion
}
