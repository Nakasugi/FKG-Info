using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FKG_Info
{
    public class FlowerSkill : XmlUnit
    {
        int Targets;
        int HitCounts;
        float Damage;
        float Chance;
        string Name;

        public FlowerSkill()
        {
            Targets = 0;
            HitCounts = 0;
            Damage = 0;
            Chance = 0;
        }

        string GetInfo()
        {
            string Info = "Deal ";


            if (Targets > 0)
                Info += Damage + "x damage to " + Targets + " enemies";
            else
                Info += HitCounts + " times " + Damage + "x damage to random enemy";

            Info += ". Trigger Chance " + Chance * 100 + "%";

            return Info;
        }

        public string CreateXMLNode()
        {
            string node = "\t<Skill ID=\"" + ID + "\">" + Environment.NewLine;

            node += "\t\t<name>" + Name + "</name>" + Environment.NewLine;
            node += "\t\t<targets>" + Targets + "</targets>" + Environment.NewLine;
            node += "\t\t<hits>" + HitCounts + "</hits>" + Environment.NewLine;
            node += "\t\t<damage>" + Damage + "</damage>" + Environment.NewLine;
            node += "\t\t<chance>" + Chance + "</chance>" + Environment.NewLine;
            node += "\t</Skill>" + Environment.NewLine;

            return node;
        }
    }
}
