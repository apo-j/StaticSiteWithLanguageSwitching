using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using SOURCE.Business.Depots;
using SOURCE.Infrastructure.Services.Translation;
using MoreLinq;

namespace SOURCE.Infrastructure.Services.Implementations.Translation
{
    struct TranslatorEntry
    {
        public string Word { get; set; }
        public long LangueId { get; set; }

        public bool IsClientSide { get; set; }
        public string TranslatedWord { get; set; }
    }

    public class Translator : ITranslator
    {
        private static List<TranslatorEntry> _dictionnary = new List<TranslatorEntry>();
        private static bool _isTranslatorLoaded = false;
        private readonly IDepotTraduction _traductionDepot;

        public Translator(IDepotTraduction traductionDepot)
        {
            _traductionDepot = traductionDepot;

            Initialize(traductionDepot);
        }

        public void Reset()
        {
            _dictionnary.Clear();

            _isTranslatorLoaded = false;

            Initialize(_traductionDepot);
        }

        private void Initialize(IDepotTraduction traductionDepot)
        {
            if (!_isTranslatorLoaded)
            {
                lock (traductionDepot)
                {
                    if (!_isTranslatorLoaded)
                    {
                        var frenchTraduction = traductionDepot.GetAll(TranslatorConstants.French.Id);
                        var englishTraduction = traductionDepot.GetAll(TranslatorConstants.English.Id);

                        foreach (var item in frenchTraduction.ToList())
                            AddTranslation(item.Code, (long)TranslatorConstants.French.Id, item.Libelle, item.IsClientSide);

                        foreach (var item in englishTraduction.ToList())
                            AddTranslation(item.Code, (long)TranslatorConstants.English.Id, item.Libelle, item.IsClientSide);

                        _isTranslatorLoaded = true;
                    }
                }
            }
        }

        public string TranslateOrdinal(string number)
        {
            string TH = Translate("TH").ToString();
            int nb = Convert.ToInt32(number);
            string s = number;
            nb %= 100;

            if ((nb >= 11) && (nb <= 13))
            {
                s = s + TH;
            }

            if (nb % 10 == 1)
            {
                s = s + Translate("ST").ToString();
            }
            else if (nb % 10 == 2)
            {
                s = s + Translate("ND").ToString();
            }
            else if (nb % 10 == 3)
            {
                s = s + Translate("RD").ToString();
            }
            else
            {
                s = s + TH;
            }

            return s;
        }

        public void AddTranslation(string word, long languageId, string translation, bool isClientSide = false)
        {
            try
            {
                _dictionnary.Add(new TranslatorEntry()
                {
                    Word = word,
                    LangueId = languageId,

                    IsClientSide = isClientSide,

                    TranslatedWord = translation,
                });
            }
            catch (Exception)
            {
            }
        }

        public bool IsFrench()
        {
            return DefaultLanguageID == TranslatorConstants.French.Id;
        }

        public bool IsEnglish()
        {
            return DefaultLanguageID == TranslatorConstants.English.Id;
        }

        public MvcHtmlString Translate(string word, long languageId)
        {
            string translated = TranslateToString(word, languageId);
            if (translated == null) 
            {
                translated = String.Format("TRANSLATION['{0}'] NOT FOUND", word);
            }

            return MvcHtmlString.Create(translated);
        }

        public MvcHtmlString Translate(string word)
        {
            return Translate(word, DefaultLanguageID);
        }


        public string TranslateToString(string word, long languageId)
        {
            int tryCount = 0;

            while (!_isTranslatorLoaded && tryCount < 10)
            {
                Thread.Sleep(5 * 1000);
                ++tryCount;
            }

            return _dictionnary.Where(w => w.Word == word && w.LangueId == languageId).Select(w => w.TranslatedWord).FirstOrDefault();

            
        }

        public string TranslateToString(string word) 
        {
            return TranslateToString(word, DefaultLanguageID);
        }

        public Dictionary<string, string> GetAll(long languageId, bool isClientSide = false)
        {
            int tryCount = 0;

            while (!_isTranslatorLoaded && tryCount < 10)
            {
                Thread.Sleep(5 * 1000);
                ++tryCount;
            }

       
            return _dictionnary.Where(w => w.LangueId == languageId && w.IsClientSide == isClientSide).DistinctBy(w => w.Word).ToDictionary(x => x.Word, x => x.TranslatedWord);
        }

        public Dictionary<string, string> GetAll()
        {
            return GetAll(DefaultLanguageID);
        }

        public Dictionary<string, string> GetAllForClientSide() 
        {
            return GetAll(DefaultLanguageID, true);
        }
        public long DefaultLanguageID { get; set; }
    }
}
