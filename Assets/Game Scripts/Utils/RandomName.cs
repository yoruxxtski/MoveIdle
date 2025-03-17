using System.Collections.Generic;
using UnityEngine;

public class RandomName : MonoBehaviour
{
    private static List<string> names = new List<string> 
    { 
        "Liam", "Olivia", "Noah", "Emma", "Ethan", "Ava", "Lucas", "Sophia", "Mason", "Isabella",
        "James", "Mia", "Benjamin", "Charlotte", "Henry", "Amelia", "Alexander", "Harper", "Daniel", "Evelyn"
    };

    public static string GetRandomName() 
    {
        return names[Random.Range(0, names.Count)];
    }
}