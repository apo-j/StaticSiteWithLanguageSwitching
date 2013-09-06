using System.Collections.Generic;
using System.Web.Mvc;

namespace SOURCE.Infrastructure.Services.Translation
{
    public interface ITranslator
    {
        void Reset();

        void AddTranslation(string word, long languageId, string translation, bool isClientSide = false);

        string TranslateOrdinal(string number);

        MvcHtmlString Translate(string word, long languageId);

        MvcHtmlString Translate(string word);

        string TranslateToString(string word);
        string TranslateToString(string word, long languageId);

        Dictionary<string, string> GetAll(long languageId, bool isClientSide = false);

        Dictionary<string, string> GetAll();

        Dictionary<string, string> GetAllForClientSide();

        long DefaultLanguageID { get; set; }

        bool IsFrench();

        bool IsEnglish();
    }
}
