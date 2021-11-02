using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1.Enums
{
    public enum Ratings
    {
        Unknown = 0,
        [Description("Nothing that would offend parents for viewing by children.")]
        G = 1,
        [Description("Parents urged to give 'parental guidance. May contain some material parents might not like for their young children.")]
        PG = 2,
        [Description("Parents are urged to be cautious. Some material may be inappropriate for pre-teenagers.")]
        PG13 = 3,
        [Description("Contains some adult material. Parents are urged to learn more about the film before taking their young children with them.")]
        R = 4,
        [Description("Clearly adult. Children are not admitted.")]
        NC17 = 5
    }
}
