﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiaNet.Model.Layers
{
    public class MaxPool1D : LayerConfig
    {
        public MaxPool1D()
        {
            base.Name = "MaxPool1D";
            base.Params = new ExpandoObject();
        }

        public MaxPool1D(int poolSize, int strides = 1, bool padding = true)
            : this()
        {
            PoolSize = poolSize;
            Strides = strides;
            Padding = padding;
        }

        [Newtonsoft.Json.JsonIgnore]
        public int PoolSize
        {
            get
            {
                return base.Params.PoolSize;
            }

            set
            {
                base.Params.PoolSize = value;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public int Strides
        {
            get
            {
                return base.Params.Strides;
            }

            set
            {
                base.Params.Strides = value;
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public bool Padding
        {
            get
            {
                return base.Params.Padding;
            }

            set
            {
                base.Params.Padding = value;
            }
        }
    }
}