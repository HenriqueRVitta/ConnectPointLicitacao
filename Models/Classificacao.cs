﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licitacao.Models
{
    public class Classificacao
    {
        public int status { get; set; }
        public string message { get; set; }
        public int classification { get; set; }
        public int classification_id { get; set; }
        public float proba { get; set; }

    }
}