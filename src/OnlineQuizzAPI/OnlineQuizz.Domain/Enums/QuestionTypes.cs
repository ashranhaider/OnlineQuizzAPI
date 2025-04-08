using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuizz.Domain.Enums
{
    public enum QuestionTypes
    {
        [Description("Multiple Choice Question")]
        MultiChoice = 1,
        [Description("Short Answer")]
        ShortAnswer = 2,
        [Description("Long Answer")]
        LongAnswer = 3, 
        [Description("True or False")]
        TrueFalse = 4
    }
}
