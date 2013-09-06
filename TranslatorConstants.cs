using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOURCE.Infrastructure.Services.Implementations.Translation
{
    public class TranslatorConstant
    {
        public long Id { get; set; }
        public string LangueCode { get; set; }
    }

    public class TranslatorConstants
    {
        public readonly static TranslatorConstant French = new TranslatorConstant() { Id = 1, LangueCode = "fr-FR" };
        public readonly static TranslatorConstant English = new TranslatorConstant() { Id = 2, LangueCode = "en-GB" };
    }
}
