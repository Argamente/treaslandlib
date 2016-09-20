using System;
using System.Collections.Generic;
using System.Collections;

namespace TreaslandLib.Core
{
    public delegate void Listener();
    public delegate void Listener<T>(T arg1);
    public delegate void Listener<T, U>(T arg1, U arg2);
   
}
