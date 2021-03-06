﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace DampServer.responses
{
    [XmlRoot(ElementName = "Achievement")]
    public class AchievementListResponse : XmlResponse
    {
        public List<Archivement> Achievement { get; set; }
    }
}