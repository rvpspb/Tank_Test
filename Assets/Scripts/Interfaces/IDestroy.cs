using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDestroy<Ttype> 
{
    event Action<Ttype> OnDestroyed;
}
