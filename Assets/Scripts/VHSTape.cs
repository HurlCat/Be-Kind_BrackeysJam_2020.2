using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct VHSTape
{
    public Genre genre;
    public bool rewound;
    public Sprite graphics;

    public VHSTape(Genre genre, bool rewound)
    {
        this.genre = genre;
        this.rewound = rewound;

        graphics = VHSLibrary.singleton.GetRandomArtwork(genre);
    }
}

public enum Genre
{
    Action,
    Horror,
    Comedy,
    Romance,
    Documentary,
    Mystery
}