﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Algorithms.Tree
{
    public class Tree : Tree<Tree, string>
    {
        public Tree() { }
        public Tree(string model) : base(model) { }

        public override string Key { get => Model; set => Model = value; }
    }

}
