﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JSONTypes
{
    public class brute
    {
        public double sales { get; set; }
        public string month { get; set; }
    }

    public class eagle
    {
        public double sales { get; set; }
        public string month { get; set; }
    }

    public class delta
    {
        public double sales { get; set; }
        public string month { get; set; }
    }

    public class RootObject
    {
        public List<brute> brute { get; set; }
        public List<eagle> eagle { get; set; }
        public List<delta> delta { get; set; }
    }
}