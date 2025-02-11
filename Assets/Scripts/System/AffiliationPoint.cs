using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffiliationPoint
{
    public MonoBehaviour Affiliation { get; set; }

    public AffiliationPoint(MonoBehaviour affiliation)
    {
        Affiliation = affiliation;
    }
}
