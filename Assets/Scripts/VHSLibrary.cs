using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = System.Random;

public class VHSLibrary : MonoBehaviour
{
    public static VHSLibrary singleton;
    
    public Sprite[] Action_MovieCovers;
    public Sprite[] Horror_MovieCovers;
    public Sprite[] Mystery_MovieCovers;
    public Sprite[] Comedy_MovieCovers;
    public Sprite[] Docu_MovieCovers;
    public Sprite[] Romance_MovieCovers;

    void Awake()
    {
        if(singleton != null)
            Destroy(this);
        else
            singleton = this;
    }

    public Sprite GetRandomArtwork(Genre genre)
    {
        switch (genre)
        {
            case Genre.Action:
                if(Action_MovieCovers.Length != 0)
                    return Action_MovieCovers[UnityEngine.Random.Range(0, Action_MovieCovers.Length-1)];
                break;
            case Genre.Comedy:
                if(Comedy_MovieCovers.Length != 0)
                    return Comedy_MovieCovers[UnityEngine.Random.Range(0, Comedy_MovieCovers.Length-1)];
                break;
            case Genre.Documentary:
                if(Docu_MovieCovers.Length != 0)
                    return Docu_MovieCovers[UnityEngine.Random.Range(0, Docu_MovieCovers.Length-1)];
                break;
            case Genre.Horror:
                if(Horror_MovieCovers.Length != 0)
                    return Horror_MovieCovers[UnityEngine.Random.Range(0, Horror_MovieCovers.Length-1)];
                break;
            case Genre.Mystery:
                if(Mystery_MovieCovers.Length != 0)
                    return Mystery_MovieCovers[UnityEngine.Random.Range(0, Mystery_MovieCovers.Length-1)];
                break;
            case Genre.Romance:
                if(Romance_MovieCovers.Length != 0)
                    return Romance_MovieCovers[UnityEngine.Random.Range(0, Romance_MovieCovers.Length-1)];
                break;
        }

        return null;
    }
    
}
