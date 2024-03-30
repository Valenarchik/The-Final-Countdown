﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class UnityObjectExtensions
{
    public static IEnumerable<TOut> GetComponentsMany<TOut>(this IEnumerable<Component> innerEnumerable)
    {
        return innerEnumerable.SelectMany(x => x.GetComponents<TOut>());
    }
}