﻿using System.Collections.Generic;
using Slithin.Core.Remarkable.Rendering;

namespace Slithin.Core.Remarkable.LinesAreBeatiful
{
    public struct Page
    {
        /**
         * All the layers of this page, from the bottom to the top of the
         * layer stack.
         */
        public List<Layer> Layers;
    };
}
