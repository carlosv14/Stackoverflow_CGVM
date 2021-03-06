﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QuestionListModel
    {
        public string Title { get; set; }
        public int Votes { get; set; }
        public DateTime CreationTime { get; set; }
        public string OwnerName { get; set; }

        public Guid OwnerId { get; set; }
        public Guid QuestionId { get; set; }
        public string Description { get; set; }

        public int Vistas { get; set; }
        public int CantidadRespuestas { get; set; }
        }
    }
